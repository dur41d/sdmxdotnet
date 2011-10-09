using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Common;

namespace SDMX
{
    public class DecimalTextFormat : ITextFormat
    {
        public bool IsValid(Value value)
        {
            return value is DecimalValue;
        }

        public Type GetValueType()
        {
            return typeof(DecimalValue);
        }

        public bool TryParse(string s, string startTime, out object value)
        {
            decimal result = 0;
            value = null;
            if (!decimal.TryParse(s, out result))
            {
                return false;
            }
            value = result;
            return true;
        }
    }
}
