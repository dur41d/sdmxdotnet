using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace OXM
{
    public class StringConverter : SimpleTypeConverter<string>
    {
        public override bool TrySerialize(string value, out string s)
        {
            s = value;
            return true;
        }

        public override bool TryParse(string s, out string value)
        {
            value = s;
            return true;
        }
    }

}