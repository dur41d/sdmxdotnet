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

        public override bool TrySerialize(TimePeriod value, out string s)
        {
            s = null;

            if (!registry.ContainsKey(value.Type))
            {
                return false;
            }

            var converter = registry[value.Type];

            if (converter is YearConverter)
            {
                var dt = new DateTimeOffset(value.Year, 1, 1, 0, 0, 0, value.Offset);
                return converter.TrySerialize(dt, out s);
            }
            else if (converter is YearMonthConverter)
            {
                var dt = new DateTimeOffset(value.Year, value.Month, 1, 0, 0, 0, value.Offset);
                return converter.TrySerialize(dt, out s);
            }
            if (converter is DateConverter || converter is DateTimeConverter)
            {
                return converter.TrySerialize(value.DateTimeOffset, out s);
            }
            else
            {
                s = value.ToString();
                return true;
            }
        }

        public override bool TryParse(string s, out TimePeriod obj)
        {
            foreach (var converter in registry.Values)
            {
                object result = null;
                if (converter.TryParse(s, out result))
                {
                    if (converter is WeeklyValueConverter)
                    {
                        obj = TimePeriod.FromWeekly((Weekly)result);
                        return true;
                    }
                    else if (converter is QuarterlyValueConverter)
                    {
                        obj = TimePeriod.FromQuarterly((Quarterly)result);
                        return true;
                    }
                    else if (converter is BiannualValueConverter)
                    {
                        obj = TimePeriod.FromBiannual((Biannual)result);
                        return true;
                    }
                    else if (converter is TriannaulValueConverter)
                    {
                        obj = TimePeriod.FromTriannual((Triannual)result);
                        return true;
                    }
                    else if (converter is YearConverter)
                    {
                        obj = TimePeriod.FromYear((DateTimeOffset)result);
                        return true;
                    }
                    else if (converter is YearMonthConverter)
                    {
                        obj = TimePeriod.FromYearMonth((DateTimeOffset)result);
                        return true;
                    }
                    else if (converter is DateConverter)
                    {
                        obj = TimePeriod.FromDate((DateTimeOffset)result);
                        return true;
                    }
                    else if (converter is DateTimeConverter)
                    {
                        obj = TimePeriod.FromDateTime((DateTimeOffset)result);
                        return true;
                    }
                }
            }

            obj = null;
            return false;
        }
    }
}
