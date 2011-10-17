using System;
using SDMX.Parsers;
using OXM;

namespace SDMX
{
    public class YearMonthTextFormat : TimePeriodTextFormatBase
    {
        static ISimpleTypeConverter _converter = new YearMonthConverter();

        internal override ISimpleTypeConverter Converter { get { return _converter; } }

        public override bool Equals(TextFormat other)
        {
            return other is YearMonthTextFormat;
        }
    }
}
