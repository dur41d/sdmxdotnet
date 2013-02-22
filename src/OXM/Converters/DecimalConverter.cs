using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace OXM
{
    public class DecimalConverter : SimpleTypeConverter<decimal>
    {
        public override bool TrySerialize(decimal value, out string s)
        {
            s = XmlConvert.ToString(value);
            return true;
        }

        public override bool TryParse(string s, out decimal value)
        {
            return decimal.TryParse(s, out value);
        }
    }


    public class NullableDecimalConverter : NullabeConverter<decimal>
    {
        DecimalConverter _converter = new DecimalConverter();
        protected override SimpleTypeConverter<decimal> Converter
        {
            get
            {
                return _converter;
            }
        }
    }

}
