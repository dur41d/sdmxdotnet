using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Common;

namespace OXM
{
    internal class Collection<TObj, TProperty>
    {
        private Expression<Func<TObj, IEnumerable<TProperty>>> _collection;
        private Func<TObj, IEnumerable<TProperty>> _getter;
        private Action<TProperty> _setter;

        public Collection(Expression<Func<TObj, IEnumerable<TProperty>>> collection, Action<TProperty> setter)
        {
            _collection = collection;
            _getter = collection.Compile();
            _setter = setter;
        }

        public IEnumerable<TProperty> Get(TObj obj)
        {
            return _getter(obj);
        }

        public void Set(TProperty value)
        {
            if (_setter != null)
            {
                _setter(value);
            }
        }

        public string GetTypeName()
        {
            return _collection.Parameters[0].Type.ToString();
        }

        public string GetName()
        {
            return _collection.Body.ToString();
        }
    }
}
