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
    public abstract class ClassMap<T>
    {
        private Dictionary<string, IAttributeMap<T>> _attributeMaps = new Dictionary<string, IAttributeMap<T>>();
        private Dictionary<string, IElementMap<T>> _elementMaps = new Dictionary<string, IElementMap<T>>();        
        private IValueMap<T> _valueMap;
        

        protected abstract T CreateObject();

        public void ToXml(T obj, XElement element)
        {
            foreach (var map in _attributeMaps.Values)
            {
                map.ToXml(element, obj);
            }
            foreach (var map in _elementMaps.Values)
            {
                map.ToXml(element, obj);
            }
            if (_valueMap != null)
            {
                _valueMap.ToXml(element, obj);
            }
        }

        public T ToObj(XElement element)
        {
            foreach (var attribute in element.Attributes())
            {
                var map = _attributeMaps.GetValueOrDefault(attribute.Name.LocalName, null);
                if (map == null)
                {
                    throw new OXMException("Attribute not mapped '{0}'", attribute.Name.LocalName);
                }
                map.SetValue(attribute);
            }

            foreach (var a in _attributeMaps.Values)
            {
                a.AssertValid();
            }

            foreach (var childElement in element.Elements())
            {
                var elementMap = _elementMaps.GetValueOrDefault(childElement.Name.LocalName, null);
                if (elementMap == null)
                {
                    throw new OXMException("Element not mapped '{0}'", childElement.Name.LocalName);
                }
                elementMap.SetValue(childElement);
            }
          
            foreach (var e in _elementMaps.Values)
            {
                e.AssertValid();
            }

            if (_valueMap != null)
            {
                _valueMap.SetValue(element);
            }

            T obj = CreateObject();

            foreach (var map in _attributeMaps.Values)
            {
                map.SetProperty(obj);
            }

            foreach (var elementMap in _elementMaps.Values)
            {
                elementMap.SetProperty(obj);
            }

            if (_valueMap != null)
            {
                _valueMap.SetProperty(obj);
            }

            return obj;
        }

        public AttributeMap<T, TProperty> MapAttribute<TProperty>(string name, bool required)
        {
            return MapAttribute<TProperty>(name, required, default(TProperty), false);
        }

        public AttributeMap<T, TProperty> MapAttribute<TProperty>(string name, bool required, TProperty defaultValue)
        {
            return MapAttribute<TProperty>(name, required, defaultValue, true);
        }

        private AttributeMap<T, TProperty> MapAttribute<TProperty>(string name, bool required, TProperty defaultValue, bool hasDefault)
        {
            var attributeMap = new AttributeMap<T, TProperty>(name, required, defaultValue, hasDefault);
            _attributeMaps.Add(name, attributeMap);
            return attributeMap;
        }

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

        public ValueMap<T, TProperty> MapValue<TProperty>()
        {
            var valueMap = new ValueMap<T, TProperty>();
            this._valueMap = valueMap;
            return valueMap;
        }

        public ElementContainerMap<T> MapElementContainer(string name, bool required)
        {
            var container = new ElementContainerMap<T>(name, required);
            _elementMaps.Add(name, container);
            return container;
        }
    }
}
