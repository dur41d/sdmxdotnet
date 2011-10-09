using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Common;

namespace SDMX
{
    public class DateTimeTextFormat : ITimePeriodTextFormat
    {
        const string p = @"^(?<Sign>[-|+]?)(?<Year>\d{4})-(?<Month>\d{2})-(?<Day>\d{2})T(?<Hour>\d{2}):(?<Minute>\d{2}):(?<Second>\d{2})(?<Ticks>(?:\.\d+)?)(?<Z>Z)?(?:(?<ZoneSign>[+-])(?<ZoneHour>\d{2}):(?<ZoneMinute>\d{2}))?$";
        static Regex pattern = new Regex(p, RegexOptions.Compiled);

        public bool IsValid(Value value)
        {
            return value is DateTimeValue;
        }

        public Type GetValueType()
        {
            return typeof(DateTimeValue);
        }

        public bool TryParse(string s, string startTime, out object value)
        {
            value = null;
            var match = pattern.Match(s);
            if (!match.Success)
            {
                return false;
            }
            value = DateTimeOffset.Parse(s);
            return true;
        }

        public bool IsValid(string str)
        {
            return pattern.IsMatch(str);
        }

        //public string Serialize(Value value, out string startTime)
        //{
        //    startTime = null;
        //    return value.ToString();
        //}
    }
}
