using System;
using SDMX.Parsers;
using OXM;

namespace SDMX
{
    public class YearTextFormat : TimePeriodTextFormatBase
    {
        static ISimpleTypeConverter _converter = new YearConverter();

        internal override ISimpleTypeConverter Converter { get { return _converter; } }

        public override bool Equals(TextFormat other)
        {
            return other is YearTextFormat;
        }
    }
}
