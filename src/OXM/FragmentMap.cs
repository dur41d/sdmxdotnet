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
            writer.WriteStartElement(_name.LocalName, _name.NamespaceName);
            _classMap.WriteXml(writer, obj);
            writer.WriteEndElement();
        }

        public T ReadXml(XmlReader reader)
        {
            do
            {
                reader.Read();
            }
            while (reader.NodeType != XmlNodeType.Element);


            return _classMap.ReadXml(reader);
        }
    }
}
