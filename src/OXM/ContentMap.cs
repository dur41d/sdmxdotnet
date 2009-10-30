using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Common;
using System.Xml;

namespace OXM
{    
    internal class ContentMap<T, TProperty> : SimpleTypeMap<T, TProperty>
    {
        protected override void WriteValue(XmlWriter writer, string value)
        {
            writer.WriteString(value);
        }

        protected override string ReadValue(XmlReader reader)
        {
            return reader.ReadElementContentAsString();
        }
    }
}
