using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace OXM
{
    public class Int32Converter : SimpleTypeConverter<int>
    {
        public override string ToXml(int value)
        {
            return XmlConvert.ToString(value);
        }

        public override int ToObj(string value)
        {
            return XmlConvert.ToInt32(value);
        }

        public override bool CanConvertToObj(string s)
        {
            int result = 0;
            return int.TryParse(s, out result);
        }
    }

    public class NullableInt32Converter : NullabeConverter<int>
    {
        protected override SimpleTypeConverter<int> Converter
        {
            get
            {
                return new Int32Converter();
            }
        }
    }
}
