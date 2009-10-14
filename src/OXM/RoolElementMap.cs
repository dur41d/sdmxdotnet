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
        public abstract string Name { get; }

        public override void ToXml(T obj, XElement element)
        {
            throw new NotImplementedException();
        }

        public void WriteXml(T obj, XmlWriter writer)
        {
            XElement element = new XElement(Name);
            base.ToXml(obj, element);
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
            return base.ToObj((XElement)element);
        }
    }
}
