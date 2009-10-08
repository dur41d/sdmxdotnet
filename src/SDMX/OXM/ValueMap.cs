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
    public class ValueMap<T, TProperty> : IValueMap<T>
    {
        public TProperty Value {get; set;}
        private Func<T, TProperty> _getter;
        private Func<string, TProperty> _parser;
        private Action<T, TProperty> _setter;
        private Func<TProperty, string> _toString;

        #region IValueMap<T> Members

        public void SetValue(XElement element)
        {
            Value = _parser(element.Value);
        }

        #endregion

        #region IMap<T> Members

        public void ToXml(XElement element, T parent)
        {
            Contract.AssertNotNull(() => _getter);
            
            TProperty value = _getter(parent);

            if (_toString != null)
            {
                element.Value = _toString(value);
            }
            else
            {
                element.Value = value.ToString();
            }
        }

        public void SetProperty(T parent)
        {
            if (_setter != null)
                _setter(parent, Value);
        }     

        #endregion

        public ValueMap<T, TProperty> Getter(Func<T, TProperty> getter)
        {
            _getter = getter;
            return this;
        }

        public ValueMap<T, TProperty> Setter(Action<T, TProperty> setter)
        {
            _setter = setter;
            return this;
        }

        public ValueMap<T, TProperty> Parser(Func<string, TProperty> parser)
        {
            _parser = parser;
            return this;
        }

        public ValueMap<T, TProperty> ToString(Func<TProperty, string> toString)
        {
            _toString = toString;
            return this;
        }
    }
}
