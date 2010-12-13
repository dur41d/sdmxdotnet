using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Text.RegularExpressions;

namespace SDMX
{
    public class DateValue : TimePeriod, IYearValue
    {
        DateTimeOffset _value;
        string _toString;

        public int Year
        {
            get { return _value.Year; }
        }

        public DateValue(DateTimeOffset dateTime)
        {
            _value = new DateTimeOffset(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0, dateTime.Offset);
        }
        public override string ToString()
        {
            if (_toString == null)
            {
                if (_value.Offset.Ticks == 0)
                    _toString = _value.ToString("yyyy-MM-dd");
                else
                    _toString = _value.ToString("yyyy-MM-ddK");
            }
            return _toString;
        }       

        public static explicit operator DateTimeOffset(DateValue input)
        {
            Contract.AssertNotNull(input, "input");
            return input._value;
        }

        public static explicit operator DateValue(DateTimeOffset input)
        {
            return new DateValue(input);
        }
    }
}
