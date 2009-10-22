using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using Common;

namespace SDMX.Parsers
{
    internal class CrossSectionalMeasureMap : MeasureMap<CrossSectionalMeasure>
    {
        CrossSectionalMeasure _measure;
        
        internal CrossSectionalMeasureMap(DSD dsd)
            : base(dsd)
        {
            AttributesOrder("conceptRef",
                            "codelist",
                            "measureDimension",
                            "code");

            ElementsOrder("TextFormat", "Annotations");

            Map(o => o.Dimension).ToAttribute("measureDimension", true)
                .Set(v => _measure.Dimension = v)
                .Converter(new IDConverter());

            Map(o => o.Code).ToAttribute("code", true)
                .Set(v => _measure.Code = v)
                .Converter(new IDConverter());
        }      

        protected override CrossSectionalMeasure Create(Concept conecpt)
        {
            _measure = new CrossSectionalMeasure(conecpt);
            return _measure;
        }

        protected override void SetAnnotations(IEnumerable<Annotation> annotations)
        {
            annotations.ForEach(i => _measure.Annotations.Add(i));
        }

        protected override CrossSectionalMeasure Return()
        {
            return _measure;
        }
    }
}
