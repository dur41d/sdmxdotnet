//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using OXM;
//using System.Text.RegularExpressions;
//using Common;

//namespace SDMX.Parsers
//{
//    internal class DateValueConverter : ITimePeriodConverter
//    {
//        private const string p = @"^(?<Sign>[-|+]?)(?<Year>\d{4})-(?<Month>\d{2})-(?<Day>\d{2})(?<Z>Z)?(?:(?<ZoneSign>[+-])(?<ZoneHour>\d{2}):(?<ZoneMinute>\d{2}))?$";
//        static Regex pattern = new Regex(p, RegexOptions.Compiled);

//        public Value Parse(string str, string startTime)
//        {
//            var match = pattern.Match(str);
//            if (!match.Success)
//            {
//                throw new SDMXException("Invalid date value '{0}'.", str);
//            }
//            int year = int.Parse(match.Groups["Year"].Value);
//            int month = int.Parse(match.Groups["Month"].Value);
//            int day = int.Parse(match.Groups["Day"].Value);
//            TimeSpan offset = TimePeriodUtility.ParseTimeOffset(match);
//            return new DateValue(new DateTimeOffset(year, month, day, 0, 0, 0, 0, offset));
//        }

//        public bool IsValid(string str)
//        {
//            return pattern.IsMatch(str);
//        }

//        public string Serialize(Value value, out string startTime)
//        {
//            startTime = null;
//            return value.ToString();
//        }
//    }
//}
