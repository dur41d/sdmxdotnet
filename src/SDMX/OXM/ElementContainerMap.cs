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
    public class ElementContainerMap<T> : IElementMap<T>
    {
        string _name;
        bool _required;
        int _occurances;

        public ElementContainerMap(string name, bool required)
        {
            _name = name;
            _required = required;
        }

        private Dictionary<string, IElementMap<T>> _elementMaps = new Dictionary<string, IElementMap<T>>();

        public ElementMap<T, TProperty> MapElement<TProperty>(string name, bool required)
        {
            var elementMap = new ElementMap<T, TProperty>(name, required);
            _elementMaps.Add(name, elementMap);
            return elementMap;
        }

        public ElementCollectionMap<T, TProperty> MapElementCollection<TProperty>(string name, bool required)
        {
            var elementMap = new ElementCollectionMap<T, TProperty>(name, required);
            _elementMaps.Add(name, elementMap);
            return elementMap;
        }

        public void SetValue(XElement element)
        {
            _occurances++;

            foreach (var childElement in element.Elements())
            {
                var elementMap = _elementMaps.GetValueOrDefault(childElement.Name.LocalName, null);
                if (elementMap == null)
                {
                    throw new OXMException("Element not mapped '{0}'", childElement.Name.LocalName);
                }
                elementMap.SetValue(childElement);
            }
        }

        public void SetProperty(T obj)
        {
            foreach (var elementMap in _elementMaps.Values)
            {
                elementMap.SetProperty(obj);
            }
        }

        public void AssertValid()
        {            
            if (_required && _occurances == 0)
            {
                throw new OXMException("Container Element '{0}' is required but was not found'", _name);
            }
            if (_required && _occurances > 1)
            {
                throw new OXMException("Container Element {0}' is supposed to occure only once but occured '{1}' times. Use MapElementCollections instead.", _name, _occurances);
            }

            foreach (var elementMap in _elementMaps.Values)
            {
                elementMap.AssertValid();
            }
        }

        public void ToXml(XElement element, T parent)
        {
            XElement container = new XElement(_name);
            

            foreach (var map in _elementMaps.Values)
            {
                map.ToXml(container, parent);
            }

            // don't add empty container elements
            if (container.HasElements)
            {
                element.Add(container);
            }
        }
    }
}
