using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public class Dimension : Component, Item
    {
        public bool IsMeasureDimension { get; set; }
        public bool IsFrequencyDimension { get; set; }
        public bool IsEntityDimension { get; set; }
        public bool IsCountDimension { get; set; }
        public bool IsNonObservationTimeDimension { get; set; }
        public bool IsIdentityDimension { get; set; }
        public CrossSectionalAttachmentLevel CrossSectionalAttachmentLevel { get; set; }

        public override TextFormat DefaultTextFormat { get { return new StringTextFormat(); } }
        
        public Dimension(Concept concept)
            : base(concept)
        {

        }

        public Dimension(Concept concept, CodeList codeList)
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