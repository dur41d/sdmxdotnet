using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using Common;

namespace SDMX.Parsers
{
    internal class PrimaryMeasureMap : ComponentMap<PrimaryMeasure>
    {
        PrimaryMeasure _measure;
        Concept _concept;

        internal PrimaryMeasureMap(StructureMessage message)
            : base(message)
        {
            AttributesOrder("conceptRef", "codelist");

            ElementsOrder("TextFormat", "Annotations");
        }

        protected override PrimaryMeasure Create(Concept concept)
        {
            _concept = concept;
            if (concept != null)
            {
                _measure = new PrimaryMeasure(concept);
            }
            else
            {
                var fakeConcept = CreateFakeConcept();
                _measure = new PrimaryMeasure(fakeConcept);
            }

            return _measure;
        }

        protected override void AddAnnotation(Annotation annotation)
        {
            _measure.Annotations.Add(annotation);
        }

        protected override PrimaryMeasure Return()
        {
            if (_measure.CodeList == null && _measure.TextFormat == null)
            {
                _measure.TextFormat = new DecimalTextFormat();
            }

            if (_concept == null)
            {
                return null;
            }
            else
            {
                return _measure;
            }
        }
    }
}
