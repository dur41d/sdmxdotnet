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
        Func<TObj, TProperty> _property;
        
        XName _name;
        bool _required;
        string _defaultValue;
        
        // for AttributeGroup
        bool _groupHasDefault;
        TProperty _groupDefaultValue;

        bool isAttribute, isAttributeGroup, isContent, isElement, isSimpleElement;
        
        ClassMemberMap<TObj, TProperty> _classMemberMap;
        SimpleMemberMap<TObj, TProperty> _simpleMemberMap;
        AttributeGroupMemberMap<TObj, TProperty> _attributeGroupMap;
        
        public PropertyMap(Func<TObj, TProperty> property)
        {
            _property = property;
        }

        public SimpleMemberMap<TObj, TProperty> ToAttribute(XName name, bool required)
        {
            isAttribute = true;
            
            _name = name;
            _required = required;

            _simpleMemberMap = new SimpleMemberMap<TObj, TProperty>(_property);
            return _simpleMemberMap;
        }

        public SimpleMemberMap<TObj, TProperty> ToAttribute(XName name, bool required, string defaultValue)
        {
            if (String.IsNullOrEmpty(defaultValue))
            {
                throw new ParseException("defaultValue cannot be null or empty.");
            }

            isAttribute = true;
            
            _name = name;
            _required = required;
            _defaultValue = defaultValue;

            _simpleMemberMap = new SimpleMemberMap<TObj, TProperty>(_property);
            return _simpleMemberMap;
        }

        public AttributeGroupMemberMap<TObj, TProperty> ToAttributeGroup(XName groupName)
        {
            isAttributeGroup = true;

            _name = groupName;
            _groupHasDefault = false;

            _attributeGroupMap = new AttributeGroupMemberMap<TObj, TProperty>(_property);
            return _attributeGroupMap;
        }

        public AttributeGroupMemberMap<TObj, TProperty> ToAttributeGroup(XName groupName, TProperty defaultValue)
        {
            isAttributeGroup = true;

            _name = groupName;
            _groupDefaultValue = defaultValue;
            _groupHasDefault = true;

            _attributeGroupMap = new AttributeGroupMemberMap<TObj, TProperty>(_property);
            return _attributeGroupMap;
        }

        public ClassMemberMap<TObj, TProperty> ToElement(XName name, bool required)
        {
            isElement = true;
            
            _name = name;
            _required = required;

            _classMemberMap = new ClassMemberMap<TObj, TProperty>(_property);
            return _classMemberMap;
        }

        public SimpleMemberMap<TObj, TProperty> ToSimpleElement(XName name, bool required)
        {
            isSimpleElement = true;
            
            _name = name;
            _required = required;
            
            _simpleMemberMap = new SimpleMemberMap<TObj, TProperty>(_property);
            return _simpleMemberMap;
        }

        public SimpleMemberMap<TObj, TProperty> ToContent()
        {
            isContent = true;            
            
            _simpleMemberMap = new SimpleMemberMap<TObj, TProperty>(_property);
            return _simpleMemberMap;
        }

        void IMapBuilder<TObj>.BuildMaps(IMapContainer<TObj> map)
        {
            if (map is IAttributeMapContainer<TObj> && isAttribute)
            {
                AttributeMap<TObj, TProperty> attributeMap;
                if (_defaultValue == null)
                {
                    attributeMap = new AttributeMap<TObj, TProperty>(_name, _required, null, false);
                }
                else
                {
                    attributeMap = new AttributeMap<TObj, TProperty>(_name, _required, _defaultValue, true);
                }

                ((IAttributeMapContainer<TObj>)map).AddAttributeMap(_name, attributeMap);
                attributeMap.Property = _simpleMemberMap.GetProperty();
                attributeMap.Converter = _simpleMemberMap.GetConverter();
            }
            else if (map is IAttributeMapContainer<TObj> && isAttributeGroup)
            {
                var attributeGroupMap = new AttributeGroupMap<TObj,TProperty>(_name, _groupDefaultValue, _groupHasDefault);                
                ((IAttributeMapContainer<TObj>)map).AddAttributeMap(_name, attributeGroupMap);
                attributeGroupMap.Property = _attributeGroupMap.GetProperty();
                attributeGroupMap.GroupTypeMap = _attributeGroupMap.GetGroupTypeMap();
            }
            else if (map is IElementMapContainer<TObj> && isElement)
            {
                MapBuilderUtility.QualifyName(ref _name, map.Namespace);
                var elementMap = new ElementMap<TObj, TProperty>(_name, _required);
                ((IElementMapContainer<TObj>)map).AddElementMap(_name, elementMap);
                elementMap.Property = _classMemberMap.GetProperty();
                elementMap.ClassMapFactory = _classMemberMap.GetClassMapFactory();
            }
            else if (map is IElementMapContainer<TObj> && isSimpleElement)
            {
                MapBuilderUtility.QualifyName(ref _name, map.Namespace);
                var simpleElementMap = new SimpleElementMap<TObj, TProperty>(_name, _required);
                ((IElementMapContainer<TObj>)map).AddElementMap(_name, simpleElementMap);
                simpleElementMap.Property = _simpleMemberMap.GetProperty();
                simpleElementMap.ClassMapFactory = () => new SimpleElementClassMap<TProperty>(_simpleMemberMap.GetConverter());
            }
            else if (map is IElementContentContainer<TObj> && isContent)
            {
                var contentMap = new ContentMap<TObj, TProperty>();
                ((IElementContentContainer<TObj>)map).SetElementContentMap(contentMap);
                contentMap.Property = _simpleMemberMap.GetProperty();
                contentMap.Converter = _simpleMemberMap.GetConverter();
            }
            else
            {
                var prop = new Property<TObj, TProperty>(_property, null);                
                throw new ParseException("Error mapping ({0}).{1}: a property must be mapped to either an attribute, an element, or element conent.", prop.GetTypeName(), prop.GetName());
            }
        }
    }
}
