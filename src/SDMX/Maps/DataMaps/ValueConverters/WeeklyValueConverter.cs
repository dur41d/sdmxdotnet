using System;
using System.Text.RegularExpressions;
using SDMX.Parsers;
using OXM;

namespace SDMX.Parsers
{
    internal class WeeklyValueConverter : SimpleTypeConverter<Weekly>
    {
        public override bool TrySerialize(Weekly obj, out string s)
        {
            s = obj.ToString();
            return true;
        }

        public override bool TryParse(string s, out Weekly obj)
        {
            return Weekly.TryParse(s, out obj);
        }
    }
}
