using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using Common;

namespace SDMX.Parsers
{
    public class AnnotationMap : ClassMap<Annotation>
    {
        Annotation annotation = new Annotation();

        public AnnotationMap()
        {
            ElementsOrder("AnnotationTitle", "AnnotationType", "AnnotationURL", "AnnotationText");

            Map(o => o.Title).ToSimpleElement("AnnotationTitle", false)
                .Set(p => annotation.Title = p)
                .Converter(new StringConverter());

            Map(o => o.Type).ToSimpleElement("AnnotationType", false)
               .Set(p => annotation.Type = p)
               .Converter(new StringConverter());

            Map(o => o.Url).ToSimpleElement("AnnotationURL", false)
               .Set(p => annotation.Url = p)
               .Converter(new UriConverter());

            MapCollection(o => GetTextList(o)).ToElement("AnnotationText", false)
                .Set(v => v.ForEach(i => annotation.Text[i.Key] = i.Value))
                .ClassMap(new InternationalStringMap());
        }

        IEnumerable<KeyValuePair<Language, string>> GetTextList(Annotation annotation)
        {
            foreach (var lang in annotation.Text.Languages)
            {
                yield return new KeyValuePair<Language, string>(lang, annotation.Text[lang]);
            }
        }

        protected override Annotation Return()
        {
            return annotation;
        }
    }
}
