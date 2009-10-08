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
        public List<TProperty> Values { get; private set; }

        private ClassMap<TProperty> _classMap;
        private Action<T, IList<TProperty>> _setter;
        private Func<T, IEnumerable<TProperty>> _getter;
        private bool _required;

        public ElementCollectionMap(string name, bool required)
        {
            _name = name;
            _required = required;
            Values = new List<TProperty>();
        }

        public ElementCollectionMap<T, TProperty> Parser(ClassMap<TProperty> classMap)
        {
            _classMap = classMap;
            return this;
        }

        public ElementCollectionMap<T, TProperty> Getter(Func<T, IEnumerable<TProperty>> getter)
        {
            _getter = getter;
            return this;
        }

        public ElementCollectionMap<T, TProperty> Setter(Action<T, IList<TProperty>> setter)
        {
            _setter = setter;
            return this;
        }

        public void SetValue(XElement element)
        {            
            Contract.AssertNotNull(() => _classMap);
            Values.Add(_classMap.ToObj(element));
        }

        public void AssertValid()
        {
            if (_required && Values.Count == 0)
            {
                throw new OXMException("MinOccures for element '{0}' is 1 but found 0", _name);
            }
        }

        public void SetProperty(T obj)
        {
            if (_setter != null)
            {
                _setter(obj, Values);
            }
        }

        public void ToXml(XElement element, T parent)
        {
            Contract.AssertNotNull(() => _classMap);
            Contract.AssertNotNull(() => _getter);

            var values = _getter(parent);
            if ((object)values != null)
            {
                foreach (TProperty value in values)
                {
                    XElement child = new XElement(_name);
                    _classMap.ToXml(value, child);
                    element.Add(child);
                }
            }
        }
    }
}
