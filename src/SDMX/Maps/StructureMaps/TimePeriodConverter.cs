using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Common;
using OXM;
using System.Xml;
using System.Text.RegularExpressions;

namespace SDMX.Parsers
{
    internal class TimePeriodConverter : SimpleTypeConverter<TimePeriod>
    {
        static Dictionary<TimePeriodType, ISimpleTypeConverter> registry = new Dictionary<TimePeriodType, ISimpleTypeConverter>();

        static TimePeriodConverter()
        {
            // Order is important. From the most restrictive (year) to the least restrictive (datetime)
            registry.Add(TimePeriodType.Weekly, new WeeklyValueConverter());
            registry.Add(TimePeriodType.Quarterly, new QuarterlyValueConverter());
            registry.Add(TimePeriodType.Biannual, new BiannualValueConverter());
            registry.Add(TimePeriodType.Triannual, new TriannaulValueConverter());
            registry.Add(TimePeriodType.Year, new YearConverter());
            registry.Add(TimePeriodType.YearMonth, new YearMonthConverter());
            registry.Add(TimePeriodType.Date, new DateConverter());
            registry.Add(TimePeriodType.DateTime, new DateTimeConverter());
        }


        public override string ToXml(TimePeriod value)
        {
            if (!registry.ContainsKey(value.TimePeriodType))
            {
                throw new SDMXException("Cannot serialize type: {0}.", value.TimePeriodType);
            }

            var converter = registry[value.TimePeriodType];

            if (converter is YearConverter
                        || converter is YearMonthConverter
                        || converter is DateConverter
                        || converter is DateTimeConverter)
            {
                return converter.ToXml(value.DateTimeOffset);
            }
            else
            {
                return value.ToString();
            }
        }

        public override TimePeriod ToObj(string value)
        {
            foreach (var converter in registry.Values)
            {
                if (converter.CanConvertToObj(value))
                {
                    if (converter is WeeklyValueConverter)
                        return TimePeriod.FromWeekly((Weekly)converter.ToObj(value));
                    if (converter is QuarterlyValueConverter)
                        return TimePeriod.FromQuarterly((Quarterly)converter.ToObj(value));
                    if (converter is BiannualValueConverter)
                        return TimePeriod.FromBiannual((Biannual)converter.ToObj(value));
                    if (converter is TriannaulValueConverter)
                        return TimePeriod.FromTriannual((Triannual)converter.ToObj(value));
                    else if (converter is YearConverter)
                        return TimePeriod.FromYear((DateTimeOffset)converter.ToObj(value));
                    else if (converter is YearMonthConverter)
                        return TimePeriod.FromYearMonth((DateTimeOffset)converter.ToObj(value));
                    else if (converter is DateConverter)
                        return TimePeriod.FromDate((DateTimeOffset)converter.ToObj(value));
                    else if (converter is DateTimeConverter)
                        return TimePeriod.FromDateTime((DateTimeOffset)converter.ToObj(value));
                }
            }

            throw new SDMXException("Invalid time period value '{0}'.", value);
        }
    }
}
