using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;
using Common;

namespace OXM
{
    public interface IRootElementMap<T>
    {
         void WriteXml(XmlWriter writer, T obj);
         T ReadXml(XmlReader reader);
    }

    public abstract class RootElementMap<T> : ClassMap<T>
    {
        public abstract XName Name { get; }

        private Dictionary<string, XNamespace> namespaces = new Dictionary<string, XNamespace>();

        public RootElementMap()
        {            
            Namespace = Name.Namespace;
            _rootMap = this;
        }

        public void RegisterNamespace(string prefix, XNamespace ns)
        {
            namespaces.Add(prefix, ns);
        }      

        public new void WriteXml(XmlWriter writer, T obj)
        {
            writer.WriteStartElement(Name.LocalName, Name.NamespaceName);
            foreach (var item in namespaces)
            {
                writer.WriteAttributeString("xmlns", item.Key, null, item.Value.NamespaceName);

            }
            base.WriteXml(writer, obj);
            writer.WriteEndElement();
        }

        public T ReadXml(XmlReader reader)
        {
            return ReadXml(reader, null);
        }

        public new T ReadXml(XmlReader reader, Action<ValidationMessage> validationAction)
        {   
            if (reader.ReadState == ReadState.Initial)
            {
                reader.Read();
            }

            if (reader.NodeType != XmlNodeType.Element)
            {
                reader.ReadNextStartElement();
            }

            if (!reader.NameEquals(Name))
            {
                Helper.Notify(validationAction, reader, typeof(T), "The first element name is '{0}' and the expected name is '{1}'."
                    , string.IsNullOrEmpty(reader.NamespaceURI) ? 
                        reader.Name : string.Format("{0}:{1}", reader.NamespaceURI, reader.Name)
                    , Name);
            }

            return base.ReadXml(reader, validationAction);
        }
    }
}
