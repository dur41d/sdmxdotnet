using System;
using OXM;

namespace SDMX.Parsers
{
    internal class NullableHeaderTimeConverter : ISimpleTypeConverter<DateTimeOffset?>
    {
        HeaderTimeConverter converter = new HeaderTimeConverter();

        public string ToXml(DateTimeOffset? value)
        {
            if (value == null)
                return null;

            return converter.ToXml(value.Value);
        }

        public DateTimeOffset? ToObj(string value)
        {
            if (value == null)
                return null;
            return converter.ToObj(value);
        }
    }
}
