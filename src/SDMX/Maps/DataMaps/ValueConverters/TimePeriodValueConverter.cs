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
            registry.Add(typeof(YearValueConverter), new YearValueConverter());
            registry.Add(typeof(YearMonthValueConverter), new YearMonthValueConverter());
            registry.Add(typeof(DateValueConverter), new DateValueConverter());
            registry.Add(typeof(DateTimeValueConverter), new DateTimeValueConverter());
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
            var converter = GetConverter(obj);
            return converter.Serialize(obj, out startTime);
        }

        ITimePeriodConverter GetConverter(object obj)
        { 
            var value = obj as DateTimeOffset?;
            if (value == null)
            {
                throw new SDMXException("Only object of type DateTimeOffset are supported for time value. Type={0}.", obj.GetType());
            }

            if (value.Value.Year > 0
                && value.Value.Month > 0
                && value.Value.Day > 0
                && value.Value.Hour > 0)
            {
                return registry[typeof(DateTimeValueConverter)];
            }
            else if (value.Value.Year > 0
                && value.Value.Month > 0
                && value.Value.Day > 0)
            {
                return registry[typeof(DateValueConverter)];
            }
            else if (value.Value.Year > 0
                && value.Value.Month > 0)
            {
                return registry[typeof(YearMonthValueConverter)];
            }
            else if (value.Value.Year > 0)
            {
                return registry[typeof(YearValueConverter)];
            }
            else
            {
                throw new SDMXException("Cannot figure out converter for DateTimeOffset value: {0}.", value.Value.ToString());
            }
        }
    }
}
