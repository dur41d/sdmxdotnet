using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Common;
using System.Runtime.Serialization;

namespace OXM
{
    public class PropertyMap<TObj, TProperty> : IMapBuilder<TObj>
    {
        Expression<Func<TObj, TProperty>> _property;
        XName _name;
        AttributeMap<TObj, TProperty> _attributeMap;
        ElementMap<TObj, TProperty> _elementMap;
        ValueMap<TObj, TProperty> _elementContent;
        MemberMap<TObj, TProperty> _memberMap;
        
        public PropertyMap(Expression<Func<TObj, TProperty>> property)
        {
            _property = property;
        }

        public MemberMap<TObj, TProperty> ToAttribute(XName name, bool required)
        {
            _name = name;
            _attributeMap = new AttributeMap<TObj, TProperty>(name, required, null, false);
            _memberMap = new MemberMap<TObj, TProperty>(_property);
            return _memberMap;
        }

        public MemberMap<TObj, TProperty> ToAttribute(XName name, bool required, string defaultValue)
        {
            _name = name;
            _attributeMap = new AttributeMap<TObj, TProperty>(name, required, defaultValue, true);
            _memberMap = new MemberMap<TObj, TProperty>(_property);
            return _memberMap;
        }

        public MemberMap<TObj, TProperty> ToElement(XName name, bool required)
        {
            _name = name;
            _elementMap = new ElementMap<TObj, TProperty>(name, required);
            _memberMap = new MemberMap<TObj, TProperty>(_property);
            return _memberMap;
        }

        public MemberMap<TObj, TProperty> ToElementContent()
        {
            _elementContent = new ValueMap<TObj, TProperty>();
            _memberMap = new MemberMap<TObj, TProperty>(_property);
            return _memberMap;
        }

        void IMapBuilder<TObj>.BuildMaps(IMapContainer<TObj> map)
        {
            if (map is IAttributeMapContainer<TObj> && _attributeMap != null)
            {
                ((IAttributeMapContainer<TObj>)map).AddAttributeMap(_name, _attributeMap);               
                _attributeMap.SetProperty(_memberMap.GetProperty());
                _attributeMap.SetConverter(_memberMap.GetConverter());
            }
            else if (map is IElementMapContainer<TObj> && _elementMap != null)
            {
                ((IElementMapContainer<TObj>)map).AddElementMap(_name, _elementMap);                
                _elementMap.Property = _memberMap.GetProperty();
                _elementMap.ClassMap = _memberMap.GetClassMap();
            }
            else if (map is IElementContentContainer<TObj> && _elementContent != null)
            {
                ((IElementContentContainer<TObj>)map).SetElementContentMap(_elementContent);
                _elementContent.SetProperty(_memberMap.GetProperty());
                _elementContent.SetConverter(_memberMap.GetConverter());
            }
            else
            {
                throw new OXMException("Error mapping {0}.{1}: a property must be mapped to either an attribute, an element, or element conent.", "ClassName", "PropertyName");
            }
        }
    }
}
