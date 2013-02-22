using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Text.RegularExpressions;

namespace SDMX
{
    public struct Biannual : IEquatable<Biannual>
    {
        private const string p = @"^(?<Year>\d{4})-B(?<Biannum>[1-2])$";
        static Regex pattern = new Regex(p, RegexOptions.Compiled);

        public static readonly Biannual MinValue = new Biannual(DateTime.MinValue.Year, Biannum.B1);
        public static readonly Biannual MaxValue = new Biannual(DateTime.MaxValue.Year, Biannum.B2);

        int _year;
        Biannum _annum;

        public int Year { get { return _year; } }
        public Biannum Annum { get { return _annum; } }

        public Biannual(int year, Biannum annum)
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
            if (!(other is Biannual)) return false;
            return Equals((Biannual)other);
        }

        public bool Equals(Biannual other)
        {
            return _year == other._year && _annum == other._annum;
        }

        public static bool operator ==(Biannual x, Biannual y)
        {
            return Extensions.Equals(x, y);
        }

        public static bool operator !=(Biannual x, Biannual y)
        {
            return !(x == y);
        }

        public static Biannual Parse(string value)
        {
            Biannual result;
            if (!TryParse(value, out result))
            {
                throw new SDMXException("Invalid Biannual value '{0}'.", value);
            }
            return result;
        }

        public static bool TryParse(string value, out Biannual result)
        {
            var match = pattern.Match(value);
            if (!match.Success)
            {
                result = new Biannual();
                return false;
            }
            int year = int.Parse(match.Groups["Year"].Value);
            int biannum = int.Parse(match.Groups["Biannum"].Value);
            result = new Biannual(year, (Biannum)biannum);
            return true;
        }

        static void Validate(int year, Biannum biannum)
        {
            if (year < DateTime.MinValue.Year || year > DateTime.MaxValue.Year)
                throw new SDMXException("Year value is out of range: {0}.", year);

            int q = (int)biannum;
            if (q < 1 || q > 2)
                throw new SDMXException("Biannum value is out of range: {0}.", q);
        }

    }
}
