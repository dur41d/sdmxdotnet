using System;
using SDMX.Parsers;

namespace SDMX
{
    public class DoubleTextFormat : TextFormat
    {
        static IValueConverter _converter = new DoubleValueConverter();

        internal override IValueConverter Converter { get { return _converter; } }

        public override bool IsValid(object obj)
        {
            var value = obj as double?;
            return value != null;
        }

        public override bool Equals(TextFormat other)
        {
            return other is DecimalTextFormat;
        }

        public override Type GetValueType()
        {
            return typeof(double);
        }
    }
}
