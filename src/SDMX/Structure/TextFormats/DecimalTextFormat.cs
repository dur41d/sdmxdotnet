using System;
using SDMX.Parsers;

namespace SDMX
{
    public class DecimalTextFormat : TextFormat
    {
        static IValueConverter _converter = new DecimalValueConverter();

        internal override IValueConverter Converter { get { return _converter; } }

        public override bool IsValid(object obj)
        {
            var value = obj as decimal?;
            return value != null;
        }

        public override bool Equals(TextFormat other)
        {
            return other is DecimalTextFormat;
        }
    }
}
