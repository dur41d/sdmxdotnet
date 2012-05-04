using System;
using SDMX.Parsers;
using OXM;

namespace SDMX
{
    public class DoubleTextFormat : TextFormat
    {
        static ISimpleTypeConverter _converter = new DoubleConverter();

        internal override ISimpleTypeConverter Converter { get { return _converter; } }

        internal override bool TryCast(object obj, out object result)
        {
            if (obj is double)
            {
                result = (double)obj;
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
            return typeof(double);
        }
    }
}
