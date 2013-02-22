using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using Common;

namespace SDMX.Parsers
{
    internal class XMeasureMap : ComponentMap<CrossSectionalMeasure>
    {
        CrossSectionalMeasure _measure;
        Concept _concept;
        
        internal XMeasureMap(StructureMessage message)
            : base(message)
        {
            AttributesOrder("conceptRef",
                            "codelist",
                            "measureDimension",
                            "code");

            ElementsOrder("TextFormat", "Annotations");

            Map(o => o.Dimension).ToAttribute("measureDimension", true)
                .Set(v => _measure.Dimension = v)
                .Converter(new IdConverter());

            Map(o => o.Code).ToAttribute("code", true)
                .Set(v => _measure.Code = v)
                .Converter(new IdConverter());
        }      

        protected override CrossSectionalMeasure Create(Concept concept)
        {
            _concept = concept;
            if (concept != null)
            {
                _measure = new CrossSectionalMeasure(concept);
            }
            else
            {
                var fakeConcept = CreateFakeConcept();
                _measure = new CrossSectionalMeasure(fakeConcept);
            }

            return _measure;
        }

        protected override void AddAnnotation(Annotation annotation)
        {
            _measure.Annotations.Add(annotation);
        }

        protected override CrossSectionalMeasure Return()
        {
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
