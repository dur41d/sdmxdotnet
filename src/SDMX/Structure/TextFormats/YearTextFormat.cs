using System;
using SDMX.Parsers;

namespace SDMX
{
    public class YearTextFormat : TimePeriodTextFormatBase
    {
        static IValueConverter _converter = new YearValueConverter();

        internal override IValueConverter Converter { get { return _converter; } }

        public override bool IsValid(object obj)
        {
            return obj is YearValue;
        }

        public override bool Equals(TextFormat other)
        {
            return other is YearTextFormat;
        }

        public override Type GetValueType()
        {
            return typeof(YearValue);
        }
    }
}
