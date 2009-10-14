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
    public class ElementMap<T, TProperty> : IElementMap<T>
    {
        private string _name;
        private bool _required;
        private ClassMap<TProperty> _classMap;
        private Action<TProperty> _setter;
        private Func<T, TProperty> _getter;
        int _occurances;

        public TProperty Value { get; set; }

        public ElementMap(string name, bool required)
        {
            _name = name;
            _required = required;
        }

        public ElementMap<T, TProperty> Parser(ClassMap<TProperty> classMap)
        {
            _classMap = classMap;
            return this;
        }

        public ElementMap<T, TProperty> Getter(Func<T, TProperty> getter)
        {
            _getter = getter;
            return this;
        }

        public ElementMap<T, TProperty> Setter(Action<TProperty> setter)
        {
            _setter = setter;
            return this;
        }

        public void SetValue(XElement element)
        {            
            Contract.AssertNotNull(() => _classMap);
            Contract.AssertNotNull(() => _setter);
            _setter(_classMap.ToObj(element));
            _occurances++;
        }

        public void AssertValid()
        {
            if (_required && _occurances == 0)
            {
                throw new OXMException("Element '{0}' is required but was not found'", _name);
            }
            if (_required && _occurances > 1)
            {
                throw new OXMException("Element '{0}' is supposed to occure only once but occured '{1}' times. Use MapElementCollections instead.", _name, _occurances);
            }
        }

        public void ToXml(XElement element, T parent)
        {
            Contract.AssertNotNull(() => _classMap);
            Contract.AssertNotNull(() => _getter);
            var value = _getter(parent);
            if ((object)value != null)
            {
                XElement child = new XElement(_name);
                _classMap.ToXml(value, child);
                element.Add(child);
            }
        }
    }
}
