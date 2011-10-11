using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using System.Text.RegularExpressions;
using Common;

namespace SDMX.Parsers
{
    internal class DecimalValueConverter : IValueConverter
    {
        public object Parse(string s, string startTime)
        {
            return decimal.Parse(s);
        }

        public string Serialize(object obj, out string startTime)
        {
            decimal value = (decimal)obj;
            startTime = null;
            return value.ToString();
        }
    }
}
