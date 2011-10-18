using System;
using SDMX.Parsers;
using Common;
using OXM;

namespace SDMX
{
    public class TimePeriodTextFormat : TimePeriodTextFormatBase
    {
        static ISimpleTypeConverter _converter = new TimePeriodConverter();

        internal override ISimpleTypeConverter Converter { get { return _converter; } }


        public override bool IsValid(object obj)
        {
            return obj is TimePeriod;
        }

        public override bool Equals(TextFormat other)
        {
            return other is TimePeriodTextFormat;
        }

        public override Type GetValueType()
        {
            return typeof(TimePeriod);
        }
    }
}
