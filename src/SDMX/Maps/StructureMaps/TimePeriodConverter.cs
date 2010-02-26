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
    internal class HeaderTimeConverter : ISimpleTypeConverter<TimePeriod>
    {
        TimePeriodValueConverter converter = new TimePeriodValueConverter();

        public string ToXml(TimePeriod value)
        {
            string startTime;
            return value == null ? null : converter.Serialize(value, out startTime);
        }

        public TimePeriod ToObj(string value)
        {
            string startTime = null;
            return (TimePeriod)converter.Parse(value, startTime);
        }
    }

    internal class TimePeriodConverter : ISimpleTypeConverter<TimePeriod>
    {
        TimePeriodValueConverter converter = new TimePeriodValueConverter();
      
        public string ToXml(TimePeriod value)
        {           
            string startTime;
            return value == null ? null : converter.Serialize(value, out startTime);
        }

        public TimePeriod ToObj(string value)
        {
            string startTime = null;
            return (TimePeriod)converter.Parse(value, startTime);
        }
    }
}
