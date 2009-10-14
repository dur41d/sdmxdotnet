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
    public class AttributeCollectionMap<T, TProperty> : IAttributeMap<T>
    {
        private List<IAttributeMap<TProperty>> _attributeMaps = new List<IAttributeMap<TProperty>>();
        private List<string> mappedAttributes = new List<string>();

        private Func<T, TProperty> _getter;
        private Action<TProperty> _setter;

        public AttributeMap<TProperty, TAttribute> MapAttribute<TAttribute>(string name, bool required)
        {
            return MapAttribute<TAttribute>(name, required, default(TAttribute), false);
        }

        public AttributeMap<TProperty, TAttribute> MapAttribute<TAttribute>(string name, bool required, TAttribute defaultValue)
        {
            return MapAttribute<TAttribute>(name, required, defaultValue, true);
        }

        private AttributeMap<TProperty, TAttribute> MapAttribute<TAttribute>(string name, bool required, TAttribute defaultValue, bool hasDefault)
        {
            if (mappedAttributes.Exists(item => item == name))
                throw new OXMException("Attribute with name '{0}' already has been mapped.", name);
            else
                mappedAttributes.Add(name);
            
            var attributeMap = new AttributeMap<TProperty, TAttribute>(name, required, defaultValue, hasDefault);
            _attributeMaps.Add(attributeMap);
            return attributeMap;
        }

        public AttributeCollectionMap<T, TProperty> Getter(Func<T, TProperty> getter)
        {
            _getter = getter;
            return this;
        }

        public AttributeCollectionMap<T, TProperty> Setter(Action<TProperty> setter)
        {
            _setter = setter;
            return this;
        }

        public void ToXml(XElement element, T parent)
        {
            Contract.AssertNotNull(() => _getter);

            TProperty property = _getter(parent);

            foreach (var map in _attributeMaps)
            {
                map.ToXml(element, property);
            }
        }

        public void SetValue(XElement element)
        {
            Contract.AssertNotNull(() => _setter);

            foreach (var map in _attributeMaps)
            {
                map.SetValue(element);
            }
        }
    }



    public class AttributeMap<T, TProperty> : IAttributeMap<T>
    {
        private string _name;
        private bool _required;
        private TProperty _default;
        private bool _hasDefault;        

        private Func<T, TProperty> _getter;
        private Func<string, TProperty> _parser;
        private Action<TProperty> _setter;
        private Func<TProperty, string> _toString;

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

        public AttributeMap<T, TProperty> Setter(Action<TProperty> setter)
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

            if (object.Equals(value, null))
            {
                // if the attribute is required through an exception otherwise do nothing
                if (_required)
                {
                    throw new OXMException("Attribute '{0}' is required but its value is null.", _name);
                }
            }
            else
            {
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
        }

        public void SetValue(XElement element)
        {
            Contract.AssertNotNull(() => _parser);
            Contract.AssertNotNull(() => _setter);
            var attribute = element.Attribute(_name);
            if (attribute != null)
            {
                _setter(_parser(attribute.Value));
            }
            else
            {
                if (_required)
                {
                    throw new OXMException("Attribute '{0}' is required but was not found.", _name);
                }
                else if (_hasDefault)
                {                    
                    _setter(_default);
                }
            }
            
        }
    }
}
