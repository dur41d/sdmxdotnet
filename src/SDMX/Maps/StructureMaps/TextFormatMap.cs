using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using OXM;

namespace SDMX.Parsers
{
    internal class TextFormatMap : ClassMap<TextFormat>
    {
        TextFormat format;

        public TextFormatMap()
        {
            Map<TextType?>(o => GetTextType(o)).ToAttribute("textType", false)
                .Set(v => format = GetTextFormat(v.Value))
                .Converter(new EnumConverter<TextType?>());
        }

        protected override TextFormat Return()
        {
            return format;
        }

        private TextType? GetTextType(TextFormat format)
        {
            if (format is StringTextFormat)
                return null;
            else if (format is DecimalTextFormat)
                return TextType.Double;
            else if (format is TimePeriodTextFormatBase)
                return TextType.ObservationalTimePeriod;
            else
                throw new SDMXException("Unsupported text format: '{0}'.", format);
        }

        private TextFormat GetTextFormat(TextType textType)
        {
            switch (textType)
            { 
                case TextType.None:
                case TextType.String:
                    return new StringTextFormat();
                case TextType.Double:
                    return new DecimalTextFormat();
                case TextType.ObservationalTimePeriod:
                    return new TimePeriodTextFormat();
                default:
                    throw new SDMXException("Unsupported text type '{0}'", textType);
            }
        }
    }

    internal enum TextType
    {
        None,
        String,
        BigInteger,
        Integer,
        Long,
        Short,
        Decimal,
        Float,
        Double,
        Boolean,
        DateTime,
        Date,
        Time,
        Year,
        Month,
        Day,
        MonthDay,
        YearMonth,
        Duration,
        URI,
        Timespan,
        Count,
        InclusiveValueRange,
        ExclusiveValueRange,
        Incremental,
        ObservationalTimePeriod
    }
}
