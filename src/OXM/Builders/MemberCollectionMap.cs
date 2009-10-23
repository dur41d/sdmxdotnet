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
        Action<IEnumerable<TProperty>> _setter;


        public MemberCollectionMap(Expression<Func<TObj, IEnumerable<TProperty>>> collection)
        {
            _collection = collection;
        }

        public void Set(Action<IEnumerable<TProperty>> set)
        {
            _setter = set;
        }      

        internal Collection<TObj, TProperty> GetCollection()
        {
            return new Collection<TObj, TProperty>(_collection, _setter);
        }        
    }
}
