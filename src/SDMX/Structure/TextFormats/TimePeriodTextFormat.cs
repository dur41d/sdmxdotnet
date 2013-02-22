using System;
using SDMX.Parsers;
using Common;
using OXM;

namespace SDMX
{
    public class TimePeriodTextFormat : TimePeriodTextFormatBase
    {
        TimePeriodConverter _converter = new TimePeriodConverter();

        internal override ISimpleTypeConverter Converter
        {
            get { return _converter; }
        }

        internal override bool TryCast(object obj, out object result)
        {
            if (obj is TimePeriod)
            {
                result = (TimePeriod)obj;
                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }

        public override Type GetValueType()
        {
            return typeof(TimePeriod);
        }
    }
}
