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

        internal PrimaryMeasureMap(DSD dsd)
            : base(dsd)
        {
            AttributesOrder("conceptRef", "codelist");

            ElementsOrder("TextFormat", "Annotations");
        }

        protected override PrimaryMeasure Create(Concept conecpt)
        {
            _measure = new PrimaryMeasure(conecpt);
            return _measure;
        }

        protected override void SetAnnotations(IEnumerable<Annotation> annotations)
        {
            annotations.ForEach(i => _measure.Annotations.Add(i));
        }

        protected override PrimaryMeasure Return()
        {
            return _measure;
        }
    }
}
