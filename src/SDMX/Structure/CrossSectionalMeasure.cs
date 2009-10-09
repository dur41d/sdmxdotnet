using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public class CrossSectionalMeasure : Measure
    {
        public ID Dimension { get; set; }
        public ID Code { get; set; }

        public CrossSectionalMeasure(Concept concept)
            : base(concept)
        {
        }

        public CrossSectionalMeasure(Concept concept, CodeList codeList)
            : base(concept, codeList)
        {
        }
    }
}
