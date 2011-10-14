using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Text.RegularExpressions;

namespace SDMX
{
    public class YearMonth : TimePeriod, IEquatable<YearMonth>
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

        public YearMonth(int year, int month)
        {
            _value = new DateTimeOffset(year, month, 1, 0, 0, 0, new TimeSpan());
        }

        public YearMonth(DateTimeOffset value)
        {
            _value = value;
        }

        public override string ToString()
        {
            if (_toString == null)
            {
                if (_value.Offset.Ticks == 0)
                    _toString = _value.ToString("yyyy-MM");
                else
                    _toString = _value.ToString("yyyy-MMK");
            }
            return _toString;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as YearMonth);
        }

        public override bool Equals(TimePeriod other)
        {
            return Equals(other as YearMonth);
        }

        public bool Equals(YearMonth other)
        {
            return this.Equals(other, () => _value.Year == other.Year && _value.Month == other.Month);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public static implicit operator DateTimeOffset(YearMonth input)
        {
            Contract.AssertNotNull(input, "input");
            return input._value;
        }

        public static implicit operator YearMonth(DateTimeOffset input)
        {
            return new YearMonth(input);
        }

        public static bool operator ==(YearMonth x, YearMonth y)
        {
            return Extensions.Equals(x, y);
        }

        public static bool operator !=(YearMonth x, YearMonth y)
        {
            return !(x == y);
        }
    }
}
