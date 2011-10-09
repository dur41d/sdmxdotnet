//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using OXM;
//using System.Text.RegularExpressions;
//using Common;

//namespace SDMX.Parsers
//{
//    internal class YearMonthValueConverter : ITimePeriodConverter
//    {
//        const string p = @"^(?<Sign>[-|+]?)(?<Year>\d{4})-(?<Month>\d{2})(?<Z>Z)?(?:(?<ZoneSign>[+-])(?<ZoneHour>\d{2}):(?<ZoneMinute>\d{2}))?$";
//        static Regex pattern = new Regex(p, RegexOptions.Compiled);

//        public Value Parse(string str, string startTime)
//        {
//            var match = pattern.Match(str);
//            if (!match.Success)
//            {
//                throw new SDMXException("Invalid year month value '{0}'.", str);
//            }
//            int year = int.Parse(match.Groups["Year"].Value);
//            int month = int.Parse(match.Groups["Month"].Value);
//            TimeSpan offset = TimePeriodUtility.ParseTimeOffset(match);
//            return new YearMonthValue(new DateTimeOffset(year, month, 1, 1, 1, 1, offset));
//        }

//        public bool IsValid(string str)
//        {
//            return pattern.IsMatch(str);
//        }

//        public string Serialize(Value value, out string startTime)
//        {
//            startTime = null;
//            var yearMonth = (YearMonthValue)value;
//            return yearMonth.ToString();
//        }
//    }
//}
