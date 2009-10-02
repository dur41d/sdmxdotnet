using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public class Group
    {
        public IList<Attribute> Attributes { get; internal set; }
        public IList<Annotation> Annotations { get; internal set; }

        internal Group()
        {         
            
        }

    }
}
