using System;
using OXM;

namespace SDMX.Parsers
{
    internal class HeaderTimeConverter : ISimpleTypeConverter<DateTimeOffset>
    {
        TimePeriodValueConverter converter = new TimePeriodValueConverter();

        public string ToXml(DateTimeOffset value)
        {
            string startTime;

            TimePeriod timePeriod = null;

            if (value.Hour > 0)
                timePeriod = new DateTimeValue(value);
            else
                timePeriod = new Date(value);

            return converter.Serialize(timePeriod, out startTime);
        }

        public DateTimeOffset ToObj(string value)
        {
            string startTime = null;
            var timePeriod = (TimePeriod)converter.Parse(value, startTime);

            if (timePeriod is DateTimeValue)
                return (DateTimeOffset)(DateTimeValue)timePeriod;
            else
                return (DateTimeOffset)(Date)timePeriod;
        }
    }
}