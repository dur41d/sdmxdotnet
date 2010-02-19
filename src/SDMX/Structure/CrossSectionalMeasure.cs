using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace SDMX
{   
    public class XMeasure : Component, Item
    {
        public ID Dimension { get; set; }
        public ID Code { get; set; }

        public XMeasure(Concept concept)
            : base(concept)
        {
        }

        public XMeasure(Concept concept, CodeList codeList)
            : base(concept, codeList)
        {
        }

        #region Item Members

        ID Item.ID
        {
            get { return Concept.ID; }
        }

        #endregion
    }
}
