using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Common;
using System.Runtime.Serialization;
using System.Xml;

namespace OXM
{
    public class FragmentMap<T>
    {
        XName _name;
        ClassMap<T> _classMap;
        
        public FragmentMap(XName fragmentName, ClassMap<T> classMap)
        {
            _name = fragmentName;
            _classMap = classMap;
            _classMap.Namespace = _name.Namespace;
        }

        public void WriteXml(XmlWriter writer, T obj)
        {
            XElement element = new XElement(_name);
            _classMap.WriteXml(element, obj);
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
            return _classMap.ReadXml((XElement)element);
        }
    }
}
