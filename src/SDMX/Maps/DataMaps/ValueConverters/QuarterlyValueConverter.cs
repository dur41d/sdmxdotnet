using System;
using System.Text.RegularExpressions;
using SDMX.Parsers;
using OXM;

namespace SDMX.Parsers
{
    internal class QuarterlyValueConverter : SimpleTypeConverter<Quarterly>
    {
        public override bool TrySerialize(Quarterly obj, out string s)
        {
            s = obj.ToString();
            return true;
        }

        public override bool TryParse(string s, out Quarterly obj)
        {
            return Quarterly.TryParse(s, out obj);
        }
    }
}
