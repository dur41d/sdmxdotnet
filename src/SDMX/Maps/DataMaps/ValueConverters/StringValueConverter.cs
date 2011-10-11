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
        public object Parse(string s, string startTime)
        {
            return s;
        }

        public string Serialize(object obj, out string startTime)
        {
            startTime = null;
            string value = (string)obj;
            return value;
        }
    }
}
