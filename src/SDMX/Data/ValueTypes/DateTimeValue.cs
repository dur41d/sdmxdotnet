using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Text.RegularExpressions;

namespace SDMX
{
    public class DateTimeValue : TimePeriod, IYearValue
    {
        DateTimeOffset _value;

        public int Year
        {
            get { return _value.Year; }
        }

        public DateTimeValue(DateTimeOffset dateTime)
        {
            _value = dateTime;
        }

        public static DateTimeValue Parse(string input)
        {
            return new DateTimeValue(DateTimeOffset.Parse(input));
        }

        public static explicit operator DateTimeOffset(DateTimeValue input)
        {
            Contract.AssertNotNull(input, "input");
            return input._value;
        }

        public static explicit operator DateTimeValue(DateTimeOffset input)
        {
            return new DateTimeValue(input);
        }

        public override string ToString()
        {
            return _value.ToString("yyyy-MM-ddThh:mm:ss.FFFFFFFK");
        }
    }
}
