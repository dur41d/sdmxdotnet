using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;

namespace OXM
{
    public abstract class RoolElementMap<T> : ClassMap<T>
    {
        public abstract XName Name { get; }

        private Dictionary<string, XNamespace> namespaces = new Dictionary<string, XNamespace>();

        public RoolElementMap()
        {
            if (Name.NamespaceName == "")
            {
                throw new OXMException("Root element is not qualified '{0}'. Please set the name space", Name);
            }
            Namespace = Name.Namespace;
            _rootMap = this;
        }

        //public void RegisterNamespace(string prefix, XNamespace ns)
        //{               
        //    namespaces.Add(prefix, ns);
        //}

        //internal void VerifyNamespace(XNamespace ns)
        //{
        //    if (namespaces.Values.Where(x => x == ns).Count() > 0)
        //    {
        //        throw new OXMException("Namespace '{0}' is not registered. Must be registered with the root element.");
        //    }
        //}

        public void WriteXml(XmlWriter writer, T obj)
        {
            XElement element = new XElement(Name);            
            base.WriteXml(element, obj);
            element.WriteTo(writer);
        }

        public T ReadXml(XmlReader reader)
        {
            do
            {
                reader.Read();
            }
            while (reader.NodeType != XmlNodeType.Element);

            var element = XNode.ReadFrom(reader);
            return base.ReadXml((XElement)element);
        }
    }
}
