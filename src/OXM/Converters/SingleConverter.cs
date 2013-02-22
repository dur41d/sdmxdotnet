using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace OXM
{
    public class SingleConverter : SimpleTypeConverter<float>
    {
        public override bool TrySerialize(float value, out string s)
        {
            s = XmlConvert.ToString(value);
            return true;
        }

        public override bool TryParse(string s, out float value)
        {
            return float.TryParse(s, out value);
        }
    }

    public class NullableSingleConverter : NullabeConverter<float>
    {
        SingleConverter _converter = new SingleConverter();
        protected override SimpleTypeConverter<float> Converter
        {
            get
            {
                return _converter;
            }
        }
    }
}
