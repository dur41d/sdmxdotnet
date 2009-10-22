using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using Common;

namespace SDMX.Parsers
{
    internal class GroupMap : AnnotableArtefactMap<Group>
    {
        Group _group;
        
        public GroupMap(KeyFamily keyFamily)
        {
            ElementsOrder("DimensionRef", "AttachmentConstraintRef", "Description", "Annotations");

            Map(o => o.ID).ToAttribute("id", true)
                .Set(v => _group = keyFamily.CreateNewGroup(v))
                .Converter(new IDConverter());

            MapCollection(o => o.Dimensions.Select(d => d.Concept.ID)).ToSimpleElement("DimensionRef", true)
                .Set(v => v.ForEach(i => _group.AddDimension(i)))
                .Converter(new IDConverter());

            Map(o => o.AttachmentConstraintRef).ToSimpleElement("AttachmentConstraintRef", false)
                .Set(v => _group.AttachmentConstraintRef = v)
                .Converter(new IDConverter());

            MapCollection(o => GetTextList(o.Description)).ToElement("Description", false)
                .Set(v => v.ForEach(i => _group.Description[i.Key] = i.Value))
                .ClassMap(new InternationalStringMap());
        }

        IEnumerable<KeyValuePair<Language, string>> GetTextList(InternationalText text)
        {
            foreach (var lang in text.Languages)
            {
                yield return new KeyValuePair<Language, string>(lang, text[lang]);
            }
        }

        protected override void SetAnnotations(IEnumerable<Annotation> annotations)
        {
            annotations.ForEach(i => _group.Annotations.Add(i));
        }

        protected override Group Return()
        {
            return _group;
        }
    }
}
