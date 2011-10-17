using System;
using System.Text.RegularExpressions;
using SDMX.Parsers;
using OXM;

namespace SDMX.Parsers
{
    internal class WeeklyValueConverter : SimpleTypeConverter<Weekly>
    {
        private const string p = @"^(?<Year>\d{4})-W(?<Week>[1-9]|[1-4][0-9]|5[0-2])$";
        static Regex pattern = new Regex(p, RegexOptions.Compiled);

        public override string ToXml(Weekly value)
        {
            return value.ToString();
        }

        public override Weekly ToObj(string value)
        {
            var match = pattern.Match(value);
            if (!match.Success)
            {
                throw new SDMXException("Invalid date value '{0}'.", value);
            }
            int year = int.Parse(match.Groups["Year"].Value);
            int week = int.Parse(match.Groups["Week"].Value);
            return new Weekly(year, (Week)week);
        }

        public override bool CanConvertToObj(string value)
        {
            return pattern.IsMatch(value);
        }
    }
}
