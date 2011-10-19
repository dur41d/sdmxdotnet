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
        private const string p = @"^(?<Year>\d{4})-W(?<Week>[1-9]|[1-4][0-9]|5[0-2])$";
        static Regex pattern = new Regex(p, RegexOptions.Compiled);

        public static readonly Weekly MinValue = new Weekly(DateTime.MinValue.Year, Week.W1);
        public static readonly Weekly MaxValue = new Weekly(DateTime.MaxValue.Year, Week.W52);

        int _year;
        Week _week;

        public int Year { get { return _year; } }
        public Week Week { get { return _week; } }

        public Weekly(int year, Week week)
        {
            Validate(year, week);
            _year = year;
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

        public static Weekly Parse(string value)
        {
            Weekly result;
            if (!TryParse(value, out result))
            {
                throw new SDMXException("Invalid Weekly value '{0}'.", value);
            }
            return result;
        }

        public static bool TryParse(string value, out Weekly result)
        {
            var match = pattern.Match(value);
            if (!match.Success)
            {
                result = new Weekly();
                return false;
            }
            int year = int.Parse(match.Groups["Year"].Value);
            int week = int.Parse(match.Groups["Week"].Value);
            result = new Weekly(year, (Week)week);
            return true;
        }

        static void Validate(int year, Week week)
        {
            if (year < DateTime.MinValue.Year || year > DateTime.MaxValue.Year)
                throw new SDMXException("Year value is out of range: {0}.", year);

            int q = (int)week;
            if (q < 1 || q > 52)
                throw new SDMXException("Week value is out of range: {0}.", q);
        }
    }
}
