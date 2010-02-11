using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace SDMX
{
    public class CodeList : MaintainableArtefact, IEnumerable<Code>
    {
        Dictionary<ID, Code> codes = new Dictionary<ID, Code>();
        
        public CodeList(InternationalString name, ID id, ID agencyID)
            : base(id, agencyID)
        {
            Name.Add(name);
        }

        public void Add(Code code)
        {
            Contract.AssertNotNull(() => code);
            code.CodeList = this;
            codes.Add(code.ID, code);
        }

        public void Remove(Code code)
        {
            Contract.AssertNotNull(() => code);
            codes.Remove(code.ID);
        }

        public Code Get(ID codeID)
        {
            return codes.GetValueOrDefault(codeID, null);
        }

        public bool Contains(ID codeID)
        {
            return codes.ContainsKey(codeID);
        }
        
        public override Uri Urn
        {
            get 
            {
                return new Uri(string.Format("{0}.codelist={1}:{2}[{3}]".F(UrnPrefix, AgencyID, ID, Version)));
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
