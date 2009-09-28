using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX.Tests
{

    public class Dimension : Component
    {
        public Dimension(Concept concept)
            : base(concept)
        {

        }

        public Dimension(Concept concept, CodeList codeList)
            : base(concept, codeList)
        {
        }
    }
}