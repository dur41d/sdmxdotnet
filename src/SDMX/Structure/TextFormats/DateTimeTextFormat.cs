using System;
using SDMX.Parsers;

namespace SDMX
{
    public class DateTimeTextFormat : TimePeriodTextFormatBase
    {
        static IValueConverter _converter = new DateTimeValueConverter();

        internal override IValueConverter Converter { get { return _converter; } }

        public override bool IsValid(object obj)
        {
            return obj is DateTimeValue;
        }

        public override bool Equals(TextFormat other)
        {
            return other is DateTimeTextFormat;
        }

        public override Type GetValueType()
        {
            return typeof(DateTimeValue);
        }
    }
}
