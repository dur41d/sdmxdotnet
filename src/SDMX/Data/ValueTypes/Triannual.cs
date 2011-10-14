using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Text.RegularExpressions;

namespace SDMX
{
    public class Triannual : TimePeriod, IEquatable<Triannual>
    {
        int _year;
        Triannum _annum;

        public int Year { get { return _year; } }
        public Triannum Annum { get { return _annum; } }

        public Triannual(int year, Triannum annum)
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

        #region IEquatable<TriannualValue> Members

        public override int GetHashCode()
        {
            return _year.HashWith(_annum);
        }

        public override bool Equals(object other)
        {
            return Equals(other as Triannual);
        }

        public override bool Equals(TimePeriod other)
        {
            return Equals(other as Triannual);
        }

        public bool Equals(Triannual other)
        {
            return this.Equals(other, () => _year.Equals(other._year) && _annum.Equals(other._annum));
        }

        public static bool operator ==(Triannual x, Triannual y)
        {
            return Extensions.Equals(x, y);
        }

        public static bool operator !=(Triannual x, Triannual y)
        {
            return !(x == y);
        }

        #endregion
    }
}
