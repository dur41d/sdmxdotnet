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

    internal class NullableIDConverter : ISimpleTypeConverter<ID?>
    {
        IDConverter converter = new IDConverter();
        public string ToXml(ID? value)
        {
            return !value.HasValue ? null : converter.ToXml(value.Value);
        }

        public ID? ToObj(string value)
        {
            return value == null ? (ID?)null : converter.ToObj(value);
        }
    }
}
