using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace SDMX
{
    public class HierarchicalCodeList : MaintainableArtefact, IEnumerable<Hierarchy>
    {
        private List<CodeListRef> codeListRefs = new List<CodeListRef>();
        private List<Hierarchy> hierarchies = new List<Hierarchy>();

        public HierarchicalCodeList(ID id, ID agencyID)
            : base(id, agencyID)
        {
        }

        public HierarchicalCodeList(InternationalString name, ID id, ID agencyID)
            : this(id, agencyID)
        {
            Name.Add(name);
        }

        public IEnumerable<CodeListRef> CodeListRefs
        {
            get
            {
                return codeListRefs.AsEnumerable();
            }
        }

        public void AddCodeList(CodeList codeList, ID alias)
        {
            Contract.AssertNotNull(() => codeList);

            AddCodeList(new CodeListRef(codeList, alias));
        }

        public void AddCodeList(CodeListRef codelistRef)
        {
            Contract.AssertNotNull(() => codelistRef);
            Contract.AssertNotNull(() => codelistRef.ID);
            Contract.AssertNotNull(() => codelistRef.AgencyID);
            Contract.AssertNotNull(() => codelistRef.Alias);

            if (codeListRefs.Exists(c => c == codelistRef))
            {
                throw new SDMXException("CodeListRef '{0}' already exists.", codelistRef);
            }

            codeListRefs.Add(codelistRef);
        }

        public void Add(Hierarchy hierarchy)
        {
            Contract.AssertNotNull(() => hierarchy);

            hierarchy.HierarchicalCodeList = this;
            SetCodeListRefAliases(hierarchy);
            hierarchies.Add(hierarchy);
        }

        private void SetCodeListRefAliases(Hierarchy hierarchy)
        {
            foreach (var codeRef in hierarchy.GetCodeRefs())
            {
                if (codeRef.CodeListRef.Alias.IsEmpty())
                {
                    var codeListRef = codeListRefs.Where(c => c.ID == codeRef.CodeListRef.ID
                        && c.AgencyID == codeRef.CodeListRef.AgencyID).FirstOrDefault();

                    if (codeListRef == null)
                    {
                        string message = "Code list ref is not found in the hierarchical code list for code '{0}' CodeListID '{1}', CodeListAgency '{2}'. Use AddCodeList to add the code list before adding the code associated with it.";
                        throw new SDMXException(message, codeRef.CodeID, codeRef.CodeListRef.ID, codeRef.CodeListRef.AgencyID);
                    }

                    codeRef.CodeListRef.Alias = codeListRef.Alias;
                }
            }
        }

        public override Uri Urn
        {
            get
            {
                return new Uri(string.Format("{0}.hierarchicalcodelist={1}:{2}[{3}]".F(UrnPrefix, AgencyID, ID, Version)));
            }
        }

        #region IEnumerable<Hierarchy> Members

        public IEnumerator<Hierarchy> GetEnumerator()
        {
            foreach (var item in hierarchies)
            {
                yield return item;
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
