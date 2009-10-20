using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Common;
using System.Runtime.Serialization;

namespace OXM
{
    public abstract class ClassMap<T> : IElementMapContainer<T>, IAttributeMapContainer<T>, IElementContentContainer<T>
    {
        internal XNamespace Namespace { get; set; }
        protected RoolElementMap<T> _rootMap;
        private MapList<T> _attributeMaps = new MapList<T>();
        private MapList<T> _elementMaps = new MapList<T>();
        private IMemberMap<T> _valueMap;
        private string[] _attributesOrder;
        private string[] _elementsOrder;
        private List<IMapBuilder<T>> builders = new List<IMapBuilder<T>>();

        protected abstract T Return();

        private void BuidMaps()
        {
            builders.ForEach(b => b.BuildMaps(this));
        }

        internal void WriteXml(XElement element, T obj)
        {
            BuidMaps();
            foreach (var map in _attributeMaps.GetOrderedList(_attributesOrder))
            {
                map.WriteXml(element, obj);
            }

            foreach (var map in _elementMaps.GetOrderedList(_elementsOrder))
            {
                map.WriteXml(element, obj);
            }

            if (_valueMap != null)
            {
                _valueMap.WriteXml(element, obj);
            }
        }

        internal T ReadXml(XElement element)
        {
            BuidMaps();
            foreach (var attributeMap in _attributeMaps.GetOrderedList(_attributesOrder))
            {
                attributeMap.ReadXml(element);
            }

            foreach (var childElement in element.Elements())
            {
                var elementMap = _elementMaps.Get(childElement.Name);
                elementMap.ReadXml(childElement);
            }

            foreach (var e in _elementMaps)
            {
                ((IElementMap<T>)e).AssertValid();
            }

            if (_valueMap != null)
            {
                _valueMap.ReadXml(element);
            }

            return Return();
        }

        protected PropertyMap<T, TProperty> Map<TProperty>(Expression<Func<T, TProperty>> property)
        {
            var builder = new PropertyMap<T, TProperty>(property);
            builders.Add(builder);
            return builder;
        }

        protected CollectionMap<T, TProperty> Map<TProperty>(Expression<Func<T, IEnumerable<TProperty>>> collection)
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
            if (_valueMap != null)
            {
                throw new OXMException("Element content has already been mapped.");
            }
            _valueMap = map;
        }

        #endregion
    }
}
