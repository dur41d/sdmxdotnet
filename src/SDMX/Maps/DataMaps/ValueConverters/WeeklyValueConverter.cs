using System;
using System.Text.RegularExpressions;
using SDMX.Parsers;

namespace SDMX.Parsers
{    
    internal class WeeklyValueConverter : ITimePeriodConverter
    {
        private const string p = @"^(?<Year>\d{4})-W(?<Week>[1-9]|[1-4][0-9]|5[0-2])$";
        static Regex pattern = new Regex(p, RegexOptions.Compiled);

        public object Parse(string str, string startTime)
        {
            var match = pattern.Match(str);
            if (!match.Success)
            {
                throw new SDMXException("Invalid date value '{0}'.", str);
            }
            int year = int.Parse(match.Groups["Year"].Value);
            int week = int.Parse(match.Groups["Week"].Value);
            return new WeeklyValue(year, (Week)week);            
        }

        public bool IsValid(string str)
        {
            return pattern.IsMatch(str);
        }

        public string Serialize(object obj, out string startTime)
        {
            if (!(obj is WeeklyValue))
                throw new SDMXException("Cannot serialize object of type: {0}.", obj.GetType());

            startTime = null;
            return ((WeeklyValue)obj).ToString();
        }
    }
}
