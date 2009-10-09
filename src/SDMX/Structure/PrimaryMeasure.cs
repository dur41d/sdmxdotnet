using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public class PrimaryMeasure : Measure
    {  
        public PrimaryMeasure(Concept concept)
            : base(concept)
        {
        }

        public PrimaryMeasure(Concept concept, CodeList codeList)
            : base(concept, codeList)
        {
        }
    }
}
