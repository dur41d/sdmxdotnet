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
    internal class AttributeMap<T, TProperty> : SimpleTypeMap<T, TProperty>
    {
        private XName _name;
        private bool _required;
        private string _default;
        private bool _hasDefault;       

        public AttributeMap(XName name, bool required, string defaultValue, bool hasDefault)
        {
            _name = name;
            _required = required;
            _default = defaultValue;
            _hasDefault = hasDefault;            
        }  
        
        protected override void WriteValue(XElement element, string value)
        {
            if (value == null)
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

                element.SetAttributeValue(_name, value);
            }
        }

        protected override string ReadValue(XElement element)
        {
            var attribute = element.Attribute(_name);
            if (attribute != null)
            {
                return attribute.Value;
            }
            else
            {
                if (_required)
                {
                    throw new OXMException("Attribute '{0}' is required but was not found.", _name);
                }
                else if (_hasDefault)
                {
                    return _default;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
