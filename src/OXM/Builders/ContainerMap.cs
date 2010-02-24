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
    public class ContainerMap<T> : IMapBuilder<T>
    {
        private List<IMapBuilder<T>> builders = new List<IMapBuilder<T>>();
        private XName _name;
        private bool _required;

        public ContainerMap(XName name, bool required)
        {
            _name = name;
            _required = required;
        }

        public PropertyMap<T, TProperty> Map<TProperty>(Func<T, TProperty> property)
        {
            var builder = new PropertyMap<T, TProperty>(property);
            builders.Add(builder);
            return builder;
        }

        public CollectionMap<T, TProperty> MapCollection<TProperty>(Func<T, IEnumerable<TProperty>> collection)
        {            
            var builder = new CollectionMap<T, TProperty>(collection);
            builders.Add(builder);
            return builder;
        }

        #region IMapBuilder<TObj> Members

        void IMapBuilder<T>.BuildMaps(IMapContainer<T> map)
        {
            MapBuilderUtility.QualifyName(ref _name, map.Namespace);
            
            var elementMap = new ElementContainerMap<T>(_name, _required, map.GetType().Name);
            ((IElementMapContainer<T>)map).AddElementMap(_name, elementMap);

            // build children
            builders.ForEach(b => b.BuildMaps(elementMap));
        }

        #endregion
    }
}
