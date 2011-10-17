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
    interface IAttributeMap
    {
        bool Required { get; }
    }
    
    internal class AttributeMap<T, TProperty> : IMemberMap<T>, IAttributeMap
    {
        private XName _name;
        private bool _required;
        private string _default;
        private bool _hasDefault;
        private bool _writeDefault;

        internal Property<T, TProperty> Property { get; set; }
        internal SimpleTypeConverter<TProperty> Converter { get; set; }

        public AttributeMap(XName name, bool required, string defaultValue, bool writeDefault)
        {
            _name = name;
            _required = required;
            _default = defaultValue;
            _hasDefault = defaultValue != null;
            _writeDefault = writeDefault;
        }

        public bool Required
        {
            get
            {
                return _required;
            }
        }

        public void ReadXml(XmlReader reader)
        {
            string value = reader.GetAttribute(_name.LocalName);

            if (value == null)
            {
                value = reader.GetAttribute(_name.LocalName, _name.NamespaceName);
            }

            if (value == null)
            {
                if (_required)
                {
                    ParseException.Throw(reader, typeof(T), "Attribute '{0}' for element '{1}' is required but was not found.", _name, reader.GetXName());
                }
                else if (_hasDefault)
                {
                    value = _default;
                }
            }

            if (value != null)
            {
                TProperty property = Converter.ToObj(value);
                Property.Set(property);
            }
        }

        public void WriteXml(XmlWriter writer, T obj)
        {
            TProperty property = Property.Get(obj);                       

            if (property.IsDefault())
            {
                // if the attribute is required through an exception otherwise do nothing
                if (_required)
                {
                    throw new ParseException("Attribute '{0}' is required but its value is null.", _name);
                }
            }
            else
            {
                string xmlValue = Converter.ToXml(property);
                

                // write the attribute if it's writeDefault is true or it's required or if it's value
                // is not equal to the default value.
                // if the attribute is optional and the its value is equal to the default value 
                // then don't write it for compactness
                if (_writeDefault || _required || !(_hasDefault && _default.Equals(xmlValue)))
                {
                    writer.WriteAttributeString(_name.LocalName, _name.NamespaceName, xmlValue);
                }
            }
        }
    }
}
