using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX.Tests
{
    public class TimeDimension : Component
    {
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
