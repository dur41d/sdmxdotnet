using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Common;
using SDMX.Parsers;

namespace SDMX
{
    public class DateTextFormat : ITimePeriodTextFormat
    {
        const string p = @"^(?<Sign>[-|+]?)(?<Year>\d{4})-(?<Month>\d{2})-(?<Day>\d{2})(?<Z>Z)?(?:(?<ZoneSign>[+-])(?<ZoneHour>\d{2}):(?<ZoneMinute>\d{2}))?$";
        static Regex pattern = new Regex(p, RegexOptions.Compiled);

        public bool IsValid(Value value)
        {
            return value is DateValue;
        }

        public Type GetValueType()
        {
            return typeof(DateValue);
        }

        public bool TryParse(string s, string startTime, out object value)
        {
            value = null;
            var match = pattern.Match(s);
            if (!match.Success)
            {
                return false;
            }
            int year = int.Parse(match.Groups["Year"].Value);
            int month = int.Parse(match.Groups["Month"].Value);
            int day = int.Parse(match.Groups["Day"].Value);
            TimeSpan offset = TimePeriodUtility.ParseTimeOffset(match);
            value = new DateTimeOffset(year, month, day, 0, 0, 0, 0, offset);
            return true;
        }

        public bool IsValid(string str)
        {
            return pattern.IsMatch(str);
        }

        //public string Serialize(object value, out string startTime)
        //{
        //    startTime = null;
        //    return value.ToString();
        //}
    }
}
