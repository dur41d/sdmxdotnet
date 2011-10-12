using System;
using System.Text.RegularExpressions;

namespace SDMX.Parsers
{
    internal class YearMonthValueConverter : ITimePeriodConverter
    {
        const string p = @"^(?<Sign>[-|+]?)(?<Year>\d{4})-(?<Month>\d{1,2})(?<Z>Z)?(?:(?<ZoneSign>[+-])(?<ZoneHour>\d{2}):(?<ZoneMinute>\d{2}))?$";
        static Regex pattern = new Regex(p, RegexOptions.Compiled);

        public object Parse(string str, string startTime)
        {
            var match = pattern.Match(str);
            if (!match.Success)
            {
                throw new SDMXException("Invalid year month value '{0}'.", str);
            }
            int year = int.Parse(match.Groups["Year"].Value);
            int month = int.Parse(match.Groups["Month"].Value);
            TimeSpan offset = TimePeriodUtility.ParseTimeOffset(match);
            return new YearMonth(new DateTimeOffset(year, month, 1, 0, 0, 0, offset));
        }

        public bool IsValid(string str)
        {
            return pattern.IsMatch(str);
        }

        public string Serialize(object obj, out string startTime)
        {
            if (!(obj is YearMonth))
                throw new SDMXException("Cannot serialize object of type: {0}.", obj.GetType());

            startTime = null;
            return ((YearMonth)obj).ToString();
        }
    }
}
