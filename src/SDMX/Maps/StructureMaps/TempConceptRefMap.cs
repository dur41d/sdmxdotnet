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
        
        public Id Id { get; set; }
        public string Version { get; set; }
        public Id AgencyId { get; set; }
        public TempConceptSchemeRef SchemeRef { get; set; }

        internal static TempConceptRef Create(Concept concept)
        {
            if (concept == null)
                return null;

            TempConceptRef conceptRef = new TempConceptRef();
            conceptRef.Id = concept.Id;
            conceptRef.Version = concept.Version;
            conceptRef.AgencyId = concept.AgencyId;
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
            schemeRef.Id = scheme.Id;
            schemeRef.Version = scheme.Version;
            schemeRef.AgencyId = scheme.AgencyId;

            return schemeRef;
        }
        
        public Id Id { get; set; }
        public string Version { get; set; }
        public Id AgencyId { get; set; }
    }
    
    public class TempConceptRefMap : AttributeGroupTypeMap<TempConceptRef>
    {        
        TempConceptRef conceptRef = new TempConceptRef();

        public TempConceptRefMap()
        {
            conceptRef.SchemeRef = new TempConceptSchemeRef();

            MapAttribute(o => o.Id, "conceptRef", true)
                .Set(v => conceptRef.Id = v)
                .Converter(new IdConverter());

            MapAttribute(o => o.Version, "conceptVersion", false)
                .Set(v => conceptRef.Version = v)
                .Converter(new StringConverter());

            MapAttribute(o => o.AgencyId, "conceptAgency", false)
                .Set(v => conceptRef.AgencyId = v)
                .Converter(new IdConverter());

            MapAttribute(o => conceptRef.SchemeRef.Id, "conceptSchemeRef", false)
                .Set(v => conceptRef.SchemeRef.Id = v)
                .Converter(new IdConverter());

            MapAttribute(o => conceptRef.SchemeRef.AgencyId, "conceptSchemeAgency", false)
               .Set(v => conceptRef.SchemeRef.AgencyId = v)
               .Converter(new IdConverter());
        }

        protected override TempConceptRef Return()
        {
            return conceptRef;
        }
    }
}
