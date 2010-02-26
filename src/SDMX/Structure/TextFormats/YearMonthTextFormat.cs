using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Common;

namespace SDMX
{
    public class YearMonthTextFormat : ITimePeriodTextFormat
    {
        public bool IsValid(Value value)
        {
            return value is YearMonthValue;
        }

        public Type GetValueType()
        {
            return typeof(YearMonthValue);
        }
    }
}
