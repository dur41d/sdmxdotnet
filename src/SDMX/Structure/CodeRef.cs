using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace SDMX
{
    public class CodeRef
    {
        List<CodeRef> _children = new List<CodeRef>();

        public CodeListRef CodeListRef { get; internal set; }        
        public ID CodeID { get; set; }
        public ID LevelRef { get; set; }
        public ID NodeAliasID { get; set; }
        public string Version { get; set; }
        public ITimePeriod ValidFrom { get; set; }
        public ITimePeriod ValidTo { get; set; }
        public CodeRef Parent { get; internal set; }

        public IEnumerable<CodeRef> Children
        {
            get
            {
                return _children.AsEnumerable();
            }
        }

        public CodeRef()
        { }

        public CodeRef(Code code)
        {
            Contract.AssertNotNull(() => code);

            CodeID = code.ID;
            CodeListRef = new CodeListRef(code.CodeList, ID.Empty);
        }
      

        public CodeRef(Code code, params CodeRef[] children)
            : this(code)
        {
            Contract.AssertNotNull(() => children);

            AddChildren(children);                       
        }

        public CodeRef(Code code, IEnumerable<Code> children)
            : this(code)
        {
            Contract.AssertNotNull(() => children);

            var list = CreateList(children);
            AddChildren(list);
        }

        public CodeRef(Code code, Func<IEnumerable<Code>> funcCodeRef)
            : this(code)
        {
            Contract.AssertNotNull(() => funcCodeRef);

            var list = CreateList(funcCodeRef());
            AddChildren(list);
        }

        private List<CodeRef> CreateList(IEnumerable<Code> codes)
        {
            List<CodeRef> result = new List<CodeRef>();
            foreach (var code in codes)
            {
                result.Add(new CodeRef(code));
            }
            return result;
        }

        private void AddChildren(IEnumerable<CodeRef> children)
        {
            foreach (var child in children)
            {
                child.Parent = this;
                _children.Add(child);
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
            _children.Add(child);
        }
    }
}
