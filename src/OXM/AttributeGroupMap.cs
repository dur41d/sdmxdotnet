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

        public void ReadXml(XElement element)
        {
            TProperty property = GroupTypeMap.ReadXml(element);
            Property.Set(property);
        }

        public void WriteXml(XElement element, TObj obj)
        {
            TProperty property = Property.Get(obj);

            if (_hasDefault && _default.Equals(property))
                return;

            GroupTypeMap.WriteXml(element, property);
        }

        #endregion
    }
}