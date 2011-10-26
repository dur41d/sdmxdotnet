using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using SDMX.Parsers;
using System.Xml;
using System.IO;
using Linq.Expressions;

namespace SDMX
{ 
    public class StructureMessage : MessageBase<StructureMessage>
    {
        public IList<CodeList> CodeLists { get; private set; }
        public IList<Concept> Concepts { get; private set; }
        public IList<ConceptScheme> ConceptSchemes { get; private set; }
        public IList<KeyFamily> KeyFamilies { get; private set; }
        public IList<HierarchicalCodeList> HierarchicalCodeLists { get; private set; }

        public StructureMessage()
        {
            CodeLists = new List<CodeList>();
            Concepts = new List<Concept>();
            ConceptSchemes = new List<ConceptScheme>();
            KeyFamilies = new List<KeyFamily>();
            HierarchicalCodeLists = new List<HierarchicalCodeList>();
        }

        public CodeList FindCodeList(Id codeListId, Id agencyId, string version)
        {
            return CodeLists.Where(i => i.Id == codeListId
                    && (agencyId == null || i.AgencyId == agencyId)
                    && (version == null || i.Version == version)).SingleOrDefault();
        }



        public Concept GetConcept(Id coneceptSchemeId, Id coneceptSchemeAgencyId, string coneceptSchemeVersion, 
            Id conceptId, Id conceptAgencyId, string conceptVersion)
        {
            IEnumerable<Concept> list = null;
            if (coneceptSchemeId != null)
            {  
                list = ConceptSchemes.Where(i => i.Id == coneceptSchemeId
                    && (coneceptSchemeAgencyId == null || i.AgencyId == coneceptSchemeAgencyId)
                    && (coneceptSchemeVersion == null || i.Version == coneceptSchemeVersion)).SingleOrDefault();
            }
            else
            {
                list = Concepts;
            }

            if (list != null)
            {

                return list.Where(i => i.Id == conceptId
                    && (conceptAgencyId == null || i.AgencyId == conceptAgencyId)
                    && (conceptVersion == null || i.Version == conceptVersion)).SingleOrDefault();
            }

            return null;
        }
    }
}
