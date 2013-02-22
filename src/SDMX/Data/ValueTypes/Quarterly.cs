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
        private const string p = @"^(?<Year>\d{4})-Q(?<Quarter>[1-4])$";
        static Regex pattern = new Regex(p, RegexOptions.Compiled);

        public static readonly Quarterly MinValue = new Quarterly(DateTime.MinValue.Year, Quarter.Q1);
        public static readonly Quarterly MaxValue = new Quarterly(DateTime.MaxValue.Year, Quarter.Q4);


        int _year;
        Quarter _quarter;

        public int Year { get { return _year; } }
        public Quarter Quarter { get { return _quarter; } }

        public Quarterly(int year, Quarter quarter)
        {
            Validate(year, quarter);
            _year = year;
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

        public static Quarterly Parse(string value)
        {
            Quarterly result;
            if (!TryParse(value, out result))
            {
                throw new SDMXException("Invalid Quarterly value '{0}'.", value);
            }
            return result;
        }

        public static bool TryParse(string value, out Quarterly result)
        {
            var match = pattern.Match(value);
            if (!match.Success)
            {
                result = new Quarterly();
                return false;
            }
            int year = int.Parse(match.Groups["Year"].Value);
            int quarter = int.Parse(match.Groups["Quarter"].Value);
            result = new Quarterly(year, (Quarter)quarter);
            return true;
        }

        static void Validate(int year, Quarter quarter)
        {
            if (year < DateTime.MinValue.Year || year > DateTime.MaxValue.Year)
                throw new SDMXException("Year value is out of range: {0}.", year);

            int q = (int)quarter;
            if (q < 1 || q > 4)
                throw new SDMXException("Quarter value is out of range: {0}.", q);
        }
    }
}
