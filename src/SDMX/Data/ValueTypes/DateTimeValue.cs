using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Text.RegularExpressions;

namespace SDMX
{
    /// <summary>
    /// The SDMX equivalent of DateTimeOffset.
    /// </summary>
    /// <remarks>
    /// The class is created because SDMX definition of time different than the .NET one.
    /// The TimePeriod class is the base class of all the time values in SDMX.
    /// </remarks>
    public class DateTimeValue : TimePeriod, IEquatable<DateTimeValue>
    {
        DateTimeOffset _value;
        string _toString;

        public override int Year { get { return _value.Year; } }
        public override int Month { get { return _value.Month; } }
        public override int Day { get { return _value.Day; } }
        public override int Hour { get { return _value.Hour; } }
        public override int Minute { get { return _value.Minute; } }
        public override int Second { get { return _value.Second; } }
        public override int Millisecond { get { return _value.Millisecond; } }
        public override TimeSpan Offset { get { return _value.Offset; } }

        public DateTime DateTime { get { return _value.DateTime; } }
        public DateTimeOffset DateTimeOffset { get { return _value; } }

        public DateTimeValue(DateTimeOffset dateTime)
        {
            _value = dateTime;
        }

        public static DateTimeValue Parse(string input)
        {
            return new DateTimeValue(DateTimeOffset.Parse(input));
        }

        public static implicit operator DateTimeOffset(DateTimeValue input)
        {
            Contract.AssertNotNull(input, "input");
            return input._value;
        }

        public static implicit operator DateTimeValue(DateTimeOffset input)
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

        public override bool Equals(object other)
        {
            return Equals(other as DateTimeValue);
        }

        public override bool Equals(TimePeriod other)
        {
            return Equals(other as DateTimeValue);
        }

        public bool Equals(DateTimeValue other)
        {
            return this.Equals(other, () => _value.Equals(other._value));
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}
