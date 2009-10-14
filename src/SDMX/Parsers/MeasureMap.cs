using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using Common;

namespace SDMX.Parsers
{
    internal abstract class MeasureMap<T> : AnnotableArtefactMap<T>
            where T : Measure
    {
        DSD _dsd;

        protected AttributeMap<T, ID> conceptRef;
        protected AttributeMap<T, string> conceptVersion;
        protected AttributeMap<T, ID> conceptAgency;
        protected AttributeMap<T, ID> conceptSchemeRef;
        protected AttributeMap<T, ID> conceptSchemeAgency;

        private AttributeMap<T, ID> codelist;
        private AttributeMap<T, string> codelistVersion;
        private AttributeMap<T, ID> codelistAgency;

        internal MeasureMap(DSD dsd)
        {
            _dsd = dsd;

            conceptRef = MapAttribute<ID>("conceptRef", true)
               .Getter(d => d.Concept.ID)
               .Parser(s => s);

            conceptVersion = MapAttribute<string>("conceptVersion", false)
               .Getter(d => d.Concept.Version)
               .Parser(s => s);

            conceptAgency = MapAttribute<ID>("conceptAgency", false)
               .Getter(d => d.Concept.AgencyID)
               .Parser(s => s);

            conceptSchemeRef = MapAttribute<ID>("conceptSchemeRef", false)
              .Getter(d => d.Concept.ConceptScheme.ID)
              .Parser(s => s);

            conceptSchemeAgency = MapAttribute<ID>("conceptSchemeAgency", false)
              .Getter(d => d.Concept.ConceptScheme.AgencyID)
              .Parser(s => s);

            codelist = MapAttribute<ID>("codelist", false)
            .Getter(d => d.CodeList.ID)
            .Parser(s => s);

            codelistVersion = MapAttribute<string>("codelistVersion", false)
              .Getter(d => d.CodeList.Version)
              .Parser(s => s);

            codelistAgency = MapAttribute<ID>("codelistAgency", false)
              .Getter(d => d.CodeList.AgencyID)
              .Parser(s => s);

            MapElement<TextFormat>("TextFormat", false)
                .Parser(new TextFormatMap())
                .Getter(o => o.TextFormat)
                .Setter(p => Instance.TextFormat = p);
        }

        protected void SetMeasureProperties(T component)
        {
            //component.CodeList = _dsd.GetCodeList(codelist.Value, codelistAgency.Value, codelistVersion.Value);
        }
    }
}
