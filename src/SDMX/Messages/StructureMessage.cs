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
            var where = ExpressionExtensions.True<CodeList>();

            where = where.And(i => i.Id == codeListId);
            if (agencyId != null) where.And(i => i.AgencyId == agencyId);
            if (version != null) where.And(i => i.Version == version);
            var exp = where.Compile();

            return CodeLists.Where(exp).SingleOrDefault();
        }



        public Concept GetConcept(Id coneceptSchemeId, Id coneceptSchemeAgencyId, string coneceptSchemeVersion, 
            Id conceptId, Id conceptAgencyId, string conceptVersion)
        {
            IEnumerable<Concept> list = null;

            if (coneceptSchemeId != null)
            {
                var where = ExpressionExtensions.True<ConceptScheme>();
                where = where.And(i => i.Id == coneceptSchemeId);
                if (coneceptSchemeAgencyId != null) where.And(i => i.AgencyId == coneceptSchemeAgencyId);
                if (coneceptSchemeVersion != null) where.And(i => i.Version == coneceptSchemeVersion);
                var exp = where.Compile();

                list = ConceptSchemes.Where(exp).SingleOrDefault();
            }
            else
            {
                list = Concepts;
            }

            if (list != null)
            {
                var where2 = ExpressionExtensions.True<Concept>();
                where2 = where2.And(i => i.Id == conceptId);
                if (conceptAgencyId != null) where2.And(i => i.AgencyId == conceptAgencyId);
                if (conceptVersion != null) where2.And(i => i.Version == conceptVersion);
                var exp2 = where2.Compile();

                return list.Where(exp2).SingleOrDefault();
            }

            return null;
        }

        protected override StructureMessage GetThis()
        {
            return this;
        }
    }
}
