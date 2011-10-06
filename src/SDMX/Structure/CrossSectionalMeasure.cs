using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace SDMX
{   
    public class CrossSectionalMeasure : Component, Item
    {
        public Id Dimension { get; set; }
        public Id Code { get; set; }

        public override ITextFormat DefaultTextFormat { get { return new StringTextFormat(); } }

        public CrossSectionalMeasure(Concept concept)
            : base(concept)
        {
        }

        public CrossSectionalMeasure(Concept concept, CodeList codeList)
            : base(concept, codeList)
        {
        }

        #region Item Members

        Id Item.Id
        {
            get { return Concept.Id; }
        }

        #endregion
    }
}
