using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using System.Text.RegularExpressions;
using Common;

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
            return new DateTimeOffset(year, 1, 1, 0, 0, 0, offset);
        }

        public string Serialize(object obj, out string startTime)
        {
            startTime = null;
            var value = (DateTimeOffset)obj;
            string result = null;

            if (value.Offset.Ticks == 0)
                result = value.ToString("yyyy");
            else
                result = value.ToString("yyyyK");

            return result;
        }

        public bool IsValid(string str)
        {
            return pattern.IsMatch(str);
        }
    }
}
