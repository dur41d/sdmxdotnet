using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace OXM
{
    public class Int32Converter : SimpleTypeConverter<int>
    {
        public override bool TrySerialize(int value, out string s)
        {
            s = XmlConvert.ToString(value);
            return true;
        }

        public override bool TryParse(string s, out int value)
        {
            return int.TryParse(s, out value);
        }
    }

    public class NullableInt32Converter : NullabeConverter<int>
    {
        Int32Converter _converter = new Int32Converter();
        protected override SimpleTypeConverter<int> Converter
        {
            get
            {
                return _converter;
            }
        }
    }
}
