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
        Func<ClassMap<TProperty>> _classMapFactory;
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

        public ClassMemberCollectionMap<TObj, TProperty> ClassMap(Func<ClassMap<TProperty>> classMapConstructor)
        {
            _classMapFactory = classMapConstructor;
            return this;
        }

        internal Collection<TObj, TProperty> GetCollection()
        {
            return _memberCollectionMap.GetCollection();
        }

        internal Func<ClassMap<TProperty>> GetClassMapFactory()
        {
            if (_classMapFactory == null)
            {
                throw new OXMException("Class map is not set for property.");
            }

            return _classMapFactory;
        }
    }
}
