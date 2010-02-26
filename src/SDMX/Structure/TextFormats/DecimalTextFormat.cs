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
    }
}
