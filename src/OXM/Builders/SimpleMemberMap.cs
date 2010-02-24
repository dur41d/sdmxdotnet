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
    public class SimpleMemberMap<TObj, TProperty>
    {
        ISimpleTypeConverter<TProperty> _converter;
        MemberMap<TObj, TProperty> _memberMap;

        Func<TObj, TProperty> _prop;

        public SimpleMemberMap(Func<TObj, TProperty> property)
        {
            _prop = property;
            _memberMap = new MemberMap<TObj, TProperty>(property);
        }

        public SimpleMemberMap<TObj, TProperty> Set(Action<TProperty> set)
        {
            _memberMap.Set(set);
            return this;
        }

        public void Converter(ISimpleTypeConverter<TProperty> converter)
        {
            _converter = converter;
        }

        internal Property<TObj, TProperty> GetProperty()
        {
            return _memberMap.GetProperty();
        }

        internal ISimpleTypeConverter<TProperty> GetConverter()
        {
            if (_converter == null)
            {
                var prop = new Property<TObj, TProperty>(_prop, null);
                throw new OXMException("Converter is not set for property ({0}).{1}.", prop.GetTypeName(), prop.GetName());
            }

            return _converter;
        }

    }
}
