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
        protected abstract void SetAnnotations(IEnumerable<Annotation> annotations);
        
        public AnnotableArtefactMap()
        {
            MapContainer("Annotations", false)
                .MapCollection(o => o.Annotations).ToElement("Annotation", true)
                .Set(v => SetAnnotations(v))
                .ClassMap(new AnnotationMap());
        }
        
    }
}
