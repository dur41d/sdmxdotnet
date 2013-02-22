using System;
using SDMX.Parsers;
using OXM;

namespace SDMX
{
    public class YearMonthTextFormat : TimePeriodTextFormatBase
    {
        static ISimpleTypeConverter _converter = new YearMonthConverter();

        internal override ISimpleTypeConverter Converter { get { return _converter; } }

        internal override bool TryCast(object obj, out object result)
        {
            if (obj is DateTimeOffset)
            {
                result = (DateTimeOffset)obj;
                return true;
            }
            else if (obj is DateTime)
            {
                result = ToDateTimeOffset((DateTime)obj);
                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }

        DateTimeOffset ToDateTimeOffset(DateTime dateTime)
        {
            return new DateTimeOffset(dateTime.Ticks, TimeSpan.Zero);
        }

        public override Type GetValueType()
        {
            return typeof(DateTimeOffset);
        }
    }
}
