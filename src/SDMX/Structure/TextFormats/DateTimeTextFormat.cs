using System;
using SDMX.Parsers;
using OXM;

namespace SDMX
{
    public class DateTimeTextFormat : TimePeriodTextFormatBase
    {
        static ISimpleTypeConverter _converter = new DateTimeConverter();

        internal override ISimpleTypeConverter Converter { get { return _converter; } }

        public override bool Equals(TextFormat other)
        {
            return other is DateTimeTextFormat;
        }
    }
}
