using System;
using System.Text.RegularExpressions;

namespace SDMX.Parsers
{
    internal class YearValueConverter : ITimePeriodConverter
    {
        const string p = @"^(?<Sign>[-|+]?)(?<Year>\d{4})(?<Z>Z)?(?:(?<ZoneSign>[+-])(?<ZoneHour>\d{2}):(?<ZoneMinute>\d{2}))?$";
        static Regex pattern = new Regex(p, RegexOptions.Compiled);

        public object Parse(string str, string startTime)
        {
            var match = pattern.Match(str);
            if (!match.Success)
            {
                throw new SDMXException("Invalid year value '{0}'.", str);
            }
            int year = int.Parse(match.Groups["Year"].Value);
            TimeSpan offset = TimePeriodUtility.ParseTimeOffset(match);
            return new YearValue(new DateTimeOffset(year, 1, 1, 0, 0, 0, offset));
        }

        public string Serialize(object obj, out string startTime)
        {
            if (!(obj is YearValue))
                throw new SDMXException("Cannot serialize object of type: {0}.", obj.GetType());

            startTime = null;
            return ((YearValue)obj).ToString();
        }

        public bool IsValid(string str)
        {
            return pattern.IsMatch(str);
        }
    }
}
