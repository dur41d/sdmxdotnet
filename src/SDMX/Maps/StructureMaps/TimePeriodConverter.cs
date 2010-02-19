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
    internal class TimePeriodConverter : ISimpleTypeConverter<ITimePeriod>
    {
        public string ToXml(ITimePeriod value)
        {
            return value == null ? null : value.ToString();
        }

        public ITimePeriod ToObj(string value)
        {
            value = value.Trim();

            if (DateTimeTimePeriod.IsMatch(value))
            {
                return DateTimeTimePeriod.Parse(value);
            }
            else if (DateTimePeriod.IsMatch(value))
            {
                return DateTimePeriod.Parse(value);
            }
            else if (YearMonthTimePeriod.IsMatch(value))
            {
                return YearMonthTimePeriod.Parse(value);
            }
            else if (YearTimePeriod.IsMatch(value))
            {
                return YearTimePeriod.Parse(value);
            }
            else
            {
                throw new SDMXException("Cannot parst value to TimePeriod. Value: '{0}'.", value);
            }
        }
    }
}
