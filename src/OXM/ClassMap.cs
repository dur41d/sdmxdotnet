using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Common;

namespace OXM
{
    public abstract class ClassMap<T> : IElementMapContainer<T>, IAttributeMapContainer<T>, IElementContentContainer<T>
    {
        internal XNamespace Namespace { get; set; }
        protected RootElementMap<T> _rootMap;
        private MapList<T> _attributeMaps;
        private MapList<T> _elementMaps;
        private IMemberMap<T> _contentMap;
        private string[] _attributesOrder;
        private string[] _elementsOrder;
        private List<IMapBuilder<T>> builders = new List<IMapBuilder<T>>();

        public ClassMap()
        {
            string name = this.GetType().Name;

            _attributeMaps = new MapList<T>(name);
            _elementMaps = new MapList<T>(name);
        }

        protected abstract T Return();

        bool isBuilt = false;
        private void BuildAndVerifyMaps()
        {
            if (!isBuilt)
            {
                builders.ForEach(b => b.BuildMaps(this));

                if (_contentMap != null && _elementMaps.Count() > 0)
                {
                    throw new ParseException("Class map for '{0}' has both elements and content. This is not possible.", typeof(T).ToString());
                }
                isBuilt = true;
            }
        }

        internal void WriteXml(XmlWriter writer, T obj)
        {
            BuildAndVerifyMaps();

            foreach (var map in _attributeMaps.GetOrderedList(_attributesOrder))
            {
                map.WriteXml(writer, obj);
            }

            foreach (var map in _elementMaps.GetOrderedList(_elementsOrder))
            {
                map.WriteXml(writer, obj);
            }

            if (_contentMap != null)
            {
                _contentMap.WriteXml(writer, obj);
            }
        }

        internal T ReadXml(XmlReader reader)
        {
            BuildAndVerifyMaps();

            foreach (var attributeMap in _attributeMaps.GetOrderedList(_attributesOrder))
            {
                attributeMap.ReadXml(reader);
            }

            if (_contentMap != null)
            {
                _contentMap.ReadXml(reader);
            }
            else
            {
                ReadElements(reader, _elementMaps);
            }   

            return Return();
        }

        internal static void ReadElements(XmlReader reader, MapList<T> elementMaps)
        {
            var counts = new NameCounter<XName>();

            if (!reader.IsEmptyElement)
            {
                int depth = reader.Depth;

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.EndElement && reader.Depth == depth)
                    { 
                        break;
                    }
                    else if (reader.NodeType == XmlNodeType.Element)
                    {
                        XName name = reader.GetXName();
                        var elementMap = elementMaps.Get(name);
                        if (elementMap == null)
                        {
#if DEBUG
                            System.Diagnostics.Debug.WriteLine(string.Format("Element '{0}' is not Mapped. Line: {1} Position: {2} Type: ClassMap<{3}>",
                                name, ((IXmlLineInfo)reader).LineNumber, ((IXmlLineInfo)reader).LineNumber, typeof(T)), "Warning");
#endif
                            continue;
                        }
                        elementMap.ReadXml(reader);
                        counts.Increment(name);
                    }
                }
            }

            foreach (IElementMap<T> elementMap in elementMaps)
            {
                if (elementMap.Required && counts.Get(elementMap.Name) == 0)
                {
                    ParseException.Throw(reader, typeof(T), "Element '{0}' is required but was not found.", elementMap.Name);
                }
            }
        }

        protected PropertyMap<T, TProperty> Map<TProperty>(Func<T, TProperty> property)
        {
            var builder = new PropertyMap<T, TProperty>(property);
            builders.Add(builder);
            return builder;
        }

        protected CollectionMap<T, TProperty> MapCollection<TProperty>(Func<T, IEnumerable<TProperty>> collection)
        {
            var builder = new CollectionMap<T, TProperty>(collection);
            builders.Add(builder);
            return builder;
        }

        protected void AttributesOrder(params string[] order)
        {
            _attributesOrder = order;
        }

        protected void ElementsOrder(params string[] order)
        {
            _elementsOrder = order;
        }

        protected ContainerMap<T> MapContainer(XName name, bool required)
        {
            var builder = new ContainerMap<T>(name, required);
            builders.Add(builder);
            return builder;
        }

        #region IAttributeMapContainer<T> Members

        void IAttributeMapContainer<T>.AddAttributeMap(XName name, IMemberMap<T> map)
        {
            _attributeMaps.Add(name, map);
        }

        #endregion

        #region IElementMapContainer<T> Members

        void IElementMapContainer<T>.AddElementMap(XName name, IMemberMap<T> map)
        {
            _elementMaps.Add(name, map);
        }

        #endregion

        #region IElementContentContainer<T> Members

        void IElementContentContainer<T>.SetElementContentMap(IMemberMap<T> map)
        {
            if (_contentMap != null)
            {
                throw new ParseException("Element content is mapped more than once in {0}.", this.GetType().Name);
            }
            _contentMap = map;
        }

        #endregion

        #region IMapContainer<T> Members

        XNamespace IMapContainer<T>.Namespace
        {
            get { return Namespace; }
        }

        #endregion
    }
}
