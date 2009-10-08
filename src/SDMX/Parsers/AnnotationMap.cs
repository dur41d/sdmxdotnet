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
        public AnnotationMap()
        {
            MapElement<string>("AnnotationTitle", false)
                .Getter(o => o.Title)
                .Setter((o, p) => o.Title = p)
                .Parser(new StringValueElementMap());

            MapElement<string>("AnnotationType", false)
                .Getter(o => o.Type)
                .Setter((o, p) => o.Title = p)
                .Parser(new StringValueElementMap());

            MapElement<Uri>("AnnotationURL", false)
                .Getter(o => o.Url)
                .Setter((o, p) => o.Url = p)
                .Parser(new ValueElementMap<Uri>(s => new Uri(s)));

            MapElementCollection<KeyValuePair<Language, string>>("AnnotationText", false)
                .Getter(o =>
                        {
                            var list = new List<KeyValuePair<Language, string>>();
                            foreach (var lang in o.Text.Languages)
                            {
                                var item = new KeyValuePair<Language, string>(lang, o.Text[lang]);
                                list.Add(item);
                            }
                            return list;
                        })
                .Setter((o, list) => list.ForEach(item => o.Text[item.Key] = item.Value))
                .Parser(new InternationalStringMap());
        }

        protected override Annotation CreateObject()
        {
            return new Annotation();
        }
    }
}
