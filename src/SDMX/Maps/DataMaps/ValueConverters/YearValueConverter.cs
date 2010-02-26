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

        public Value Parse(string str, string startTime)
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

        public string Serialize(Value value, out string startTime)
        {
            startTime = null;
            return value.ToString();
        }

        public bool IsValid(string str)
        {
            return pattern.IsMatch(str);
        }
    }
}
