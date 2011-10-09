using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Common;

namespace SDMX
{
    public class TimePeriodTextFormat : ITimePeriodTextFormat
    {
        static List<ITimePeriodTextFormat> registry = new List<ITimePeriodTextFormat>();

        static TimePeriodTextFormat()
        {
            // Order is important. From the most restrictive (year) to the least restrictive (datetime)
            registry.Add(new YearTextFormat());
            registry.Add(new YearMonthTextFormat());
            registry.Add(new DateTextFormat());
            registry.Add(new DateTimeTextFormat());
        } 

        public bool IsValid(Value value)
        {
            return value is TimePeriod;
        }

        public Type GetValueType()
        {
            return typeof(TimePeriod);            
        }

        public bool TryParse(string s, string startTime, out object value)
        {   
            foreach (var converter in registry)
            {
                if (converter.TryParse(s, startTime, out value))
                {
                    return true;
                }
            }

            value = null;
            return false;
        }
    }
}
