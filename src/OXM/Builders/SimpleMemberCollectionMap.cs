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
    public class SimpleMemberCollectionMap<TObj, TProperty>
    {
        ISimpleTypeConverter<TProperty> _converter;
        MemberCollectionMap<TObj, TProperty> _memberCollectionMap;
        Expression<Func<TObj, IEnumerable<TProperty>>> _collection;


        public SimpleMemberCollectionMap(Expression<Func<TObj, IEnumerable<TProperty>>> collection)
        {
            _memberCollectionMap = new MemberCollectionMap<TObj, TProperty>(collection);
            _collection = collection;
        }

        public SimpleMemberCollectionMap<TObj, TProperty> Set(Action<TProperty> set)
        {
            _memberCollectionMap.Set(set);
            return this;
        }

        public SimpleMemberCollectionMap<TObj, TProperty> Converter(ISimpleTypeConverter<TProperty> converter)
        {
            _converter = converter;
            return this;
        }

        internal Collection<TObj, TProperty> GetCollection()
        {
            return _memberCollectionMap.GetCollection();
        }

        internal ISimpleTypeConverter<TProperty> GetConverter()
        {
            if (_converter == null)
            {
                var coll = new Collection<TObj, TProperty>(_collection, null);
                throw new OXMException("Converter is not set for collection ({0}).{1}.", coll.GetTypeName(), coll.GetName());
            }

            return _converter;
        }
    }
}
