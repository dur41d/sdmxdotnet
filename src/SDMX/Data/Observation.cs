using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public class Observation
    {
        public TimePeriod Time { get; set; }
        public object Value { get; set; }

        public AttributeValues Attributes { get; private set; }
        public IList<Annotation> Annotations { get; private set; }

        public Observation()
        {
            Attributes = new AttributeValues();
            Annotations = new List<Annotation>();
        }
    }
}
