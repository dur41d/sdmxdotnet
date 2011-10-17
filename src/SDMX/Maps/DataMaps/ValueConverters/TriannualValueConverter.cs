using System;
using System.Text.RegularExpressions;
using SDMX.Parsers;
using OXM;

namespace SDMX.Parsers
{
    internal class TriannaulValueConverter : SimpleTypeConverter<Triannual>
    {
        private const string p = @"^(?<Year>\d{4})-T(?<Triannum>[1-3])$";
        static Regex pattern = new Regex(p, RegexOptions.Compiled);

        public override string ToXml(Triannual value)
        {
            return value.ToString();
        }

        public override Triannual ToObj(string value)
        {
            var match = pattern.Match(value);
            if (!match.Success)
            {
                throw new SDMXException("Invalid date value '{0}'.", value);
            }
            int year = int.Parse(match.Groups["Year"].Value);
            int triannum = int.Parse(match.Groups["Triannum"].Value);
            return new Triannual(year, (Triannum)triannum);
        }

        public override bool CanConvertToObj(string value)
        {
            return pattern.IsMatch(value);
        }
    }
}
