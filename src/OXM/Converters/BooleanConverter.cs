using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace OXM
{
    public class BooleanConverter : SimpleTypeConverter<bool>
    {
        public override string ToXml(bool value)
        {
            return XmlConvert.ToString(value);
        }

        public override bool ToObj(string value)
        {
            return XmlConvert.ToBoolean(value);
        }
    }

    public class NullableBooleanConverter : NullabeConverter<bool>
    {
        protected override SimpleTypeConverter<bool> Converter
        {
            get
            {
                return new BooleanConverter();
            }
        }
    }
}
