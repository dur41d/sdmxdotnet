using System;
using OXM;

namespace SDMX.Parsers
{
    internal class HeaderTimeConverter : SimpleTypeConverter<DateTimeOffset>
    {
        public override string ToXml(DateTimeOffset value)
        {
            if (value.Hour > 0)
                return new DateTimeConverter().ToXml(value);
            else
                return new DateConverter().ToXml(value);
        }

        public override DateTimeOffset ToObj(string value)
        {
            var converter = new DateConverter();

            if (converter.CanConvertToObj(value))
                return converter.ToObj(value);
            else
                return new DateTimeConverter().ToObj(value);
        }

        public override bool CanConvertToObj(string s)
        {
            return new DateConverter().CanConvertToObj(s) || new DateTimeConverter().CanConvertToObj(s);
        }
    }
}