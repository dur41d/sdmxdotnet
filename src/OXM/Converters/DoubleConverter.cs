using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace OXM
{   
    public class DoubleConverter : SimpleTypeConverter<double>
    {
        public override bool TrySerialize(double value, out string s)
        {
            s = XmlConvert.ToString(value);
            return true;
        }

        public override bool TryParse(string s, out double value)
        {
            return double.TryParse(s, out value);
        }
    }

    public class NullableDoubleConverter : NullabeConverter<double>
    {
        DoubleConverter _converter = new DoubleConverter();
        protected override SimpleTypeConverter<double> Converter
        {
            get
            {
                return _converter;
            }
        }
    }
}
