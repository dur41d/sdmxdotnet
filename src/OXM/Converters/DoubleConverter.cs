using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace OXM
{   
    public class DoubleConverter : SimpleTypeConverter<double>
    {
        public override string ToXml(double value)
        {
            return XmlConvert.ToString(value);
        }

        public override double ToObj(string value)
        {
            return XmlConvert.ToDouble(value);
        }
    }

    public class NullableDoubleConverter : NullabeConverter<double>
    {
        protected override SimpleTypeConverter<double> Converter
        {
            get
            {
                return new DoubleConverter();
            }
        }
    }
}
