using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace SDMX
{
    public class CodeList : MaintainableArtefact, IEnumerable<Code>
    {
        Dictionary<Id, Code> codes = new Dictionary<Id, Code>();
        
        public CodeList(InternationalString name, Id id, Id agencyId)
            : base(id, agencyId)
        {
            Name.Add(name);
        }

        public void Add(Code code)
        {            
            Contract.AssertNotNull(code, "code");
            code.CodeList = this;
            codes.Add(code.Id, code);
        }

        public void Remove(Code code)
        {
            Contract.AssertNotNull(code, "code");
            codes.Remove(code.Id);
        }

        public Code Get(Id codeId)
        {
            return codes.GetValueOrDefault(codeId, null);
        }

        public bool Contains(Id codeId)
        {
            return codes.ContainsKey(codeId);
        }

        public override Uri Urn
        {
            get 
            {
                return new Uri(string.Format("{0}.codelist={1}:{2}[{3}]".F(UrnPrefix, AgencyId, Id, Version)));
            }
        }

        #region IEnumerable<Code> Members

        public IEnumerator<Code> GetEnumerator()
        {   
            foreach (var item in codes)
            {
                yield return item.Value;
            }
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
