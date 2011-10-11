using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using System.Text.RegularExpressions;
using Common;

namespace SDMX.Parsers
{
    internal class YearMonthValueConverter : ITimePeriodConverter
    {
        const string p = @"^(?<Sign>[-|+]?)(?<Year>\d{4})-(?<Month>\d{2})(?<Z>Z)?(?:(?<ZoneSign>[+-])(?<ZoneHour>\d{2}):(?<ZoneMinute>\d{2}))?$";
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
            return new DateTimeOffset(year, month, 1, 1, 1, 1, offset);
        }

        public bool IsValid(string str)
        {
            return pattern.IsMatch(str);
        }

        public string Serialize(object obj, out string startTime)
        {
            startTime = null;
            var value = (DateTimeOffset)obj;
            string result = null;

            if (value.Offset.Ticks == 0)
                result = value.ToString("yyyy-MM");
            else
                result = value.ToString("yyyy-MMK");

            return result;
        }
    }
}
