using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Common;

namespace SDMX
{
    public class DateTextFormat : ITimePeriodTextFormat
    {
        public bool IsValid(Value value)
        {
            return value is DateValue;
        }

        public Type GetValueType()
        {
            return typeof(DateValue);
        }
    }
}
