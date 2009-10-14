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
    public class ElementCollectionMap<T, TProperty> : IElementMap<T>
    {
        private string _name;
        private int _occurances;
        private Func<ClassMap<TProperty>> _classMapConstructor;
        private Action<TProperty> _setter;
        private Func<T, IEnumerable<TProperty>> _getter;
        private bool _required;

        public ElementCollectionMap(string name, bool required)
        {
            _name = name;
            _required = required;            
        }

        public ElementCollectionMap<T, TProperty> Parser(Func<ClassMap<TProperty>> constructor)
        {
            _classMapConstructor = constructor;
            return this;
        }

        public ElementCollectionMap<T, TProperty> Getter(Func<T, IEnumerable<TProperty>> getter)
        {
            _getter = getter;
            return this;
        }

        public ElementCollectionMap<T, TProperty> Setter(Action<TProperty> setter)
        {
            _setter = setter;
            return this;
        }

        public void SetValue(XElement element)
        {            
            Contract.AssertNotNull(() => _classMapConstructor);
            Contract.AssertNotNull(() => _setter);
            var classMap = _classMapConstructor();
            _setter((classMap.ToObj(element)));
            _occurances++;
        }

        public void AssertValid()
        {
            if (_required && _occurances == 0)
            {
                throw new OXMException("MinOccures for element '{0}' is 1 but found 0", _name);
            }
        }
   
        public void ToXml(XElement element, T parent)
        {
            Contract.AssertNotNull(() => _classMapConstructor);
            Contract.AssertNotNull(() => _getter);

            var values = _getter(parent);
            if ((object)values != null)
            {
                foreach (TProperty value in values)
                {
                    XElement child = new XElement(_name);
                    var classMap = _classMapConstructor();
                    classMap.ToXml(value, child);
                    element.Add(child);
                }
            }
        }
    }
}
