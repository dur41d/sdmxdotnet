using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Common;

namespace OXM
{
    internal abstract class SimpleTypeMap<TObj, TProperty> : IMemberMap<TObj>
    {
        protected Property<TObj, TProperty> Property { get; set; }
        protected ISimpleTypeConverter<TProperty> Converter { get; set; }

        protected abstract void WriteValue(XElement element, string value);
        protected abstract string ReadValue(XElement element);

        public void WriteXml(XElement element, TObj obj)
        {
            TProperty property = Property.Get(obj);
            string xmlValue = Converter.ToXml(property);
            WriteValue(element, xmlValue);
        }

        public void ReadXml(XElement element)
        {
            string xmlValue = ReadValue(element);
            TProperty property = Converter.ToObj(xmlValue);
            Property.Set(property);
        }

        public SimpleTypeMap<TObj, TProperty> SetProperty(Property<TObj, TProperty> property)
        {
            Property = property;
            return this;
        }

        public SimpleTypeMap<TObj, TProperty> SetConverter(ISimpleTypeConverter<TProperty> converter)
        {
            Converter = converter;
            return this;
        }
    }
}
