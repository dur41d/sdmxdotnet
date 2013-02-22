using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace OXM
{
    public class YearMonthConverter : SimpleTypeConverter<DateTimeOffset>
    {
        const string p = @"^(?<Sign>[-|+]?)(?<Year>\d{4})-(?<Month>\d{1,2})(?<Z>Z)?(?:(?<ZoneSign>[+-])(?<ZoneHour>\d{2}):(?<ZoneMinute>\d{2}))?$";
        static Regex pattern = new Regex(p, RegexOptions.Compiled);

        public override bool TrySerialize(DateTimeOffset value, out string s)
        {
            s = value.Offset.Ticks == 0 ?
                value.ToString("yyyy-MM") : value.ToString("yyyy-MMK");
            return true;
        }

        public override bool TryParse(string value, out DateTimeOffset obj)
        {
            var match = pattern.Match(value);
            if (!match.Success)
            {
                obj = new DateTimeOffset();
                return false;
            }
            int year = int.Parse(match.Groups["Year"].Value);
            int month = int.Parse(match.Groups["Month"].Value);
            TimeSpan offset = DateTimeConverter.ParseTimeOffset(match);
            obj = new DateTimeOffset(year, month, 1, 0, 0, 0, offset);
            return true;
        }
    }

    public class NullableYearMonthConverter : NullabeConverter<DateTimeOffset>
    {
        YearMonthConverter _converter = new YearMonthConverter();
        protected override SimpleTypeConverter<DateTimeOffset> Converter
        {
            get
            {
                return new YearMonthConverter();
            }
        }
    }
}
