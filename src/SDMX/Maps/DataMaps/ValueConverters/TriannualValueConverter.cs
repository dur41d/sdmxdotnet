using System;
using System.Text.RegularExpressions;
using SDMX.Parsers;
using OXM;

namespace SDMX.Parsers
{
    internal class TriannaulValueConverter : SimpleTypeConverter<Triannual>
    {
        public override bool TrySerialize(Triannual obj, out string s)
        {
            s = obj.ToString();
            return true;
        }

        public override bool TryParse(string s, out Triannual obj)
        {
            return Triannual.TryParse(s, out obj);
        }
    }
}
