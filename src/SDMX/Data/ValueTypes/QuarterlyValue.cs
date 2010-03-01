using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Text.RegularExpressions;

namespace SDMX
{
    public class QuarterlyValue : TimePeriod, IEquatable<QuarterlyValue>
    {
        int _year;
        Quarter _quarter;

        public int Year
        {
            get { return _year; }
        }

        public Quarter Quarter
        {
            get { return _quarter; }
        }

        public QuarterlyValue(int year, Quarter quarter)
        {
            // use date time to validate the integer
            var dateTime = new DateTime(year, 1, 1);
            _year = dateTime.Year;
            _quarter = quarter;
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}", _year, _quarter);
        }

        #region IEquatable<QuarterlyValue> Members

        public override int GetHashCode()
        {
            return _year.HashWith(_quarter);
        }

        public override bool Equals(object other)
        {
            return Equals(other as QuarterlyValue);
        }

        public bool Equals(QuarterlyValue other)
        {
            return this.Equals(other, () => _year.Equals(other._year) && _quarter.Equals(other._quarter));
        }

        public static bool operator ==(Value x, QuarterlyValue y)
        {
            return object.Equals(x, y);
        }

        public static bool operator !=(Value x, QuarterlyValue y)
        {
            return !object.Equals(x, y);
        }

        #endregion
    }
}
