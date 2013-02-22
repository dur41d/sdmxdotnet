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
    internal static class Extensions
    {
        public static bool IsDefault<T>(this T value)
        {
            //if (typeof(T).IsEnum) return false;
            //if (typeof(T) == typeof(bool)) return false;

            return (object)value == null;
            //return value.Equals(default(T));
        }
    }

    internal class ElementMap<T, TProperty> : ElementMapBase<T>
    {
        internal Property<T, TProperty> Property { get; set; }        
        
        internal Func<ClassMap<TProperty>> ClassMapFactory { get; set; }

        public ElementMap(XName name, bool required)
            : base(name, required)
        {
        }

        public override void ReadXml(XmlReader reader, Action<ValidationMessage> validationAction)
        {
            var classMap = ClassMapFactory();
            classMap.Namespace = Name.Namespace;
            TProperty property = classMap.ReadXml(reader, validationAction);

            if (!property.IsDefault())
            {
                try
                {
                    Property.Set(property);
                }
                catch (Exception ex)
                {
                    throw new MappingException(string.Format(
@"Mapping  exception while setting property. Check the mapping class to correct the exception.
Element Name: '{6}'.
Class Map Type: '{0}'.
Property Type: '{1}'.
Property Value: '{2}'.
Inner Exception Message: '{3}'.
Line Number: '{4}'.
Line Position: '{5}'.", classMap.GetType(), typeof(TProperty), property, ex.Message, 
                      ((IXmlLineInfo)reader).LineNumber, ((IXmlLineInfo)reader).LinePosition, Name), ex);

                }
            }
        }

        public override void WriteXml(XmlWriter writer, T obj)
        {
            var value = Property.Get(obj);
            if (value.IsDefault())
            {
                if (Required)
                {
                    throw new SerializationException("Element '{0}' is required but its property value is null. Property: ({1}).{2}"
                        , Name, Property.GetTypeName(), Property.GetName());
                }
            }
            else
            {
                if (Writing != null) Writing();
                writer.WriteStartElement(Name.LocalName, Name.NamespaceName);
                var classMap = ClassMapFactory();
                classMap.Namespace = Name.Namespace;
                classMap.WriteXml(writer, value);
                writer.WriteEndElement();
            }
        }
    }
}
