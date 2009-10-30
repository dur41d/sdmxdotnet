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
    public class ClassMemberCollectionMap<TObj, TProperty>
    {
        ClassMap<TProperty> _classMap;
        MemberCollectionMap<TObj, TProperty> _memberCollectionMap;

        public ClassMemberCollectionMap(Expression<Func<TObj, IEnumerable<TProperty>>> collection)
        {
            _memberCollectionMap = new MemberCollectionMap<TObj, TProperty>(collection);
        }

        public ClassMemberCollectionMap<TObj, TProperty> Set(Action<TProperty> set)
        {
            _memberCollectionMap.Set(set);
            return this;
        }

        public ClassMemberCollectionMap<TObj, TProperty> ClassMap(ClassMap<TProperty> classMap)
        {
            _classMap = classMap;
            return this;
        }

        internal Collection<TObj, TProperty> GetCollection()
        {
            return _memberCollectionMap.GetCollection();
        }

        internal ClassMap<TProperty> GetClassMap()
        {
            if (_classMap == null)
            {
                throw new OXMException("Class map is not set for property.");
            }

            return _classMap;
        }
    }
}
