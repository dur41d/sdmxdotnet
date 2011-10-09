using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Common;
using SDMX.Parsers;

namespace SDMX
{
    public class YearMonthTextFormat : ITimePeriodTextFormat
    {
        const string p = @"^(?<Sign>[-|+]?)(?<Year>\d{4})-(?<Month>\d{2})(?<Z>Z)?(?:(?<ZoneSign>[+-])(?<ZoneHour>\d{2}):(?<ZoneMinute>\d{2}))?$";
        static Regex pattern = new Regex(p, RegexOptions.Compiled);

        public bool IsValid(Value value)
        {
            return value is YearMonthValue;
        }

        public Type GetValueType()
        {
            return typeof(YearMonthValue);
        }

        public bool TryParse(string s, string startTime, out object value)
        {
            var match = pattern.Match(s);
            if (!match.Success)
            {
                value = null;
                return false;
            }
            int year = int.Parse(match.Groups["Year"].Value);
            int month = int.Parse(match.Groups["Month"].Value);
            TimeSpan offset = TimePeriodUtility.ParseTimeOffset(match);
            value = new DateTimeOffset(year, month, 1, 1, 1, 1, offset);
            return true;
        }

        public bool IsValid(string str)
        {
            return pattern.IsMatch(str);
        }

        public string Serialize(Value value, out string startTime)
        {
            startTime = null;
            var yearMonth = (YearMonthValue)value;
            return yearMonth.ToString();
        }
    }
}
