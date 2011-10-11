using System;
using System.Text.RegularExpressions;
using SDMX.Parsers;

namespace SDMX.Parsers
{
    internal class BiannualValueConverter : ITimePeriodConverter
    {
        private const string p = @"^(?<Year>\d{4})-B(?<Biannum>[1-2])$";
        static Regex pattern = new Regex(p, RegexOptions.Compiled);

        public object Parse(string str, string startTime)
        {
            var match = pattern.Match(str);
            if (!match.Success)
            {
                throw new SDMXException("Invalid date value '{0}'.", str);
            }
            int year = int.Parse(match.Groups["Year"].Value);
            int biannum = int.Parse(match.Groups["Biannum"].Value);
            return new BiannualValue(year, (Biannum)biannum);
        }

        public bool IsValid(string str)
        {
            return pattern.IsMatch(str);
        }

        public string Serialize(object obj, out string startTime)
        {
            if (!(obj is BiannualValue))
                throw new SDMXException("Cannot serialize object of type: {0}.", obj.GetType());

            startTime = null;
            return ((BiannualValue)obj).ToString();
        }
    }
}
