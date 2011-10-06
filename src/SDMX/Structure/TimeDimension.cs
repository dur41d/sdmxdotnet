using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public class TimeDimension : Component
    {
        public CrossSectionalAttachmentLevel CrossSectionalAttachmentLevel { get; set; }

        public override ITextFormat DefaultTextFormat { get { return new TimePeriodTextFormat();} }

        public new ITimePeriodTextFormat TextFormat
        {
            get { return (ITimePeriodTextFormat)TextFormatImpl; }
            set { TextFormatImpl = value; }
        }

        // only used to fake Covariance with Component
        // http://peisker.net/dotnet/covariance.htm
        protected override ITextFormat TextFormatImpl
        {
            get
            {
                return base.TextFormatImpl;
            }
            set
            {
                if (!(value is ITimePeriodTextFormat))
                {
                    throw new SDMXException("The text format for TimeDimension must be of type 'ITimePeriodTextFormat' but was found to be of type '{0}'.", value.GetType());
                }
                base.TextFormatImpl = (ITimePeriodTextFormat)value;
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
