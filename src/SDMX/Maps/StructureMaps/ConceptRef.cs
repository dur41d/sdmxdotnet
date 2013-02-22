
namespace SDMX.Parsers
{
    class ConceptRef
    {
        public ConceptRef()
        {             
        }     
        
        public Id Id { get; set; }
        public string Version { get; set; }
        public Id AgencyId { get; set; }
        public ConceptSchemeRef SchemeRef { get; set; }

        internal static ConceptRef Create(Concept concept)
        {
            if (concept == null)
                return null;

            ConceptRef conceptRef = new ConceptRef();
            conceptRef.Id = concept.Id;
            conceptRef.Version = concept.Version;
            conceptRef.AgencyId = concept.AgencyId;
            conceptRef.SchemeRef = ConceptSchemeRef.Create(concept.ConceptScheme);

            return conceptRef;
        }
    }
}