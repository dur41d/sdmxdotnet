using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using Common;

namespace SDMX.Parsers
{
    internal class GroupDescriptorMap : AnnotableArtefactMap<GroupDescriptor>
    {
        GroupDescriptor _group;
        
        public GroupDescriptorMap(KeyFamily keyFamily)
        {
            ElementsOrder("DimensionRef", "AttachmentConstraintRef", "Description", "Annotations");

            Map(o => o.Id).ToAttribute("id", true)
                .Set(v => _group = keyFamily.CreateNewGroup(v))
                .Converter(new IdConverter());

            MapCollection(o => o.Dimensions.Select(d => d.Concept.Id)).ToSimpleElement("DimensionRef", true)
                .Set(v => _group.AddDimension(v))
                .Converter(new IdConverter());

            Map(o => o.AttachmentConstraintRef).ToSimpleElement("AttachmentConstraintRef", false)
                .Set(v => _group.AttachmentConstraintRef = v)
                .Converter(new IdConverter());

            int count = 0;
            MapCollection(o => { count = o.Description.Count(); return o.Description; }).ToElement("Description", false)
                .Set(v => _group.Description.Add(v))
                .ClassMap(() => new InternationalStringMap(count));
        }

        protected override void AddAnnotation(Annotation annotation)
        {
            _group.Annotations.Add(annotation);
        }

        protected override GroupDescriptor Return()
        {
            return _group;
        }
    }
}
