using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Common;
using SDMX.Parsers;

namespace SDMX
{
    public class YearTextFormat : ITimePeriodTextFormat
    {
        const string p = @"^(?<Sign>[-|+]?)(?<Year>\d{4})(?<Z>Z)?(?:(?<ZoneSign>[+-])(?<ZoneHour>\d{2}):(?<ZoneMinute>\d{2}))?$";
        static Regex pattern = new Regex(p, RegexOptions.Compiled);

        public bool IsValid(Value value)
        {
            return value is YearValue;
        }

        public Type GetValueType()
        {
            return typeof(YearValue);
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
            TimeSpan offset = TimePeriodUtility.ParseTimeOffset(match);
            value = new DateTimeOffset(year, 1, 1, 0, 0, 0, offset);
            return true;
        }

        //public string Serialize(Value value, out string startTime)
        //{
        //    startTime = null;
        //    return value.ToString();
        //}

        //public bool IsValid(string str)
        //{
        //    return pattern.IsMatch(str);
        //}
    }
}
