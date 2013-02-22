using System;
using OXM;

namespace SDMX.Parsers
{
    internal class HeaderTimeConverter : SimpleTypeConverter<DateTimeOffset>
    {
        DateTimeConverter _dateTimeConverter = new DateTimeConverter();
        DateConverter _dateConverter = new DateConverter();
        
        public override bool TrySerialize(DateTimeOffset obj, out string s)
        {
            if (obj.Hour > 0)
                return _dateTimeConverter.TrySerialize(obj, out s);
            else
                return _dateConverter.TrySerialize(obj, out s);
        }

        public override bool TryParse(string s, out DateTimeOffset obj)
        {
            if (_dateConverter.TryParse(s, out obj))
            {
                return true;
            }
            else
            {
                return _dateTimeConverter.TryParse(s, out obj);
            }
        }
    }
}