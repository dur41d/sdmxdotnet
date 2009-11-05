using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace SDMX
{
    public class CodeRef
    {
        public CodeListRef CodeListRef { get; internal set; }        
        public ID CodeID { get; set; }
        public IList<CodeRef> Children { get; private set; }
        public ID LevelRef { get; set; }
        public ID NodeAliasID { get; set; }
        public string Version { get; set; }
        public TimePeriod ValidFrom { get; set; }
        public TimePeriod ValidTo { get; set; }

        public CodeRef Parent { get; internal set; }

        public CodeRef()
        {
            Children = new List<CodeRef>();
        }

        public CodeRef(Code code)
        {
            Contract.AssertNotNull(() => code);

            CodeID = code.ID;
            CodeListRef = new CodeListRef(code.CodeList, null);
            
            Children = new List<CodeRef>();
        }
      

        public CodeRef(Code code, params CodeRef[] children)
            : this(code)
        {
            Contract.AssertNotNull(() => children);

            AddChildren(children);                       
        }

        public CodeRef(Code code, Func<IEnumerable<CodeRef>> funcCodeRef)
            : this(code)
        {
            Contract.AssertNotNull(() => funcCodeRef);

            AddChildren(funcCodeRef());
        }

        private void AddChildren(IEnumerable<CodeRef> children)
        {
            foreach (var child in children)
            {
                child.Parent = this;
                Children.Add(child);
            }
        }


        private readonly string UrnPrefix = "urn:sdmx:org.sdmx.infomodel.";

        public Uri Urn
        {
            get
            {                
                return new Uri(string.Format("{0}.codelist={1}[{2}]".F(UrnPrefix, CodeID, Version)));
            }
        }

        public void Add(CodeRef child)
        {
            Contract.AssertNotNull(() => child);
            Contract.AssertNotNull(() => child.CodeID);
            Contract.AssertNotNull(() => child.CodeListRef);
            Contract.AssertNotNull(() => child.CodeListRef.ID);
            Contract.AssertNotNull(() => child.CodeListRef.AgencyID);
            Contract.AssertNotNull(() => child.CodeListRef.Alias);

            child.Parent = this;
            Children.Add(child);
        }
    }
}
