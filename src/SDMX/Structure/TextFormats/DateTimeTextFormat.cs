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
            var value = obj as DateTimeOffset?;
            return value != null;
        }
    }
}
