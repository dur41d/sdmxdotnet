using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Text.RegularExpressions;

namespace SDMX
{
    public class YearValue : TimePeriod, IEquatable<YearValue>
    {
        DateTimeOffset _value;
        private string _toString;

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

        public YearValue(DateTimeOffset dateTime)
        {
            _value = dateTime;
        }

        public YearValue(int year)
        {
            _value = new DateTimeOffset(year, 1, 1, 0, 0, 0, new TimeSpan());
        }

        public override string ToString()
        {
            if (_toString == null)
            {
                if (_value.Offset.Ticks == 0)
                    _toString = _value.ToString("yyyy");
                else
                    _toString = _value.ToString("yyyyK");
            }
            return _toString;
        }

        public static implicit operator DateTimeOffset(YearValue input)
        {
            Contract.AssertNotNull(input, "input");
            return input._value;
        }

        public static implicit operator YearValue(DateTimeOffset input)
        {
            return new YearValue(input);
        }

        public static explicit operator YearValue(int input)
        {
            return new YearValue(input);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public override bool Equals(object other)
        {
            return Equals(other as YearValue);
        }

        public override bool Equals(TimePeriod other)
        {
            return Equals(other as YearValue);
        }

        public bool Equals(YearValue other)
        {
            return this.Equals(other, () => _value.Equals(other._value));
        }

        public static bool operator ==(YearValue x, object y)
        {
            return Extensions.Equals(x, y);
        }

        public static bool operator !=(YearValue x, object y)
        {
            return !(x == y);
        }
    }
}
