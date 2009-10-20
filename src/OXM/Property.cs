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
    internal class Property<TObj, TProperty>
    {
        private Func<TObj, TProperty> _getter;
        private Action<TProperty> _setter;

        public Property(Func<TObj, TProperty> getter, Action<TProperty> setter)
        {
            _getter = getter;
            _setter = setter;
        }

        public TProperty Get(TObj obj)
        {
            return _getter(obj);
        }

        public void Set(TProperty value)
        {
            _setter(value);
        }
    }
}
