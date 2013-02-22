using System;
using System.Xml;
using System.Xml.Linq;
using Common;

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

        public void ReadXml(XmlReader reader, Action<ValidationMessage> validationAction)
        {
            string xmlValue = reader.GetAttribute(_name.LocalName);

            if (xmlValue == null)
            {
                xmlValue = reader.GetAttribute(_name.LocalName, _name.NamespaceName);
            }

            if (xmlValue == null)
            {
                if (_required)
                {
                    Helper.Notify(validationAction, reader, typeof(T), "Attribute '{0}' for element '{1}' is required but was not found.", _name, reader.GetXName());
                }
                else if (_hasDefault)
                {
                    xmlValue = _default;
                }
            }

            if (xmlValue != null)
            {
                TProperty property = default(TProperty);
                if (Converter.TryParse(xmlValue, out property))
                {
                    try
                    {
                        Property.Set(property);
                    }
                    catch (Exception ex)
                    {
                        throw new MappingException(string.Format(
    @"Mapping  exception while setting property for attribute. Check the mapping class to correct the exception.
Xml Attribute: '{6}'. 
Class Map Type: '{0}'.
Property Type: '{1}'.
Property Value: '{2}'.
Inner Exception Message: '{3}'.
Line Number: '{4}'.
Line Position: '{5}'.", typeof(T), typeof(TProperty), property, ex.Message,
                          ((IXmlLineInfo)reader).LineNumber, ((IXmlLineInfo)reader).LinePosition, _name), ex);

                    }
                }
                else
                {
                    Helper.Notify(validationAction, reader, typeof(T), @"Error converting xml value: '{0}' to type '{1}'.
Converter: '{2}'.", xmlValue, typeof(TProperty), Converter.GetType());
                }
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
                    throw new SerializationException("Attribute '{0}' is required but its value is null.", _name);
                }
            }
            else
            {
                string xmlValue = null;
                if (!Converter.TrySerialize(property, out xmlValue))
                {
                    throw new SerializationException(string.Format(@"Error converting object to xml.
Object ToString: '{0}'
Object Type '{1}'
Converter: '{2}'.", property.ToString(), typeof(TProperty), Converter.GetType()));
                }
                

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
