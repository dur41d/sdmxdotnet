using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Text.RegularExpressions;

namespace SDMX
{
    public class YearMonthValue : TimePeriod, IEquatable<YearMonthValue>
    {
        DateTimeOffset _value;
        string _toString;

        public int Year
        {
            get { return _value.Year; }
        }

        public int Month
        {
            get { return _value.Month; }
        }

        public TimeSpan Offset
        {
            get { return _value.Offset; }
        }

        public YearMonthValue(int year, int month)
        {
            _value = new DateTimeOffset(year, month, 1, 2, 1, 1, new TimeSpan());
        }

        public YearMonthValue(DateTimeOffset value)
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

        #region IEquatable<YearMonthValue> Members

        public override bool Equals(object obj)
        {
            return Equals(obj as YearMonthValue);
        }

        public bool Equals(YearMonthValue other)
        {
            return this.Equals(other, () => _value.Year == other.Year && _value.Month == other.Month);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public static bool operator ==(YearMonthValue x, object y)
        {
            return object.Equals(x, y);
        }

        public static bool operator !=(YearMonthValue x, object y)
        {
            return !object.Equals(x, y);
        }

        #endregion
    }
}
