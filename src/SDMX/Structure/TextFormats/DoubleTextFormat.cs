using System;
using SDMX.Parsers;
using OXM;

namespace SDMX
{
    public class DoubleTextFormat : TextFormat
    {
        static ISimpleTypeConverter _converter = new DoubleConverter();

        internal override ISimpleTypeConverter Converter { get { return _converter; } }

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
