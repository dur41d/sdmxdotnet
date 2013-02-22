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
    internal class ElementCollectionMap<T, TProperty> : ElementMapBase<T>
    {
        internal Collection<T, TProperty> Collection { get; set; }
        internal Func<ClassMap<TProperty>> ClassMapFactory { get; set;}

        public ElementCollectionMap(XName name, bool required)
            : base(name, required)
        {}

        public override void ReadXml(XmlReader reader, Action<ValidationMessage> validationAction)
        {
            var classMap = ClassMapFactory();
            classMap.Namespace = Name.Namespace;

            TProperty property = classMap.ReadXml(reader, validationAction);
            
            if (!property.IsDefault())
            {
                try
                {
                    Collection.Set(property);
                }
                catch (Exception ex)
                {
                    throw new MappingException(string.Format(
@"Mapping  exception while setting collection property. Check the mapping class to correct the exception. 
Collection Element Name: '{6}'.
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
            var values = Collection.Get(obj);

            if ((object)values == null)
            {
                if (Required)
                {
                    throw new SerializationException("Element Collection '{0}' is required but its value is null. Collection: ({1}).{2}"
                        , Name, Collection.GetTypeName(), Collection.GetName());
                }
            }          
            else
            {
                if (Required && values.Count() == 0)
                {
                    throw new SerializationException("Element Collection '{0}' is required but the collection is empty. Collection: ({1}).{2}"
                        , Name, Collection.GetTypeName(), Collection.GetName());
                }

                foreach (TProperty value in values)
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
}
