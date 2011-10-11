using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public class TimeDimension : Component
    {
        public CrossSectionalAttachmentLevel CrossSectionalAttachmentLevel { get; set; }

        public override TextFormat DefaultTextFormat { get { return new TimePeriodTextFormat();} }

        public new TimePeriodTextFormatBase TextFormat
        {
            get { return (TimePeriodTextFormatBase)TextFormatImpl; }
            set { TextFormatImpl = value; }
        }

        // only used to fake Covariance with Component
        // http://peisker.net/dotnet/covariance.htm
        protected override TextFormat TextFormatImpl
        {
            get
            {
                return base.TextFormatImpl;
            }
            set
            {
                if (!(value is TimePeriodTextFormatBase))
                {
                    throw new SDMXException("The text format for TimeDimension must be of type 'ITimePeriodTextFormat' but was found to be of type '{0}'.", value.GetType());
                }
                base.TextFormatImpl = (TimePeriodTextFormatBase)value;
            }
        }

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
