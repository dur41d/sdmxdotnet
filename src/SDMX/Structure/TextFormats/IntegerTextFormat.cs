using System;
using SDMX.Parsers;
using OXM;

namespace SDMX
{
    public class IntegerTextFormat : TextFormat
    {
        static ISimpleTypeConverter _converter = new Int32Converter();

        internal override ISimpleTypeConverter Converter { get { return _converter; } }

        internal override bool TryCast(object obj, out object result)
        {         
            if (obj is int)
            {
                result = (int)obj;
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
            return typeof(int);
        }
    }
}
