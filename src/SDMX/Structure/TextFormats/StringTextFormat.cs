using System;
using SDMX.Parsers;

namespace SDMX
{
    public class StringTextFormat : TextFormat
    {
        static IValueConverter _converter = new StringValueConverter();

        internal override IValueConverter Converter { get { return _converter; } }

        public override bool IsValid(object obj)
        {
            var value = obj as string;
            return value != null;
        }

        public override bool Equals(TextFormat other)
        {
            return other is StringTextFormat;
        }
    }
}
