using System;
using OXM;

namespace SDMX.Parsers
{
    internal class NullableHeaderTimeConverter : NullabeConverter<DateTimeOffset>
    {
        HeaderTimeConverter converter = new HeaderTimeConverter();

        protected override SimpleTypeConverter<DateTimeOffset> Converter
        {
            get
            {
                return new HeaderTimeConverter();
            }
        }
    }
}
