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
        //private Func<TObj, IEnumerable<TProperty>> _collection;
        private Func<TObj, IEnumerable<TProperty>> _getter;
        private Action<TProperty> _setter;

        public Collection(Func<TObj, IEnumerable<TProperty>> collection, Action<TProperty> setter)
        {            
            _getter = collection;
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
            return _getter.ToString();
        }

        public string GetName()
        {
            return _getter.ToString();
        }
    }
}
