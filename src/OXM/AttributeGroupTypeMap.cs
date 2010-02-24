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
    public abstract class AttributeGroupTypeMap<T> : IAttributeMapContainer<T>, IAttributeMap
    {
        internal XNamespace Namespace { get; set; }

        MapList<T> _attributeMaps;
        List<IMapBuilder<T>> builders = new List<IMapBuilder<T>>();
        private string[] _attributesOrder;

        public AttributeGroupTypeMap()
        {
            _attributeMaps = new MapList<T>(this.GetType().Name);
        }

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

        protected SimpleMemberMap<T, TProperty> MapAttribute<TProperty>(Func<T, TProperty> property, XName attributeName, bool required)
        {
            var builder = new PropertyMap<T, TProperty>(property);
            builders.Add(builder);
            return builder.ToAttribute(attributeName, required);
        }

        public T ReadXml(XmlReader reader)
        {
            BuildAndVerifyMaps();

            foreach (var attributeMap in _attributeMaps.GetOrderedList(_attributesOrder))
            {
                attributeMap.ReadXml(reader);
            }

            return Return();
        }

        public void WriteXml(XmlWriter writer, T obj)
        {
            BuildAndVerifyMaps();

            foreach (var map in _attributeMaps.GetOrderedList(_attributesOrder))
            {
                map.WriteXml(writer, obj);
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

        #region IAttributeMap Members

        public bool Required
        {
            get 
            {
                return _attributeMaps.Any(i => ((IAttributeMap)i).Required == true);
            }
        }

        #endregion
    }
}
