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

        public SimpleMemberMap(Expression<Func<TObj, TProperty>> property)
        {
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
                throw new OXMException("Converter is not set for property.");
            }

            return _converter;
        }

    }
}
