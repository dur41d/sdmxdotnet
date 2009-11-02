using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Common;
using System.Runtime.Serialization;
using System.Xml;

namespace OXM
{
    internal class AttributeGroupMap<TObj, TProperty> : IMemberMap<TObj>
    {
        internal Property<TObj, TProperty> Property { get; set; }

        private AttributeGroupTypeMap<TProperty> _groupTypeMap;
        internal AttributeGroupTypeMap<TProperty> GroupTypeMap
        {
            get
            {
                return _groupTypeMap;
            }
            set
            {
                _groupTypeMap = value;
                _groupTypeMap.Namespace = _groupName.Namespace;
            }
        }

        XName _groupName;
        TProperty _default;
        bool _hasDefault;

        public AttributeGroupMap(XName groupName, TProperty defaultValue, bool hasDefault)
        {
            _groupName = groupName;
            _default = defaultValue;
            _hasDefault = hasDefault;
        }

        #region IMemberMap<TObj> Members

        public void ReadXml(XmlReader reader)
        {
            TProperty property = GroupTypeMap.ReadXml(reader);

            if ((object)property != null)
                Property.Set(property);
        }

        public void WriteXml(XmlWriter writer, TObj obj)
        {
            TProperty property = Property.Get(obj);

            if ((object)property == null)
            {
                if (((IAttributeMap)GroupTypeMap).Required)
                {
                    throw new OXMException("Attribute group '{0}' contains required attributes but its property is null. Property: ({1}).{2}"
                        , _groupName, Property.GetTypeName(), Property.GetName());
                }
            }
            else
            {
                if (_hasDefault && _default.Equals(property))
                    return;

                GroupTypeMap.WriteXml(writer, property);
            }
        }

        #endregion
    }
}