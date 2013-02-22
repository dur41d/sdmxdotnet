using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace OXM
{
    public class BooleanConverter : SimpleTypeConverter<bool>
    {
        public override bool TrySerialize(bool value, out string s)
        {
            s = XmlConvert.ToString(value);
            return true;
        }

        public override bool TryParse(string s, out bool value)
        {
            return bool.TryParse(s, out value);
        }
    }

    public class NullableBooleanConverter : NullabeConverter<bool>
    {
        BooleanConverter _converter = new BooleanConverter();
        protected override SimpleTypeConverter<bool> Converter
        {
            get
            {
                return _converter;
            }
        }
    }
}
