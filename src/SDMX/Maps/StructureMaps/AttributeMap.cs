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
    internal class AttributeMap : CompoenentMap<Attribute>
    {
        Attribute _attribute;

        public AttributeMap(StructureMessage message)
            : base(message)
        {
            AttributesOrder("conceptRef",
                            "codelist",
                            "attachmentLevel",
                            "assignmentStatus",
                            "isTimeFormat",
                            "crossSectionalAttachmentLevel",
                            "isEntityAttribute",
                            "isNonObservationalTimeAttribute",
                            "isCountAttribute",
                            "isFrequencyAttribute",
                            "isIdentityAttribute");

            ElementsOrder("TextFormat", "AttachmentGroup", "AttachmentMeasure", "Annotations");            

            Map(o => o.AttachementLevel).ToAttribute("attachmentLevel", true)
                .Set(v => _attribute.AttachementLevel = v)
                .Converter(new EnumConverter<AttachmentLevel>());

            Map(o => o.AssignmentStatus).ToAttribute("assignmentStatus", true)
                .Set(v => _attribute.AssignmentStatus = v)
                .Converter(new EnumConverter<AssignmentStatus>());

            Map(o => o.IsTimeFormat).ToAttribute("isTimeFormat", false, "false")
                .Set(v => _attribute.IsTimeFormat = v)
                .Converter(new BooleanConverter());

            Map(o => o.CrossSectionalAttachmentLevel).ToAttributeGroup("crossSectionalAttachmentLevel", CrossSectionalAttachmentLevel.None)
               .Set(v => _attribute.CrossSectionalAttachmentLevel = v)
               .GroupTypeMap(new CrossSectionalAttachmentLevelMap());

            Map(o => o.IsEntityAttribute).ToAttribute("isEntityAttribute", false, "false")
                .Set(v => _attribute.IsEntityAttribute = v)
                .Converter(new BooleanConverter());

            Map(o => o.IsNonObservationalTimeAttribute).ToAttribute("isNonObservationalTimeAttribute", false, "false")
                .Set(v => _attribute.IsNonObservationalTimeAttribute = v)
                .Converter(new BooleanConverter());

            Map(o => o.IsCountAttribute).ToAttribute("isCountAttribute", false, "false")
                .Set(v => _attribute.IsCountAttribute = v)
                .Converter(new BooleanConverter());

            Map(o => o.IsFrequencyAttribute).ToAttribute("isFrequencyAttribute", false, "false")
                .Set(v => _attribute.IsFrequencyAttribute = v)
                .Converter(new BooleanConverter());

            Map(o => o.IsIdentityAttribute).ToAttribute("isIdentityAttribute", false, "false")
               .Set(v => _attribute.IsIdentityAttribute = v)
               .Converter(new BooleanConverter());

            MapCollection(o => o.AttachmentGroups).ToSimpleElement("AttachmentGroup", false)
                .Set(v => _attribute.AttachmentGroups.Add(v))
                .Converter(new IDConverter());

            MapCollection(o => o.AttachmentMeasures).ToSimpleElement("AttachmentMeasure", false)
                .Set(v => _attribute.AttachmentMeasures.Add(v))
                .Converter(new IDConverter());
        }      


        protected override Attribute Create(Concept conecpt)
        {
            _attribute = new Attribute(conecpt);
            return _attribute;
        }

        protected override void AddAnnotation(Annotation annotation)
        {
            _attribute.Annotations.Add(annotation);
        }

        protected override Attribute Return()
        {
            return _attribute;
        }
    }
}
