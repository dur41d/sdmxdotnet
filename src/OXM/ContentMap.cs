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
    internal class ContentMap<T, TProperty> : IMemberMap<T>
    { 
        internal Property<T, TProperty> Property { get; set; }
        internal SimpleTypeConverter<TProperty> Converter { get; set; }

        public void ReadXml(XmlReader reader)
        {
            string xmlValue = reader.ReadElementContentAsString();

            if (xmlValue != null)
            {
                TProperty property = Converter.ToObj(xmlValue);
                Property.Set(property);
            }
        }

        public void WriteXml(XmlWriter writer, T obj)
        {
            TProperty property = Property.Get(obj);
            string xmlValue = Converter.ToXml(property);
            writer.WriteString(xmlValue);
        }
    }
}
