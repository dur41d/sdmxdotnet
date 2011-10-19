using System;
using System.Text.RegularExpressions;
using SDMX.Parsers;
using OXM;

namespace SDMX.Parsers
{
    internal class WeeklyValueConverter : SimpleTypeConverter<Weekly>
    {
        public override string ToXml(Weekly value)
        {
            return value.ToString();
        }

        public override Weekly ToObj(string value)
        {
            return Weekly.Parse(value);
        }

        public override bool CanConvertToObj(string value)
        {
            Weekly result;
            return Weekly.TryParse(value, out result);
        }
    }
}
