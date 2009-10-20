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
    public class CollectionMap<TObj, TProperty> : IMapBuilder<TObj>
    {
        Expression<Func<TObj, IEnumerable<TProperty>>> _collection;
        XName _name;
        ElementCollectionMap<TObj, TProperty> _collectionMap;
        MemberCollectionMap<TObj, TProperty> _memberCollectionMap;

        public CollectionMap(Expression<Func<TObj, IEnumerable<TProperty>>> collection)
        {
            _collection = collection;
        }

        public MemberCollectionMap<TObj, TProperty> ToElement(XName name, bool required)
        {
            _name = name;
            _collectionMap = new ElementCollectionMap<TObj, TProperty>(name, required);
            _memberCollectionMap = new MemberCollectionMap<TObj, TProperty>(_collection);
            return _memberCollectionMap;
        }

        void IMapBuilder<TObj>.BuildMaps(IMapContainer<TObj> map)
        {
            if (_collectionMap == null)
            {
                throw new OXMException("Error mapping {0}.{1}: a collection must be mapped to either an an element.", "ClassName", "PropertyName");
            }

            ((IElementMapContainer<TObj>)map).AddElementMap(_name, _collectionMap);
            _collectionMap.Collection = _memberCollectionMap.GetCollection();
            _collectionMap.ClassMap = _memberCollectionMap.GetClassMap();
            
        }
    }
}
