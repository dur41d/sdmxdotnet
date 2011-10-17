using System;
using SDMX.Parsers;
using OXM;

namespace SDMX
{
    public class StringTextFormat : TextFormat
    {
        static ISimpleTypeConverter _converter = new StringConverter();

        internal override ISimpleTypeConverter Converter { get { return _converter; } }

        public override bool IsValid(object obj)
        {
            var value = obj as string;
            return value != null;
        }

        public override bool Equals(TextFormat other)
        {
            return other is StringTextFormat;
        }

        public override Type GetValueType()
        {
            return typeof(string);
        }
    }
}
