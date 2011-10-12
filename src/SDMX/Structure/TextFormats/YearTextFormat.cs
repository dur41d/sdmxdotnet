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
    }
}
