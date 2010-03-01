using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Text.RegularExpressions;

namespace SDMX
{
    public class BiannualValue : TimePeriod, IEquatable<BiannualValue>
    {
        int _year;
        Biannum _annum;

        public int Year
        {
            get { return _year; }
        }

        public Biannum Annum
        {
            get { return _annum; }
        }

        public BiannualValue(int year, Biannum annum)
        {
            // use date time to validate the integer
            var dateTime = new DateTime(year, 1, 1);
            _year = dateTime.Year;
            _annum = annum;
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}", _year, _annum);
        }

        #region IEquatable<BiannualValue> Members

        public override int GetHashCode()
        {
            return _year.HashWith(_annum);
        }

        public override bool Equals(object other)
        {
            return Equals(other as BiannualValue);
        }

        public bool Equals(BiannualValue other)
        {
            return this.Equals(other, () => _year.Equals(other._year) && _annum.Equals(other._annum));
        }

        public static bool operator ==(Value x, BiannualValue y)
        {
            return object.Equals(x, y);
        }

        public static bool operator !=(Value x, BiannualValue y)
        {
            return !object.Equals(x, y);
        }
        #endregion
    }
}
