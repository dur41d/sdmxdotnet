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
    internal abstract class SimpleTypeMap<TObj, TProperty> : IMemberMap<TObj>
    {
        internal Property<TObj, TProperty> Property { get; set; }
        internal ISimpleTypeConverter<TProperty> Converter { get; set; }

        protected abstract void WriteValue(XmlWriter writer, string value);
        protected abstract string ReadValue(XmlReader reader);

        public void WriteXml(XmlWriter writer, TObj obj)
        {
            TProperty property = Property.Get(obj);
            string xmlValue = Converter.ToXml(property);
            WriteValue(writer, xmlValue);
        }

        public void ReadXml(XmlReader reader)
        {
            string xmlValue = ReadValue(reader);

            if (xmlValue != null)
            {
                TProperty property = Converter.ToObj(xmlValue);
                Property.Set(property);
            }
        }
    }
}
