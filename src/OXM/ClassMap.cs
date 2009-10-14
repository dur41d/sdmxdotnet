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
    //public abstract class PropertyMap<T>
    //{
    //    public T Instance { get; set; }
    //    private Dictionary<string, IAttributeMap<T>> _attributeMaps = new Dictionary<string, IAttributeMap<T>>();
    //    private Dictionary<string, IElementMap<T>> _elementMaps = new Dictionary<string, IElementMap<T>>();
    //    private IValueMap<T> _valueMap;

    //    public virtual void ToXml(T obj, XElement element)
    //    {
    //        foreach (var map in _attributeMaps.Values)
    //        {
    //            map.ToXml(element, obj);
    //        }
    //        foreach (var map in _elementMaps.Values)
    //        {
    //            map.ToXml(element, obj);
    //        }
    //        if (_valueMap != null)
    //        {
    //            _valueMap.ToXml(element, obj);
    //        }
    //    }

    //    public T ToObj(XElement element)
    //    {
    //        foreach (var map in _attributeMaps)
    //        {               
    //            map.Value.SetValue(element);
    //        }          

    //        foreach (var childElement in element.Elements())
    //        {
    //            var elementMap = _elementMaps.GetValueOrDefault(childElement.Name.LocalName, null);
    //            if (elementMap == null)
    //            {
    //                throw new OXMException("Element not mapped '{0}'", childElement.Name.LocalName);
    //            }
    //            elementMap.SetValue(childElement);
    //        }

    //        foreach (var e in _elementMaps.Values)
    //        {
    //            e.AssertValid();
    //        }

    //        if (_valueMap != null)
    //        {
    //            _valueMap.SetValue(element);
    //        }

    //        //foreach (var map in _attributeMaps.Values)
    //        //{
    //        //    map.SetProperty(Instance);
    //        //}

    //        //foreach (var elementMap in _elementMaps.Values)
    //        //{
    //        //    elementMap.SetProperty(Instance);
    //        //}

    //        //if (_valueMap != null)
    //        //{
    //        //    _valueMap.SetProperty(Instance);
    //        //}

    //        return Instance;
    //    }

    //    public AttributeMap<T, TProperty> MapAttribute<TProperty>(string name, bool required)
    //    {
    //        return MapAttribute<TProperty>(name, required, default(TProperty), false);
    //    }

    //    public AttributeMap<T, TProperty> MapAttribute<TProperty>(string name, bool required, TProperty defaultValue)
    //    {
    //        return MapAttribute<TProperty>(name, required, defaultValue, true);
    //    }

    //    private AttributeMap<T, TProperty> MapAttribute<TProperty>(string name, bool required, TProperty defaultValue, bool hasDefault)
    //    {
    //        if (_attributeMaps.GetValueOrDefault(name, null) != null)
    //        {
    //            throw new OXMException("Attribute with name '{0}' already has been mapped.", name);
    //        }
    //        var attributeMap = new AttributeMap<T, TProperty>(name, required, defaultValue, hasDefault);
    //        _attributeMaps.Add(name, attributeMap);
    //        return attributeMap;
    //    }

    //    public ElementMap<T, TProperty> MapElement<TProperty>(string name, bool required)
    //    {
    //        var elementMap = new ElementMap<T, TProperty>(name, required);
    //        _elementMaps.Add(name, elementMap);
    //        return elementMap;
    //    }

    //    public ElementCollectionMap<T, TProperty> MapElementCollection<TProperty>(string name, bool required)
    //    {
    //        var elementMap = new ElementCollectionMap<T, TProperty>(name, required);
    //        AddElementMap(name, elementMap);
    //        return elementMap;
    //    }

    //    private void AddElementMap(string name, IElementMap<T> elementMap)
    //    {
    //        if (_elementMaps.GetValueOrDefault(name, null) != null)
    //        {
    //            throw new OXMException("Element with name '{0}' already has been mapped.", name);
    //        }
    //        _elementMaps.Add(name, elementMap);
    //    }

    //    public ValueMap<T, TProperty> MapValue<TProperty>()
    //    {
    //        var valueMap = new ValueMap<T, TProperty>();
    //        this._valueMap = valueMap;
    //        return valueMap;
    //    }

    //    public ElementContainerMap<T> MapElementContainer(string name, bool required)
    //    {
    //        var container = new ElementContainerMap<T>(name, required);
    //        _elementMaps.Add(name, container);
    //        return container;
    //    }
    
    //}

    
    public abstract class ClassMap<T>
    {
        public T Instance { get; set; }
        private List<IAttributeMap<T>> _attributeMaps = new List<IAttributeMap<T>>();
        private Dictionary<string, IElementMap<T>> _elementMaps = new Dictionary<string, IElementMap<T>>();        
        private IValueMap<T> _valueMap;

        private List<string> mappedAttributes = new List<string>();

        public virtual void ToXml(T obj, XElement element)
        {
            foreach (var map in _attributeMaps)
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
            foreach (var attributeMap in _attributeMaps)
            {
                attributeMap.SetValue(element);
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

            //foreach (var map in _attributeMaps.Values)
            //{
            //    map.SetProperty(Instance);
            //}

            //foreach (var elementMap in _elementMaps.Values)
            //{
            //    elementMap.SetProperty(Instance);
            //}

            //if (_valueMap != null)
            //{
            //    _valueMap.SetProperty(Instance);
            //}

            return Instance;
        }

        public AttributeCollectionMap<T, TProperty> MapAttributeCollection<TProperty>()
        {
            var attributeCollection = new AttributeCollectionMap<T, TProperty>();
            _attributeMaps.Add(attributeCollection);
            return attributeCollection;
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
            if (mappedAttributes.Exists(item => item == name))
                throw new OXMException("Attribute with name '{0}' already has been mapped.", name);
            else
                mappedAttributes.Add(name);
            var attributeMap = new AttributeMap<T, TProperty>(name, required, defaultValue, hasDefault);
            _attributeMaps.Add(attributeMap);
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
            AddElementMap(name, elementMap);
            return elementMap;
        }

        private void AddElementMap(string name, IElementMap<T> elementMap)
        {
            if (_elementMaps.GetValueOrDefault(name, null) != null)
            {
                throw new OXMException("Element with name '{0}' already has been mapped.", name);
            }
            _elementMaps.Add(name, elementMap);
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
