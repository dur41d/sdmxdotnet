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

            MapCollection(o => o.Text).ToElement("AnnotationText", false)
                .Set(v =>  annotation.Text.Add(v))
                .ClassMap(() => new InternationalStringMap());
        }     

        protected override Annotation Return()
        {
            return annotation;
        }
    }
}
