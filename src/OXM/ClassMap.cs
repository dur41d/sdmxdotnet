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
    internal class NameCounter<T>
    {
        Dictionary<T, int> counts = new Dictionary<T, int>();

        public void Increment(T name)
        {
            if (counts.ContainsKey(name))
            {
                counts[name]++;
            }
            else
            {
                counts.Add(name, 1);
            }
        }

        public int Get(T name)
        {
            if (counts.ContainsKey(name))
            {
                return counts[name];
            }
            else
            {
                return 0;
            }
        }
    }
   
    public abstract class ClassMap<T> : IElementMapContainer<T>, IAttributeMapContainer<T>, IElementContentContainer<T>
    {
        internal XNamespace Namespace { get; set; }
        protected RoolElementMap<T> _rootMap;
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
                    throw new OXMException("Class map for '{0}' has both elements and content. This is not possible.", typeof(T).ToString());
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
                            var elementMap = _elementMaps.Get(name);                            
                            elementMap.ReadXml(subReader);
                            counts.Increment(name);  
                        }

                        foreach (IElementMap<T> elementMap in _elementMaps)
                        {
                            int count = counts.Get(elementMap.Name);
                            if (elementMap.Required && count == 0)
                            {
                                throw new OXMException("Element '{0}' is required but was not found'", elementMap.Name);
                            }
                        }
                    }
                }               
            }

            return Return();
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
                throw new OXMException("Element content has already been mapped.");
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
