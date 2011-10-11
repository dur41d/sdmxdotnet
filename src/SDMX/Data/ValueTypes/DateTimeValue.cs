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
        string _toString;

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
            if (_toString == null)
            {
                if (_value.Offset.Ticks == 0)
                    _toString = _value.ToString("yyyy-MM-ddTHH:mm:ss.FFFFFFF");
                else
                    _toString = _value.ToString("yyyy-MM-ddTHH:mm:ss.FFFFFFFK");
            }
            return _toString;
        }
    }
}
