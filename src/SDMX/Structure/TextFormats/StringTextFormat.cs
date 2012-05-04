using System;
using SDMX.Parsers;
using OXM;

namespace SDMX
{
    public class StringTextFormat : TextFormat
    {
        ISimpleTypeConverter _converter = new StringConverter();

        internal override ISimpleTypeConverter Converter { get { return _converter; } }

        internal override bool TryCast(object obj, out object result)
        {
            if (obj == null)
            {
                result = null;
                return true;
            }
            if (obj is string)
            {
                result = (string)obj;
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
            return typeof(string);
        }
    }
}
