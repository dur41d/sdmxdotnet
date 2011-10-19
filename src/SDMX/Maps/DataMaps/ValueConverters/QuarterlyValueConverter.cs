using System;
using System.Text.RegularExpressions;
using SDMX.Parsers;
using OXM;

namespace SDMX.Parsers
{
    internal class QuarterlyValueConverter : SimpleTypeConverter<Quarterly>
    {
        public override string ToXml(Quarterly value)
        {
            return value.ToString();
        }

        public override Quarterly ToObj(string value)
        {
            return Quarterly.Parse(value);
        }

        public override bool CanConvertToObj(string value)
        {
            Quarterly result;
            return Quarterly.TryParse(value, out result);
        }
    }
}
