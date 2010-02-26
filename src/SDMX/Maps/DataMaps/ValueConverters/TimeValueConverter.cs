using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using System.Text.RegularExpressions;
using Common;

namespace SDMX.Parsers
{
    internal class TimePeriodValueConverter : IValueConverter
    {
        static Dictionary<Type, ITimePeriodConverter> registry = new Dictionary<Type, ITimePeriodConverter>();

        static TimePeriodValueConverter()
        {
            // Order is important. From the most restrictive (year) to the least restrictive (datetime)
            registry.Add(typeof(YearValue), new YearValueConverter());
            registry.Add(typeof(YearMonthValue), new YearMonthValueConverter());
            registry.Add(typeof(DateValue), new DateValueConverter());
            registry.Add(typeof(DateTimeValue), new DateTimeValueConverter());
        }

        public Value Parse(string value, string startTime)
        {
            foreach (var converter in registry.Values)
            {
                if (converter.IsValid(value))
                {
                    return (TimePeriod)converter.Parse(value, startTime);
                }
            }

            throw new SDMXException("Invalid time period value '{0}'.", value);
        }

        public string Serialize(Value value, out string startTime)
        {
            var converter = registry[value.GetType()];
            return converter.Serialize(value, out startTime);
        }
    }
}
