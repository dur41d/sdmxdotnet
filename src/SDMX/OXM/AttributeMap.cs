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
    public class AttributeMap<T, TProperty> : IAttributeMap<T>
    {
        private string _name;
        private bool _required;
        private TProperty _default;
        private bool _hasDefault;
        private bool _hasValue;
        private TProperty _value;
        private Func<T, TProperty> _getter;
        private Func<string, TProperty> _parser;
        private Action<T, TProperty> _setter;
        private Func<TProperty, string> _toString;

        public TProperty Value
        {
            get
            {
                return _value;
            }
            set
            {
                this._value = value;
                _hasValue = true;
            }
        }

        public AttributeMap(string name, bool required, TProperty defaultValue, bool hasDefault)
        {
            _name = name;
            _required = required;
            _default = defaultValue;
            _hasDefault = hasDefault;
        }

        public AttributeMap<T, TProperty> Getter(Func<T, TProperty> getter)
        {
            _getter = getter;
            return this;
        }

        public AttributeMap<T, TProperty> Setter(Action<T, TProperty> setter)
        {
            _setter = setter;
            return this;
        }

        public AttributeMap<T, TProperty> Parser(Func<string, TProperty> parser)
        {
            _parser = parser;
            return this;
        }

        public AttributeMap<T, TProperty> ToString(Func<TProperty, string> toString)
        {
            _toString = toString;
            return this;
        }

        public void ToXml(XElement element, T parent)
        {
            Contract.AssertNotNull(() => _getter);            

            TProperty value = _getter(parent);

            if (_required && object.Equals(value, null))
            {
                throw new OXMException("Attribute '{0}' is required but its value is null.", _name);
            }

            // if the attribute is optional and the its value is equal to the default value 
            // then don't add it to the element for compactness
            if (!_required && _hasDefault && _default.Equals(value))
                return;

            if (_toString != null)
            {
                element.SetAttributeValue(_name, _toString(value));
            }
            else
            {
                element.SetAttributeValue(_name, value);
            }
        }

        public void SetProperty(T obj)
        {
            if (_setter != null)
            {
                _setter(obj, Value);
            }
        }

        public void SetValue(XAttribute a)
        {
            Contract.AssertNotNull(() => _parser);
            Value = _parser(a.Value);
        }

        public void AssertValid()
        {
            if (!_hasValue)
            {
                if (_required)
                {
                    throw new OXMException("Attribute '{0}' is required but was not found.", _name);
                }
                else if (_hasDefault)
                {
                    Value = _default;
                }
            }
        }
    }
}
