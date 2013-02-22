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
        Func<TObj, IEnumerable<TProperty>> _collection;
        XName _name;
        bool _required;

        bool isSimple, isClassMap;
        
        SimpleMemberCollectionMap<TObj, TProperty> _simpleMemberMap;
        ClassMemberCollectionMap<TObj, TProperty> _classMemberMap;

        public CollectionMap(Func<TObj, IEnumerable<TProperty>> collection)
        {
            _collection = collection;
        }

        public ClassMemberCollectionMap<TObj, TProperty> ToElement(XName name, bool required)
        {
            isClassMap = true;
            
            _name = name;
            _required = required;
            
            _classMemberMap = new ClassMemberCollectionMap<TObj, TProperty>(_collection);
            return _classMemberMap;
        }

        public SimpleMemberCollectionMap<TObj, TProperty> ToSimpleElement(XName name, bool required)
        {
            isSimple = true;

            _name = name;
            _required = required;

            _simpleMemberMap = new SimpleMemberCollectionMap<TObj, TProperty>(_collection);
            return _simpleMemberMap;
        }

        void IMapBuilder<TObj>.BuildMaps(IMapContainer<TObj> map)
        {
            MapBuilderUtility.QualifyName(ref _name, map.Namespace);

            if (isSimple)
            {
                var collectionMap = new SimpleElementCollectionMap<TObj, TProperty>(_name, _required);
                collectionMap.ClassMapFactory = () => new SimpleElementClassMap<TProperty>(_simpleMemberMap.GetConverter());
                collectionMap.Collection = _simpleMemberMap.GetCollection();
                ((IElementMapContainer<TObj>)map).AddElementMap(_name, collectionMap);
            }
            else if (isClassMap)
            {
                var collectionMap = new ElementCollectionMap<TObj, TProperty>(_name, _required);
                collectionMap.Collection = _classMemberMap.GetCollection();
                collectionMap.ClassMapFactory = _classMemberMap.GetClassMapFactory();
                ((IElementMapContainer<TObj>)map).AddElementMap(_name, collectionMap);
            }
            else
            {
                var coll = new Collection<TObj, TProperty>(_collection, null);
                throw new MappingException("Error mapping ({0}).{1}: a collection must be mapped to either an an element or simple element.", coll.GetTypeName(), coll.GetName());
            }
        }
    }
}
