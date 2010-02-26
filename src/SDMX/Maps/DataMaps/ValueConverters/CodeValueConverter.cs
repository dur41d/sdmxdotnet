using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using System.Text.RegularExpressions;
using Common;

namespace SDMX.Parsers
{
    internal class CodeValueConverter : IValueConverter
    {
        public Value Parse(string s, string startTime)
        {
            return CodeValue.Create(s);
        }

        public string Serialize(Value value, out string startTime)
        {
            startTime = null;
            return ((CodeValue)value).ToString();
        }
    }
}
