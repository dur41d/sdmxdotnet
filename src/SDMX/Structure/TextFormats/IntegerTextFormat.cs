using System;
using SDMX.Parsers;

namespace SDMX
{
    public class IntegerTextFormat : TextFormat
    {
        static IValueConverter _converter = new IntegerValueConverter();

        internal override IValueConverter Converter { get { return _converter; } }

        public override bool IsValid(object obj)
        {
            var value = obj as int?;
            return value != null;
        }

        public override bool Equals(TextFormat other)
        {
            return other is IntegerTextFormat;
        }
    }
}
