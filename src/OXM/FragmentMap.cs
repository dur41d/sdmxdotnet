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
            if (reader.ReadState == ReadState.Initial)
            {
                reader.Read();
            }

            if (reader.NodeType != XmlNodeType.Element)
            {
                reader.ReadNextStartElement();
            }

            if (!reader.NameEquals(_name))
            {
                ParseException.Throw(reader, typeof(T), "The first element name is '{0}:{1}' and the expected name is '{2}'."
                    , reader.NamespaceURI, reader.Name, _name);
            }

            return _classMap.ReadXml(reader);
        }
    }
}
