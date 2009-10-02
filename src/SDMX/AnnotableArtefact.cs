using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public abstract class AnnotableArtefact
    {
        public IList<Annotation> Annotations { get; protected set; }

        public AnnotableArtefact()
        {
            Annotations = new List<Annotation>();
        }
    }
}