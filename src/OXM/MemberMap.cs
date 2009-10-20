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
    public class MemberMap<TObj, TProperty>
    {
        Expression<Func<TObj, TProperty>> _property;        
        ISimpleTypeConverter<TProperty> _converter;
        ClassMap<TProperty> _classMap;
        Action<TProperty> _setter;
        Func<TObj, TProperty> _getter;


        public MemberMap(Expression<Func<TObj, TProperty>> property)
        {
            _property = property;
            _getter = property.Compile();
        }

        public MemberMap<TObj, TProperty> Set(Action<TProperty> set)
        {
            _setter = set;
            return this;
        }

        public MemberMap<TObj, TProperty> Converter(ISimpleTypeConverter<TProperty> converter)
        {
            _converter = converter;
            return this;
        }
        public MemberMap<TObj, TProperty> ClassMap(ClassMap<TProperty> classMap)
        {
            _classMap = classMap;
            return this;
        }

        internal Property<TObj, TProperty> GetProperty()
        {
            if (_setter == null)
            {
                throw new OXMException("Setter is not set for property.");
            }

            return new Property<TObj, TProperty>(_getter, _setter);
        }

        internal ISimpleTypeConverter<TProperty> GetConverter()
        {
            if (_converter == null)
            {
                throw new OXMException("Converter is not set for property.");
            }

            return _converter;
        }

        internal ClassMap<TProperty> GetClassMap()
        {
            if (_setter == null)
            {
                throw new OXMException("Class map is not set for property.");
            }

            return _classMap;
        }
    }
}
