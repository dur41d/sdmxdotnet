using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Text.RegularExpressions;

namespace SDMX
{
    public class Weekly : TimePeriod, IEquatable<Weekly>
    {
        int _year;
        Week _week;

        public override int Year { get { return _year; } }
        public override int Month { get { return 1; } }
        public override int Day { get { return 1; } }
        public override int Hour { get { return 0; } }
        public override int Minute { get { return 0; } }
        public override int Second { get { return 0; } }
        public override int Millisecond { get { return 0; } }
        public override TimeSpan Offset { get { return TimeSpan.FromTicks(0); } }

        public Week Week
        {
            get { return _week; }
        }

        public Weekly(int year, Week week)
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
            return Equals(other as Weekly);
        }

        public override bool Equals(TimePeriod other)
        {
            return Equals(other as Weekly);
        }

        public bool Equals(Weekly other)
        {
            return this.Equals(other, () => _year.Equals(other._year) && _week.Equals(other._week));
        }

        public static bool operator ==(Weekly x, Weekly y)
        {
            return Extensions.Equals(x, y);
        }

        public static bool operator !=(Weekly x, Weekly y)
        {
            return !(x == y);
        }

        #endregion
    }
}
