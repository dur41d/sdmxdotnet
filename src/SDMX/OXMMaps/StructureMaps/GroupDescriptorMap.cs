using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using Common;

namespace SDMX.Parsers
{
    internal class GroupDescriptorMap : AnnotableArtefactMap<Group>
    {
        Group _group;
        
        public GroupDescriptorMap(KeyFamily keyFamily)
        {
            ElementsOrder("DimensionRef", "AttachmentConstraintRef", "Description", "Annotations");

            Map(o => o.ID).ToAttribute("id", true)
                .Set(v => _group = keyFamily.CreateNewGroup(v))
                .Converter(new IDConverter());

            MapCollection(o => o.Dimensions.Select(d => d.Concept.ID)).ToSimpleElement("DimensionRef", true)
                .Set(v => _group.AddDimension(v))
                .Converter(new IDConverter());

            Map(o => o.AttachmentConstraintRef).ToSimpleElement("AttachmentConstraintRef", false)
                .Set(v => _group.AttachmentConstraintRef = v)
                .Converter(new IDConverter());

            MapCollection(o => o.Description).ToElement("Description", false)
                .Set(v => _group.Description.Add(v))
                .ClassMap(() => new InternationalStringMap());
        }

        protected override void AddAnnotation(Annotation annotation)
        {
            _group.Annotations.Add(annotation);
        }

        protected override Group Return()
        {
            return _group;
        }
    }
}
