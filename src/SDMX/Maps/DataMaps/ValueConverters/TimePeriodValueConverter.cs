using System;
using System.Collections.Generic;

namespace SDMX.Parsers
{
    internal class TimePeriodValueConverter : IValueConverter
    {
        static Dictionary<Type, ITimePeriodConverter> registry = new Dictionary<Type, ITimePeriodConverter>();

        static TimePeriodValueConverter()
        {
            // Order is important. From the most restrictive (year) to the least restrictive (datetime)
            registry.Add(typeof(WeeklyValue), new WeeklyValueConverter());
            registry.Add(typeof(QuarterlyValue), new QuarterlyValueConverter());
            registry.Add(typeof(BiannualValue), new BiannualValueConverter());
            registry.Add(typeof(TriannualValue), new TriannaulValueConverter());
            registry.Add(typeof(YearValue), new YearValueConverter());
            registry.Add(typeof(YearMonthValue), new YearMonthValueConverter());
            registry.Add(typeof(DateValue), new DateValueConverter());
            registry.Add(typeof(DateTimeValue), new DateTimeValueConverter());
        }

        public object Parse(string value, string startTime)
        {
            foreach (var converter in registry.Values)
            {
                if (converter.IsValid(value))
                {
                    return converter.Parse(value, startTime);
                }
            }

            throw new SDMXException("Invalid time period value '{0}'.", value);
        }

        public string Serialize(object obj, out string startTime)
        {
            var type = obj.GetType();
            if (!registry.ContainsKey(type))
            {
                throw new SDMXException("cannot serialize type: {0}.", type);
            }

            var converter = registry[type];
            return converter.Serialize(obj, out startTime);
        }
    }
}
