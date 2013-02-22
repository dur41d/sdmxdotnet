using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace OXM
{
    public class YearConverter : SimpleTypeConverter<DateTimeOffset>
    {
        // TODO: think about serializing to YearOffset and YearMonthOffset instead of DateTimeOffset
        const string p = @"^(?<Sign>[-|+]?)(?<Year>\d{4})(?<Z>Z)?(?:(?<ZoneSign>[+-])(?<ZoneHour>\d{2}):(?<ZoneMinute>\d{2}))?$";
        static Regex pattern = new Regex(p, RegexOptions.Compiled);

        public override bool TrySerialize(DateTimeOffset value, out string s)
        {
            s = value.Offset.Ticks == 0 ?
                value.ToString("yyyy") : value.ToString("yyyyK");
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
            TimeSpan offset = DateTimeConverter.ParseTimeOffset(match);
            obj = new DateTimeOffset(year, 1, 1, 0, 0, 0, offset);
            return true;
        }
    }

    public class NullableYearConverter : NullabeConverter<DateTimeOffset>
    {
        YearConverter _converter = new YearConverter();
        protected override SimpleTypeConverter<DateTimeOffset> Converter
        {
            get
            {
                return new YearConverter();
            }
        }
    }
}
