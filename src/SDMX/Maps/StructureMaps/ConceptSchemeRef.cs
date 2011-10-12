
namespace SDMX.Parsers
{
    class ConceptSchemeRef
    {
        public ConceptSchemeRef()
        { }

        internal static ConceptSchemeRef Create(ConceptScheme scheme)
        {
            if (scheme == null)
                return null;

            ConceptSchemeRef schemeRef = new ConceptSchemeRef();
            schemeRef.Id = scheme.Id;
            schemeRef.Version = scheme.Version;
            schemeRef.AgencyId = scheme.AgencyId;

            return schemeRef;
        }

        public Id Id { get; set; }
        public string Version { get; set; }
        public Id AgencyId { get; set; }
    }
}
