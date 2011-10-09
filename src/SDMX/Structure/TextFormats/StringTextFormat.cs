using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Common;

namespace SDMX
{
    public class StringTextFormat : ITextFormat
    {
        public bool IsValid(Value value)
        {
            return value is StringValue;
        }

        public Type GetValueType()
        {
            return typeof(StringValue);
        }

        public bool TryParse(string s, string startTime, out object value)
        {            
            value = s;
            return true;
        }
    }
}
