using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using Common;

namespace SDMX.Parsers
{
    internal class PrimaryMeasureMap : MeasureMap<PrimaryMeasure>
    {
        PrimaryMeasure _measure;

        internal PrimaryMeasureMap(StructureMessage message)
            : base(message)
        {
            AttributesOrder("conceptRef", "codelist");

            ElementsOrder("TextFormat", "Annotations");
        }

        protected override PrimaryMeasure Create(Concept conecpt)
        {
            _measure = new PrimaryMeasure(conecpt);
            return _measure;
        }

        protected override void AddAnnotation(Annotation annotation)
        {
            _measure.Annotations.Add(annotation);
        }

        protected override PrimaryMeasure Return()
        {
            return _measure;
        }
    }
}
