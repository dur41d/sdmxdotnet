using System;
using SDMX.Parsers;

namespace SDMX
{
    public class TimePeriodTextFormat : TimePeriodTextFormatBase
    {
        static IValueConverter _converter = new TimePeriodValueConverter();

        internal override IValueConverter Converter { get { return _converter; } }

        public override bool IsValid(object obj)
        {
            var value = obj as DateTimeOffset?;
            return value != null;
        }
    }
}
