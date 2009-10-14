using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using Common;

namespace SDMX.Parsers
{
    public abstract class AnnotableArtefactMap<T> : ClassMap<T> where T : AnnotableArtefact
    {
        public AnnotableArtefactMap()
        {
            MapElementContainer("Annotations", false)
                .MapElementCollection<Annotation>("Annotation", true)
                .Getter(o => o.Annotations)
                .Setter(p => Instance.Annotations.Add(p))
                .Parser(() => new AnnotationMap());
        }
    }
}
