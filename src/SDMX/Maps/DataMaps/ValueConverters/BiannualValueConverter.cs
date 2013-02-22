using System;
using System.Text.RegularExpressions;
using SDMX.Parsers;
using OXM;

namespace SDMX.Parsers
{
    internal class BiannualValueConverter : SimpleTypeConverter<Biannual>
    {
        public override bool TrySerialize(Biannual obj, out string s)
        {
            s = obj.ToString();
            return true;
        }


        public override bool TryParse(string s, out Biannual obj)
        {
            return Biannual.TryParse(s, out obj);
        }
    }
}
