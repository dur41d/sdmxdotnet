using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace OXM
{
    public class StringConverter : SimpleTypeConverter<string>
    {
        public override string ToXml(string value)
        {
            return value;
        }

        public override string ToObj(string value)
        {
            return value;
        }
    }

}