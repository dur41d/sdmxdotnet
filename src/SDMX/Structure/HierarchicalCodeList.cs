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

        public HierarchicalCodeList(Id id, Id agencyId)
            : base(id, agencyId)
        {
        }

        public HierarchicalCodeList(InternationalString name, Id id, Id agencyId)
            : this(id, agencyId)
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

        public void AddCodeList(CodeList codeList, Id alias)
        {
            Contract.AssertNotNull(codeList, "codeList");

            AddCodeList(new CodeListRef(codeList, alias));
        }

        public void AddCodeList(CodeListRef codelistRef)
        {
            Contract.AssertNotNull(codelistRef, "codelistRef");
            Contract.AssertNotNull(codelistRef.Id, "codelistRef.Id");
            Contract.AssertNotNull(codelistRef.AgencyId, "codelistRef.AgencyId");
            Contract.AssertNotNull(codelistRef.Alias, "codelistRef.Alias");

            if (codeListRefs.Exists(c => c == codelistRef))
            {
                throw new SDMXException("CodeListRef '{0}' already exists.", codelistRef);
            }

            codeListRefs.Add(codelistRef);
        }

        public void Add(Hierarchy hierarchy)
        {
            Contract.AssertNotNull(hierarchy, "hierarchy");

            hierarchy.HierarchicalCodeList = this;
            SetCodeListRefAliases(hierarchy);
            hierarchies.Add(hierarchy);
        }

        private void SetCodeListRefAliases(Hierarchy hierarchy)
        {
            foreach (var codeRef in hierarchy.GetCodeRefs())
            {
                if (codeRef.CodeListRef.Alias == null)
                {
                    var codeListRef = codeListRefs.Where(c => c.Id == codeRef.CodeListRef.Id
                        && c.AgencyId == codeRef.CodeListRef.AgencyId).FirstOrDefault();

                    if (codeListRef == null)
                    {
                        string message = "Code list ref is not found in the hierarchical code list for code '{0}' CodeListId '{1}', CodeListAgency '{2}'. Use AddCodeList to add the code list before adding the code associated with it.";
                        throw new SDMXException(message, codeRef.CodeId, codeRef.CodeListRef.Id, codeRef.CodeListRef.AgencyId);
                    }

                    codeRef.CodeListRef.Alias = codeListRef.Alias;
                }
            }
        }

        public override Uri Urn
        {
            get
            {
                return new Uri(string.Format("{0}.hierarchicalcodelist={1}:{2}[{3}]".F(UrnPrefix, AgencyId, Id, Version)));
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
