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

        public int Year
        {
            get { return _value.Year; }
        }

        public TimeSpan Offset
        {
            get { return _value.Offset; }
        }

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

        public static explicit operator DateTimeOffset(YearValue input)
        {
            Contract.AssertNotNull(input, "input");
            return input._value;
        }

        public static explicit operator YearValue(DateTimeOffset input)
        {
            return new YearValue(input);
        }

        public static explicit operator YearValue(int input)
        {
            return new YearValue(input);
        }

        #region IEquatable<DecimalValue> Members

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public override bool Equals(object other)
        {
            return Equals(other as YearValue);
        }

        public bool Equals(YearValue other)
        {
            return this.Equals(other, () => _value.Equals(other._value));
        }

        public static bool operator ==(YearValue x, object y)
        {
            return object.Equals(x, y);
        }

        public static bool operator !=(YearValue x, object y)
        {
            return !object.Equals(x, y);
        }

        #endregion
    }
}
