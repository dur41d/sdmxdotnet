using System;
using SDMX.Parsers;
using OXM;

namespace SDMX
{
    public class DecimalTextFormat : TextFormat
    {
        static ISimpleTypeConverter _converter = new DecimalConverter();

        internal override ISimpleTypeConverter Converter { get { return _converter; } }

        internal override bool TryCast(object obj, out object result)
        {
            if (obj is decimal)
            {
                result = (decimal)obj;
                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }

        public override Type GetValueType()
        {
            return typeof(decimal);
        }
    }
}
