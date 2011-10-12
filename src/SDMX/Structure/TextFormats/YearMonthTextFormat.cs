using System;
using SDMX.Parsers;

namespace SDMX
{
    public class YearMonthTextFormat : TimePeriodTextFormatBase
    {
        static IValueConverter _converter = new YearMonthValueConverter();

        internal override IValueConverter Converter { get { return _converter; } }

        public override bool IsValid(object obj)
        {
            return obj is YearMonthValue;
        }
    }
}
