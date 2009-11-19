using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public class Observation : AnnotableArtefact, IAttachableArtefact
    {
        public TimePeriod Time { get; set; }
        public object Value { get; set; }

        public AttributeValueCollection Attributes { get; private set; }

        public Observation()
        {
            Attributes = new AttributeValueCollection(null, AttachmentLevel.Observation);
        }
    }
}
