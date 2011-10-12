using OXM;

namespace SDMX.Parsers
{
    class ConceptRefMap : AttributeGroupTypeMap<ConceptRef>
    {
        ConceptRef conceptRef = new ConceptRef();

        public ConceptRefMap()
        {
            conceptRef.SchemeRef = new ConceptSchemeRef();

            MapAttribute(o => o.Id, "conceptRef", true)
                .Set(v => conceptRef.Id = v)
                .Converter(new IdConverter());

            MapAttribute(o => o.Version, "conceptVersion", false)
                .Set(v => conceptRef.Version = v)
                .Converter(new StringConverter());

            MapAttribute(o => o.AgencyId, "conceptAgency", false)
                .Set(v => conceptRef.AgencyId = v)
                .Converter(new IdConverter());

            MapAttribute(o => o.SchemeRef == null ? null : o.SchemeRef.Id, "conceptSchemeRef", false)
                .Set(v => conceptRef.SchemeRef.Id = v)
                .Converter(new IdConverter());

            MapAttribute(o => o.SchemeRef == null ? null : o.SchemeRef.AgencyId, "conceptSchemeAgency", false)
               .Set(v => conceptRef.SchemeRef.AgencyId = v)
               .Converter(new IdConverter());
        }

        protected override ConceptRef Return()
        {
            return conceptRef;
        }
    }
}
