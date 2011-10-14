using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Text.RegularExpressions;

namespace SDMX
{
    public class Date : TimePeriod, IEquatable<Date>
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

        public Date(DateTimeOffset dateTime)
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

        public static implicit operator DateTimeOffset(Date input)
        {
            Contract.AssertNotNull(input, "input");
            return input._value;
        }

        public static implicit operator Date(DateTimeOffset input)
        {
            return new Date(input);
        }

        public override bool Equals(object other)
        {
            return Equals(other as Date);
        }

        public override bool Equals(TimePeriod other)
        {
            return Equals(other as Date);
        }

        public bool Equals(Date other)
        {
            return this.Equals(other, () => _value.Equals(other._value));
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}
