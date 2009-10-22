using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Common;

namespace OXM
{   
    internal class ElementContainerMap<T> : ElementMapBase<T>, IElementMapContainer<T>
    {
        private List<IMapBuilder<T>> builders = new List<IMapBuilder<T>>();        
        private MapList<T> _elementMaps = new MapList<T>();
        
        public ElementContainerMap(XName name, bool required)
            : base(name, required, false)
        {
        }

        protected PropertyMap<T, TProperty> Map<TProperty>(Expression<Func<T, TProperty>> property)
        {
            var builder = new PropertyMap<T, TProperty>(property);
            builders.Add(builder);
            return builder;
        }

        protected CollectionMap<T, TProperty> Map<TProperty>(Expression<Func<T, IEnumerable<TProperty>>> collection)
        {
            var builder =  new CollectionMap<T, TProperty>(collection);
            builders.Add(builder);
            return builder;
        }

        public override void ReadXml(XElement element)
        {
            _occurances++;

            foreach (var childElement in element.Elements())
            {
                var elementMap = _elementMaps.Get(childElement.Name);
                elementMap.ReadXml(childElement);
            }
        }

        public override void WriteXml(XElement element, T obj)
        {
            XElement container = new XElement(Name);

            foreach (var map in _elementMaps)
            {
                map.WriteXml(container, obj);
            }

            // don't add empty container elements
            if (container.HasElements)
            {
                element.Add(container);
            }
        }

        public override void AssertValid()
        {
            base.AssertValid();

            // if the container element exists then check its children
            if (_occurances > 0)
            {
                foreach (var elementMap in _elementMaps)
                {
                    ((IElementMap<T>)elementMap).AssertValid();
                }
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
