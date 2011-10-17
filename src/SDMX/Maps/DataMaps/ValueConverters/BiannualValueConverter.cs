using System;
using System.Text.RegularExpressions;
using SDMX.Parsers;
using OXM;

namespace SDMX.Parsers
{
    internal class BiannualValueConverter : SimpleTypeConverter<Biannual>
    {
        private const string p = @"^(?<Year>\d{4})-B(?<Biannum>[1-2])$";
        static Regex pattern = new Regex(p, RegexOptions.Compiled);

        public override string ToXml(Biannual value)
        {
            return value.ToString();
        }

        public override Biannual ToObj(string value)
        {
            var match = pattern.Match(value);
            if (!match.Success)
            {
                throw new SDMXException("Invalid date value '{0}'.", value);
            }
            int year = int.Parse(match.Groups["Year"].Value);
            int biannum = int.Parse(match.Groups["Biannum"].Value);
            return new Biannual(year, (Biannum)biannum);
        }

        public override bool CanConvertToObj(string value)
        {
            return pattern.IsMatch(value);
        }
    }
}
