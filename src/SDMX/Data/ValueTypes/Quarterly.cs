using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Text.RegularExpressions;

namespace SDMX
{
    public struct Quarterly : IEquatable<Quarterly>
    {
        int _year;
        Quarter _quarter;

        public int Year { get { return _year; } }
        public Quarter Quarter { get { return _quarter; } }

        public Quarterly(int year, Quarter quarter)
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

        public override int GetHashCode()
        {
            return _year.HashWith(_quarter);
        }

        public override bool Equals(object other)
        {
            if (!(other is Quarterly)) return false;
            return Equals((Quarterly)other);
        }

        public bool Equals(Quarterly other)
        {
            return _year == other._year && _quarter == other._quarter;
        }

        public static bool operator ==(Quarterly x, Quarterly y)
        {
            return Extensions.Equals(x, y);
        }

        public static bool operator !=(Quarterly x, Quarterly y)
        {
            return !(x == y);
        }
    }
}
