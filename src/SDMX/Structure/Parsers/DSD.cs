using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Common;
using OXM;

namespace SDMX.Parsers
{
    public class DSD
    {
        XDocument _dsd;

        public DSD()
        {
        }

        public DSD(XDocument dsd)
        {
            _dsd = dsd;
        }

        public Concept GetConcept(ID concept, ID conceptAgency)
        {
            return new Concept("name", concept, conceptAgency);
        }

        internal CodeList GetCodeList(ID id, ID agencyID, string version)
        {
            var codeList = new CodeList("name", id, agencyID);
            return codeList;
        }

        internal CodeList GetCodeList(CodelistRef codelistRef)
        {
            var codeList = new CodeList("name", codelistRef.ID, codelistRef.AgencyID);
            return codeList;
        }

        internal Concept GetConcept(ID id, ID agencyID, string version, ID schemeID, ID schemeAgencyID)
        {
            var conceptScheme = new ConceptScheme(schemeID, schemeAgencyID);
            var concept = new Concept("name", id, agencyID);
            conceptScheme.Add(concept);
            return concept;
        }

        internal Concept GetConcept(ConceptRef conceptRef)
        {
            var conceptScheme = new ConceptScheme(conceptRef.SchemeRef.ID, conceptRef.SchemeRef.AgencyID);
            var concept = new Concept("name", conceptRef.ID, conceptRef.AgencyID);
            conceptScheme.Add(concept);
            return concept;
        }
    }
}
