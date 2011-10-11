using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using System.Text.RegularExpressions;
using Common;

namespace SDMX.Parsers
{
    internal class DateTimeValueConverter : ITimePeriodConverter
    {
        private const string p = @"^(?<Sign>[-|+]?)(?<Year>\d{4})-(?<Month>\d{2})-(?<Day>\d{2})T(?<Hour>\d{2}):(?<Minute>\d{2}):(?<Second>\d{2})(?<Ticks>(?:\.\d+)?)(?<Z>Z)?(?:(?<ZoneSign>[+-])(?<ZoneHour>\d{2}):(?<ZoneMinute>\d{2}))?$";
        static Regex pattern = new Regex(p, RegexOptions.Compiled);

        public object Parse(string str, string startTime)
        {
            return DateTimeOffset.Parse(str);
        }

        public bool IsValid(string str)
        {
            return pattern.IsMatch(str);
        }

        public string Serialize(object obj, out string startTime)
        {
            startTime = null;
            var value = (DateTimeOffset)obj;
            return value.ToString("yyyy-MM-ddThh:mm:ss.FFFFFFFK");
        }
    }
}
