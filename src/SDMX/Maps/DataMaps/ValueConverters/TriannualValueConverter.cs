using System;
using System.Text.RegularExpressions;
using SDMX.Parsers;

namespace SDMX.Parsers
{
    internal class TriannaulValueConverter : ITimePeriodConverter
    {
        private const string p = @"^(?<Year>\d{4})-T(?<Triannum>[1-3])$";
        static Regex pattern = new Regex(p, RegexOptions.Compiled);

        public object Parse(string str, string startTime)
        {
            var match = pattern.Match(str);
            if (!match.Success)
            {
                throw new SDMXException("Invalid date value '{0}'.", str);
            }
            int year = int.Parse(match.Groups["Year"].Value);
            int triannum = int.Parse(match.Groups["Triannum"].Value);
            return new Triannual(year, (Triannum)triannum);
        }

        public bool IsValid(string str)
        {
            return pattern.IsMatch(str);
        }

        public string Serialize(object obj, out string startTime)
        {
            if (!(obj is Triannual))
                throw new SDMXException("Cannot serialize object of type: {0}.", obj.GetType());

            startTime = null;
            return ((Triannual)obj).ToString();
        }
    }
}
