using System;
using System.Text.RegularExpressions;

namespace SDMX.Parsers
{
    internal class DateTimeValueConverter : ITimePeriodConverter
    {
        private const string p = @"^(?<Sign>[-|+]?)(?<Year>\d{4})-(?<Month>\d{1,2})-(?<Day>\d{1,2})T(?<Hour>\d{2}):(?<Minute>\d{2}):(?<Second>\d{2})(?<Ticks>(?:\.\d+)?)(?<Z>Z)?(?:(?<ZoneSign>[+-])(?<ZoneHour>\d{2}):(?<ZoneMinute>\d{2}))?$";
        static Regex pattern = new Regex(p, RegexOptions.Compiled);

        public object Parse(string str, string startTime)
        {
            var match = pattern.Match(str);
            if (!match.Success)
            {
                throw new SDMXException("Invalid date value '{0}'.", str);
            }
            int year = int.Parse(match.Groups["Year"].Value);
            int month = int.Parse(match.Groups["Month"].Value);
            int day = int.Parse(match.Groups["Day"].Value);
            int hour = int.Parse(match.Groups["Hour"].Value);
            int minute = int.Parse(match.Groups["Minute"].Value);
            int second = int.Parse(match.Groups["Second"].Value);

            string ticks = match.Groups["Ticks"].Value;
            int millisecond = 0;
            if (ticks != "")
                millisecond = int.Parse(ticks.Substring(1));
            TimeSpan offset = TimePeriodUtility.ParseTimeOffset(match);
            return new DateTimeValue(new DateTimeOffset(year, month, day, hour, minute, second, millisecond, offset));
        }

        public bool IsValid(string str)
        {
            return pattern.IsMatch(str);
        }

        public string Serialize(object obj, out string startTime)
        {
            if (!(obj is DateTimeValue))
                throw new SDMXException("Cannot serialize object of type: {0}.", obj.GetType());

            startTime = null;
            return ((DateTimeValue)obj).ToString();
        }
    }
}
