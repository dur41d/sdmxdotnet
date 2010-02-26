using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using System.Text.RegularExpressions;
using Common;

namespace SDMX.Parsers
{
    internal class StringValueConverter : IValueConverter
    {
        public Value Parse(string s, string startTime)
        {
            return new StringValue(s);
        }

        public string Serialize(Value value, out string startTime)
        {
            startTime = null;
            return ((StringValue)value).ToString();
        }
    }
}
