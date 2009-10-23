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
        
        public ID ID { get; set; }
        public string Version { get; set; }
        public ID AgencyID { get; set; }
        public ConceptSchemeRef SchemeRef { get; set; }

        internal static ConceptRef Create(Concept concept)
        {
            if (concept == null)
                return null;

            ConceptRef conceptRef = new ConceptRef();
            conceptRef.ID = concept.ID;
            conceptRef.Version = concept.Version;
            conceptRef.AgencyID = concept.AgencyID;
            conceptRef.SchemeRef = ConceptSchemeRef.Create(concept.ConceptScheme);

            return conceptRef;
        }
    }

    public class ConceptSchemeRef
    {
        public ConceptSchemeRef()
        { }     

        internal static ConceptSchemeRef Create(ConceptScheme scheme)
        {
            if (scheme == null)
                return null;

            ConceptSchemeRef schemeRef = new ConceptSchemeRef();
            schemeRef.ID = scheme.ID;
            schemeRef.Version = scheme.Version;
            schemeRef.AgencyID = scheme.AgencyID;

            return schemeRef;
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
