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
        TextFormat _format;

        public TextFormatMap()
        {
            Map<TextType?>(o => GetTextType(o)).ToAttribute("textType", false)
                .Set(v => _format = GetTextFormat(v.Value))
                .Converter(new EnumConverter<TextType?>());
        }

        protected override TextFormat Return()
        {
            return _format;
        }

        private TextType? GetTextType(TextFormat format)
        {
            if (format is StringTextFormat)
                return TextType.String;
            else if (format is DecimalTextFormat)
                return TextType.Double;
            else if (format is IntegerTextFormat)
                return TextType.Integer;
            else if (format is TimePeriodTextFormat)
                return TextType.ObservationalTimePeriod;
            else if (format is YearTextFormat)
                return TextType.Year;
            else if (format is YearMonthTextFormat)
                return TextType.YearMonth;
            else if (format is DateTextFormat)
                return TextType.Date;
            else if (format is DateTimeTextFormat)
                return TextType.DateTime;
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
                case TextType.Integer:
                    return new IntegerTextFormat();
                case TextType.ObservationalTimePeriod:
                    return new TimePeriodTextFormat();
                case TextType.Year:
                    return new YearTextFormat();
                case TextType.YearMonth:
                    return new YearMonthTextFormat();
                case TextType.Date:
                    return new DateTextFormat();
                case TextType.DateTime:
                    return new DateTimeTextFormat();
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
