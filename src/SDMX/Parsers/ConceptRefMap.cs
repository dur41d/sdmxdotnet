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
    public class ConceptRef
    {
        public ConceptRef()
        {             
        }

        public ConceptRef(Concept concept)
        {
            ID = concept.ID;
            Version = concept.Version;
            AgencyID = concept.AgencyID;
            SchemeRef = new ConceptSchemeRef(concept.ConceptScheme);
        }
        
        public ID ID { get; set; }
        public string Version { get; set; }
        public ID AgencyID { get; set; }
        public ConceptSchemeRef SchemeRef { get; set; }
    }

    public class ConceptSchemeRef
    {
        public ConceptSchemeRef()
        { }

        public ConceptSchemeRef(ConceptScheme schemeRef)
        {
            ID = schemeRef.ID;
            Version = schemeRef.Version;
            AgencyID = schemeRef.AgencyID;
        }
        
        public ID ID { get; set; }
        public string Version { get; set; }
        public ID AgencyID { get; set; }
    }
    
    public class ConceptRefMap : AttributeGroupTypeMap<ConceptRef>
    {        
        ConceptRef conceptRef = new ConceptRef();

        public ConceptRefMap()
        {
            conceptRef.SchemeRef = new ConceptSchemeRef();

            MapAttribute(o => o.ID, "conceptRef", true)
                .Set(v => conceptRef.ID = v)
                .Converter(new IDConverter());

            MapAttribute(o => o.Version, "conceptVersion", false)
                .Set(v => conceptRef.Version = v)
                .Converter(new StringConverter());

            MapAttribute(o => o.AgencyID, "conceptAgency", false)
                .Set(v => conceptRef.AgencyID = v)
                .Converter(new IDConverter());

            MapAttribute(o => conceptRef.SchemeRef.ID, "conceptSchemeRef", false)
                .Set(v => conceptRef.SchemeRef.ID = v)
                .Converter(new IDConverter());

            MapAttribute(o => conceptRef.SchemeRef.AgencyID, "conceptSchemeAgency", false)
               .Set(v => conceptRef.SchemeRef.AgencyID = v)
               .Converter(new IDConverter());
        }

        protected override ConceptRef Return()
        {
            return conceptRef;
        }
    }
}
