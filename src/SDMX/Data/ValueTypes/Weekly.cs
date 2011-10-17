using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Text.RegularExpressions;

namespace SDMX
{
    public struct Weekly : IEquatable<Weekly>
    {
        int _year;
        Week _week;

        public int Year { get { return _year; } }
        public Week Week { get { return _week; } }

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

        public override int GetHashCode()
        {
            return _year.HashWith(_week);
        }

        public override bool Equals(object other)
        {      
            if (!(other is Weekly)) return false;
            return Equals((Weekly)other);
        }

        public bool Equals(Weekly other)
        {
            return _year == other._year && _week == other._week;
        }

        public static bool operator ==(Weekly x, Weekly y)
        {
            return Extensions.Equals(x, y);
        }

        public static bool operator !=(Weekly x, Weekly y)
        {
            return !(x == y);
        }
    }
}
