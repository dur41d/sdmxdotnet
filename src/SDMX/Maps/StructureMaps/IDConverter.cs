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
            return value.ToString();
        }

        public ID ToObj(string value)
        {
            return (ID)value;
        }
    }   
}
