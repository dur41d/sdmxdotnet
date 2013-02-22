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
    internal class DimensionMap : ComponentMap<Dimension>
    {
        Dimension _dimension;
        Concept _concept;
        
        public DimensionMap(StructureMessage message)
            : base(message)
        {
            AttributesOrder("conceptRef",
                            "codelist",
                            "isMeasureDimension",
                            "isFrequencyDimension",
                            "isEntityDimension",
                            "isCountDimension",
                            "isNonObservationTimeDimension",
                            "isIdentityDimension",
                            "crossSectionalAttachmentLevel");

            ElementsOrder("TextFormat", "Annotations");           

            Map(o => o.IsMeasureDimension).ToAttribute("isMeasureDimension", false, "false")
                .Set(v => _dimension.IsMeasureDimension = v)
                .Converter(new BooleanConverter());

            Map(o => o.IsFrequencyDimension).ToAttribute("isFrequencyDimension", false, "false")
                .Set(v => _dimension.IsFrequencyDimension = v)
                .Converter(new BooleanConverter());

            Map(o => o.IsEntityDimension).ToAttribute("isEntityDimension", false, "false")
                .Set(v => _dimension.IsEntityDimension = v)
                .Converter(new BooleanConverter());

            Map(o => o.IsCountDimension).ToAttribute("isCountDimension", false, "false")
                .Set(v => _dimension.IsCountDimension = v)
                .Converter(new BooleanConverter());

            Map(o => o.IsNonObservationTimeDimension).ToAttribute("isNonObservationTimeDimension", false, "false")
                .Set(v => _dimension.IsNonObservationTimeDimension = v)
                .Converter(new BooleanConverter());

            Map(o => o.IsIdentityDimension).ToAttribute("isIdentityDimension", false, "false")
                .Set(v => _dimension.IsIdentityDimension = v)
                .Converter(new BooleanConverter());

            Map(o => o.CrossSectionalAttachmentLevel).ToAttributeGroup("crossSectionalAttachmentLevel", CrossSectionalAttachmentLevel.None)
              .Set(v => _dimension.CrossSectionalAttachmentLevel = v)
              .GroupTypeMap(new CrossSectionalAttachmentLevelMap());
        }

        protected override Dimension Create(Concept concept)
        {
            _concept = concept;
            if (concept != null)
            {
                _dimension = new Dimension(concept);
            }
            else
            {
                var fakeConcept = CreateFakeConcept();
                _dimension = new Dimension(fakeConcept);
            }

            return _dimension;
        }

        protected override void AddAnnotation(Annotation annotation)
        {
            _dimension.Annotations.Add(annotation);
        }

        protected override Dimension Return()
        {
            if (_concept == null)
            {
                return null;
            }
            else
            {
                return _dimension;
            }
        }
    }
}