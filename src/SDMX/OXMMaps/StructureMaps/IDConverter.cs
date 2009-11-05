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

namespace SDMX.Parsers
{
    internal class TimePeriodConverter : ISimpleTypeConverter<TimePeriod>
    {
        public string ToXml(TimePeriod value)
        {
            return value == null ? null : value.ToString();
        }

        public TimePeriod ToObj(string value)
        {
            var dateTime = XmlConvert.ToDateTime(value, XmlDateTimeSerializationMode.RoundtripKind);
            return new TimePeriod(dateTime);
        }
    }
    
    internal class IDConverter : ISimpleTypeConverter<ID>
    {
        public string ToXml(ID value)
        {
            return value == null ? null : value.ToString();
        }

        public ID ToObj(string value)
        {
            return new ID(value);
        }
    }
}
