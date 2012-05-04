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

        public override string ToXml(DateTimeOffset value)
        {            
            return value.Offset.Ticks == 0 ? 
                value.ToString("yyyy") : value.ToString("yyyyK");
        }

        public override DateTimeOffset ToObj(string value)
        {
            var match = pattern.Match(value);
            if (!match.Success)
            {
                throw new ParseException("Invalid year value '{0}'.", value);
            }
            int year = int.Parse(match.Groups["Year"].Value);
            TimeSpan offset = DateTimeConverter.ParseTimeOffset(match);
            return new DateTimeOffset(year, 1, 1, 0, 0, 0, offset);
        }

        public override bool CanConvertToObj(string value)
        {
            return pattern.IsMatch(value);
        }
    }

    public class NullableYearConverter : NullabeConverter<DateTimeOffset>
    {
        protected override SimpleTypeConverter<DateTimeOffset> Converter
        {
            get
            {
                return new YearConverter();
            }
        }
    }
}
