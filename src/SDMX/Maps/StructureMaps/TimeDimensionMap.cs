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
    public class TimeDimensionMap : CompoenentMap<TimeDimension>
    {
        TimeDimension _timeDimension;

        public TimeDimensionMap(StructureMessage message)
            : base(message)
        {
            AttributesOrder("conceptRef",
                            "codelist",
                            "crossSectionalAttachmentLevel");

            ElementsOrder("TextFormat", "Annotations");

            Map(o => o.CrossSectionalAttachmentLevel).ToAttributeGroup("crossSectionalAttachmentLevel", CrossSectionalAttachmentLevel.None)
               .Set(v => _timeDimension.CrossSectionalAttachmentLevel = v)
               .GroupTypeMap(new CrossSectionalAttachmentLevelMap());

        }

        protected override TimeDimension Create(Concept conecpt)
        {
            _timeDimension = new TimeDimension(conecpt);
            return _timeDimension;
        }

        protected override void AddAnnotation(Annotation annotation)
        {
            _timeDimension.Annotations.Add(annotation);
        }

        protected override TimeDimension Return()
        {
            return _timeDimension;
        }
    }
}
