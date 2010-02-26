using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Text.RegularExpressions;

namespace SDMX
{
    public class DateTimeValue : TimePeriod
    {
        DateTimeOffset _time;        

        public DateTimeValue(DateTimeOffset dateTime)
        {
            _time = dateTime;
        }

        public static DateTimeValue Parse(string input)
        {
            return new DateTimeValue(DateTimeOffset.Parse(input));
        }

        public static explicit operator DateTimeOffset(DateTimeValue input)
        {
            Contract.AssertNotNull(input, "input");
            return input._time;
        }

        public static explicit operator DateTimeValue(DateTimeOffset input)
        {
            return new DateTimeValue(input);
        }

        public override string ToString()
        {
            return _time.ToString("yyyy-MM-ddThh:mm:ss.FFFFFFFK");
        }
    }
}
