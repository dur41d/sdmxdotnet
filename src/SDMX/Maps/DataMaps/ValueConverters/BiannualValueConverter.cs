using System;
using System.Text.RegularExpressions;
using SDMX.Parsers;
using OXM;

namespace SDMX.Parsers
{
    internal class BiannualValueConverter : SimpleTypeConverter<Biannual>
    {
        public override string ToXml(Biannual value)
        {
            return value.ToString();
        }

        public override Biannual ToObj(string value)
        {
            return Biannual.Parse(value);
        }

        public override bool CanConvertToObj(string value)
        {
            Biannual result;
            return Biannual.TryParse(value, out result);
        }
    }
}
