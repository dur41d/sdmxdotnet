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
        private Func<TObj, IEnumerable<TProperty>> _getter;
        private Action<IEnumerable<TProperty>> _setter;

        public Collection(Func<TObj, IEnumerable<TProperty>> getter, Action<IEnumerable<TProperty>> setter)
        {
            _getter = getter;
            _setter = setter;
        }

        public IEnumerable<TProperty> Get(TObj obj)
        {
            return _getter(obj);
        }

        public void Set(IEnumerable<TProperty> value)
        {
            _setter(value);
        }
    }
}
