using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Text.RegularExpressions;

namespace SDMX
{
    public struct Triannual : IEquatable<Triannual>
    {
        private const string p = @"^(?<Year>\d{4})-T(?<Triannum>[1-3])$";
        static Regex pattern = new Regex(p, RegexOptions.Compiled);

        public static readonly Triannual MinValue = new Triannual(DateTime.MinValue.Year, Triannum.T1);
        public static readonly Triannual MaxValue = new Triannual(DateTime.MaxValue.Year, Triannum.T3);

        int _year;
        Triannum _annum;

        public int Year { get { return _year; } }
        public Triannum Annum { get { return _annum; } }

        public Triannual(int year, Triannum annum)
        {
            Validate(year, annum);
            _year = year;
            _annum = annum;
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}", _year, _annum);
        }

        public override int GetHashCode()
        {
            return _year.HashWith(_annum);
        }

        public override bool Equals(object other)
        {
            if (!(other is Triannual)) return false;
            return Equals((Triannual)other);
        }

        public bool Equals(Triannual other)
        {
            return _year == other._year && _annum == other._annum;
        }

        public static bool operator ==(Triannual x, Triannual y)
        {
            return Extensions.Equals(x, y);
        }

        public static bool operator !=(Triannual x, Triannual y)
        {
            return !(x == y);
        }

        public static Triannual Parse(string value)
        {
            Triannual result;
            if (!TryParse(value, out result))
            {
                throw new SDMXException("Invalid Triannual value '{0}'.", value);
            }
            return result;
        }

        public static bool TryParse(string value, out Triannual result)
        {
            var match = pattern.Match(value);
            if (!match.Success)
            {
                result = new Triannual();
                return false;
            }
            int year = int.Parse(match.Groups["Year"].Value);
            int triannum = int.Parse(match.Groups["Triannum"].Value);
            result = new Triannual(year, (Triannum)triannum);
            return true;
        }

        static void Validate(int year, Triannum triannum)
        {
            if (year < DateTime.MinValue.Year || year > DateTime.MaxValue.Year)
                throw new SDMXException("Year value is out of range: {0}.", year);

            int q = (int)triannum;
            if (q < 1 || q > 3)
                throw new SDMXException("Triannum value is out of range: {0}.", q);
        }
    }
}
