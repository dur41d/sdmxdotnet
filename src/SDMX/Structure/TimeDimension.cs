using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public class TimeDimension : Component
    {
        public CrossSectionalAttachmentLevel CrossSectionalAttachmentLevel { get; set; }

        public TimeDimension(Concept concept)
            : base(concept)
        {
        }

        public TimeDimension(Concept concept, CodeList codeList)
            : base(concept, codeList)
        {
        }
    }
}
