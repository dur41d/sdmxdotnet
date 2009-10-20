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
    public class MemberCollectionMap<TObj, TProperty>
    {
        Expression<Func<TObj, IEnumerable<TProperty>>> _collection;
        ClassMap<TProperty> _classMap;
        Action<IEnumerable<TProperty>> _setter;
        Func<TObj, IEnumerable<TProperty>> _getter;


        public MemberCollectionMap(Expression<Func<TObj, IEnumerable<TProperty>>> collection)
        {
            _collection = collection;
            _getter = collection.Compile();
        }

        public MemberCollectionMap<TObj, TProperty> Set(Action<IEnumerable<TProperty>> set)
        {
            _setter = set;
            return this;
        }

        public MemberCollectionMap<TObj, TProperty> ClassMap(ClassMap<TProperty> classMap)
        {
            _classMap = classMap;
            return this;
        }

        internal Collection<TObj, TProperty> GetCollection()
        {
            if (_setter == null)
            {
                throw new OXMException("Setter is not set for property.");
            }

            return new Collection<TObj, TProperty>(_getter, _setter);
        }

        internal ClassMap<TProperty> GetClassMap()
        {
            if (_setter == null)
            {
                throw new OXMException("Class map is not set for property.");
            }

            return _classMap;
        }
    }
}
