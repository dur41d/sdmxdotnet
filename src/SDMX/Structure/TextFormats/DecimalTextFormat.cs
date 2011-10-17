using System;
using SDMX.Parsers;
using OXM;

namespace SDMX
{
    public class DecimalTextFormat : TextFormat
    {
        static ISimpleTypeConverter _converter = new DecimalConverter();

        internal override ISimpleTypeConverter Converter { get { return _converter; } }

        public override bool IsValid(object obj)
        {
            var value = obj as decimal?;
            return value != null;
        }

        public override bool Equals(TextFormat other)
        {
            return other is DecimalTextFormat;
        }

        public override Type GetValueType()
        {
            return typeof(decimal);
        }
    }
}
