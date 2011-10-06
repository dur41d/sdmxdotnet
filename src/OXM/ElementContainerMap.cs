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
    internal class ElementContainerMap<T> : ElementMapBase<T>, IElementMapContainer<T>
    {
        private MapList<T> _elementMaps;

        public ElementContainerMap(XName name, bool required, string classMapName)
            : base(name, required)
        {
            _elementMaps = new MapList<T>(classMapName + "." + name.LocalName);
        }

        public override void ReadXml(XmlReader reader)
        {
            if (reader.IsEmptyElement)
            {
                reader.Read();
            }
            else
            {
                using (var subReader = reader.ReadSubtree())
                {
                    subReader.ReadStartElement();

                    var counts = new NameCounter<XName>();
                    while (subReader.ReadNextElement())
                    {
                        XName name = subReader.GetXName();
                        var elementMap = _elementMaps.Get(name, reader, typeof(T));
                        elementMap.ReadXml(subReader);
                        counts.Increment(name); 
                    }
                    foreach (IElementMap<T> elementMap in _elementMaps)
                    {
                        int count = counts.Get(elementMap.Name);
                        if (elementMap.Required && count == 0)
                        {
                            ParseException.Throw(subReader, typeof(T), "Element '{0}' is required but was not found.", elementMap.Name);
                        }
                    }
                }
            }                 
        }

        public override void WriteXml(XmlWriter writer, T obj)
        {
            bool isWritten = false;
            
            foreach (var map in _elementMaps)
            {
                // before a child element start writing write the start element of the container
                // this is necessary to avoid writing and empty container element we we only 
                // write the container element if a child element starts writing
                ((IElementMap<T>)map).Writing = () =>
                    {
                        if (!isWritten)
                        {
                            writer.WriteStartElement(Name.LocalName, Name.NamespaceName);
                            isWritten = true;
                        }
                    };
                
                map.WriteXml(writer, obj);
            }
            
            // don't add empty container elements
            if (isWritten)
            {
                writer.WriteEndElement();
            }
        }   

        #region IElementMapContainer<T> Members

        void IElementMapContainer<T>.AddElementMap(XName name, IMemberMap<T> map)
        {
            _elementMaps.Add(name, map);
        }

        #endregion

        #region IMapContainer<T> Members

        XNamespace IMapContainer<T>.Namespace
        {
            get { return Name.Namespace; }
        }

        #endregion
    }
}
