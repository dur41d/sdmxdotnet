using System;
using System.Text.RegularExpressions;
using SDMX.Parsers;
using OXM;

namespace SDMX.Parsers
{
    internal class TriannaulValueConverter : SimpleTypeConverter<Triannual>
    {
        public override string ToXml(Triannual value)
        {
            return value.ToString();
        }

        public override Triannual ToObj(string value)
        {
            return Triannual.Parse(value);
        }

        public override bool CanConvertToObj(string value)
        {
            Triannual result;
            return Triannual.TryParse(value, out result);
        }
    }
}
