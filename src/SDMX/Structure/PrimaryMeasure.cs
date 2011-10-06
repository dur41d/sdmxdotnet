using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public class PrimaryMeasure : Component
    {  
        public PrimaryMeasure(Concept concept)
            : base(concept)
        {
        }

        public PrimaryMeasure(Concept concept, CodeList codeList)
            : base(concept, codeList)
        {
        }

        public override ITextFormat DefaultTextFormat { get { return new StringTextFormat(); } }
    }
}
