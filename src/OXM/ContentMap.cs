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

        public void ReadXml(XmlReader reader, Action<ValidationMessage> validationAction)
        {
            string xmlValue = reader.ReadString();

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
@"Mapping  exception while setting property. Check the mapping class to correct the exception. 
Class Map Type: '{0}'.
Property Type: '{1}'.
Property Value: '{2}'.
Inner Exception Message: '{3}'.
Line Number: '{4}'.
Line Position: '{5}'.", typeof(T), typeof(TProperty), property, ex.Message,
                      ((IXmlLineInfo)reader).LineNumber, ((IXmlLineInfo)reader).LinePosition), ex);

                }
            }
            else
            {
                Helper.Notify(validationAction, reader, typeof(T), @"Error converting xml value: '{0}' to type '{1}'.
Converter: '{2}'.", xmlValue, typeof(TProperty), Converter.GetType());
            }
        }

        public void WriteXml(XmlWriter writer, T obj)
        {
            TProperty property = Property.Get(obj);
            string xmlValue = null;
            if (Converter.TrySerialize(property, out xmlValue))
            {
                writer.WriteString(xmlValue);
            }
            else
            {
                throw new SerializationException(string.Format(@"Error converting object to xml.
Object ToString: '{0}'
Object Type '{1}'
Converter: '{2}'.", property.ToString(), typeof(TProperty), Converter.GetType()));
            }

        }
    }
}
