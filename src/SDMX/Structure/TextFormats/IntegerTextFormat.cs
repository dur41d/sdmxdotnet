using System;
using SDMX.Parsers;
using OXM;

namespace SDMX
{
    public class IntegerTextFormat : TextFormat
    {
        static ISimpleTypeConverter _converter = new Int32Converter();

        internal override ISimpleTypeConverter Converter { get { return _converter; } }

        public override bool IsValid(object obj)
        {
            var value = obj as int?;
            return value != null;
        }

        public override bool Equals(TextFormat other)
        {
            return other is IntegerTextFormat;
        }

        public override Type GetValueType()
        {
            return typeof(int);
        }
    }
}
