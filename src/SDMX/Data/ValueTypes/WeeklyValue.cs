using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Text.RegularExpressions;

namespace SDMX
{
    public class WeeklyValue : TimePeriod, IEquatable<WeeklyValue>, IYearValue
    {
        int _year;
        Week _week;

        public int Year
        {
            get { return _year; }
        }

        public Week Week
        {
            get { return _week; }
        }

        public WeeklyValue(int year, Week week)
        {
            // use date time to validate the integer
            var dateTime = new DateTime(year, 1, 1);
            _year = dateTime.Year;
            _week = week;
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}", _year, _week);
        }

        #region IEquatable<WeeklyValue> Members

        public override int GetHashCode()
        {
            return _year.HashWith(_week);
        }

        public override bool Equals(object other)
        {
            return Equals(other as WeeklyValue);
        }

        public bool Equals(WeeklyValue other)
        {
            return this.Equals(other, () => _year.Equals(other._year) && _week.Equals(other._week));
        }

        public static bool operator ==(WeeklyValue x, WeeklyValue y)
        {
            return Extensions.Equals(x, y);
        }

        public static bool operator !=(WeeklyValue x, WeeklyValue y)
        {
            return !(x == y);
        }

        #endregion
    }
}
