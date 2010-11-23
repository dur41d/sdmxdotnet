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
    internal class CrossSectionalAttachmentLevelMap : AttributeGroupTypeMap<CrossSectionalAttachmentLevel>
    {        
        bool dataSet, group, section, observation;

        public CrossSectionalAttachmentLevelMap()
        {
            MapAttribute(o => o == CrossSectionalAttachmentLevel.DataSet, "crossSectionalAttachDataSet", false)
              .Set(v => dataSet = v)
              .Converter(new BooleanConverter());

            MapAttribute(o => o == CrossSectionalAttachmentLevel.Group, "crossSectionalAttachGroup", false)
              .Set(v => group = v)
              .Converter(new BooleanConverter());

            MapAttribute(o => o == CrossSectionalAttachmentLevel.Section, "crossSectionalAttachSection", false)
              .Set(v => section = v)
              .Converter(new BooleanConverter());

            MapAttribute(o => o == CrossSectionalAttachmentLevel.Observation, "crossSectionalAttachObservation", false)
              .Set(v => observation = v)
              .Converter(new BooleanConverter());
        }

        protected override CrossSectionalAttachmentLevel Return()
        {
            if (dataSet)
            {
                return CrossSectionalAttachmentLevel.DataSet;
            }
            else if (group)
            {
                return CrossSectionalAttachmentLevel.Group;
            }
            else if (section)
            {
                return CrossSectionalAttachmentLevel.Section;
            }
            else if (observation)
            {
                return CrossSectionalAttachmentLevel.Observation;
            }
            else
            {
                return CrossSectionalAttachmentLevel.None;
            }
        }
    }
}
