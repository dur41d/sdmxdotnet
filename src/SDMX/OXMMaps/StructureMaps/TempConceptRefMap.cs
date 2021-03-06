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
    public class TempConceptRef
    {
        public TempConceptRef()
        {             
        }     
        
        public ID ID { get; set; }
        public string Version { get; set; }
        public ID AgencyID { get; set; }
        public TempConceptSchemeRef SchemeRef { get; set; }

        internal static TempConceptRef Create(Concept concept)
        {
            if (concept == null)
                return null;

            TempConceptRef conceptRef = new TempConceptRef();
            conceptRef.ID = concept.ID;
            conceptRef.Version = concept.Version;
            conceptRef.AgencyID = concept.AgencyID;
            conceptRef.SchemeRef = TempConceptSchemeRef.Create(concept.ConceptScheme);

            return conceptRef;
        }
    }

    public class TempConceptSchemeRef
    {
        public TempConceptSchemeRef()
        { }     

        internal static TempConceptSchemeRef Create(ConceptScheme scheme)
        {
            if (scheme == null)
                return null;

            TempConceptSchemeRef schemeRef = new TempConceptSchemeRef();
            schemeRef.ID = scheme.ID;
            schemeRef.Version = scheme.Version;
            schemeRef.AgencyID = scheme.AgencyID;

            return schemeRef;
        }
        
        public ID ID { get; set; }
        public string Version { get; set; }
        public ID AgencyID { get; set; }
    }
    
    public class TempConceptRefMap : AttributeGroupTypeMap<TempConceptRef>
    {        
        TempConceptRef conceptRef = new TempConceptRef();

        public TempConceptRefMap()
        {
            conceptRef.SchemeRef = new TempConceptSchemeRef();

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

        protected override TempConceptRef Return()
        {
            return conceptRef;
        }
    }
}
