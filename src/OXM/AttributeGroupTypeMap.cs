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
    public abstract class AttributeGroupTypeMap<T> : IAttributeMapContainer<T>
    {
        internal XNamespace Namespace { get; set; }

        MapList<T> _attributeMaps = new MapList<T>();
        List<IMapBuilder<T>> builders = new List<IMapBuilder<T>>();
        private string[] _attributesOrder;

        protected abstract T Return();

        bool isBuilt = false;
        private void BuildAndVerifyMaps()
        {
            if (!isBuilt)
            {
                builders.ForEach(b => b.BuildMaps(this));
                isBuilt = true;
            }
        }

        protected void AttributesOrder(params string[] order)
        {
            _attributesOrder = order;
        }

        protected SimpleMemberMap<T, TProperty> MapAttribute<TProperty>(Expression<Func<T, TProperty>> property, XName attributeName, bool required)
        {
            var builder = new PropertyMap<T, TProperty>(property);
            builders.Add(builder);
            return builder.ToAttribute(attributeName, required);
        }

        public T ReadXml(XElement element)
        {
            BuildAndVerifyMaps();

            foreach (var attributeMap in _attributeMaps.GetOrderedList(_attributesOrder))
            {
                attributeMap.ReadXml(element);
            }

            return Return();
        }

        public void WriteXml(XElement element, T obj)
        {
            BuildAndVerifyMaps();


            foreach (var map in _attributeMaps.GetOrderedList(_attributesOrder))
            {
                map.WriteXml(element, obj);
            }
        }

        XNamespace IMapContainer<T>.Namespace
        {
            get { return Namespace; }
        }

        void IAttributeMapContainer<T>.AddAttributeMap(XName name, IMemberMap<T> map)
        {
            _attributeMaps.Add(name, map);
        }
    }
}
