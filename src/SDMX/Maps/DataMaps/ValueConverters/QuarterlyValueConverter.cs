using System;
using System.Text.RegularExpressions;
using SDMX.Parsers;
using OXM;

namespace SDMX.Parsers
{
    internal class QuarterlyValueConverter : SimpleTypeConverter<Quarterly>
    {
        private const string p = @"^(?<Year>\d{4})-Q(?<Quarter>[1-4])$";
        static Regex pattern = new Regex(p, RegexOptions.Compiled);

        public override string ToXml(Quarterly value)
        {
            return value.ToString();
        }

        public override Quarterly ToObj(string value)
        {
            var match = pattern.Match(value);
            if (!match.Success)
            {
                throw new SDMXException("Invalid date value '{0}'.", value);
            }
            int year = int.Parse(match.Groups["Year"].Value);
            int quarter = int.Parse(match.Groups["Quarter"].Value);
            return new Quarterly(year, (Quarter)quarter);
        }

        public override bool CanConvertToObj(string value)
        {
            return pattern.IsMatch(value);
        }
    }
}
