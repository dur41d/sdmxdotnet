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
        //private Expression<Func<TObj, TProperty>> _property;
        private Func<TObj, TProperty> _getter;
        private Action<TProperty> _setter;

        public Property(Func<TObj, TProperty> property, Action<TProperty> setter)
        {
            //_property = property;
            _getter = property;
            _setter = setter;
        }

        public TProperty Get(TObj obj)
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
