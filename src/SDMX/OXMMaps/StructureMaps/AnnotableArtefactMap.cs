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
        protected abstract void AddAnnotation(Annotation annotation);
        
        public AnnotableArtefactMap()
        {
            MapContainer("Annotations", false)
                .MapCollection(o => o.Annotations).ToElement("Annotation", false)
                .Set(v => AddAnnotation(v))
                .ClassMap(() => new AnnotationMap());
        }
        
    }
}
