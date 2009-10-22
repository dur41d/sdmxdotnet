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
        
        Action<TProperty> _setter;
        Func<TObj, TProperty> _getter;

        public MemberMap(Expression<Func<TObj, TProperty>> property)
        {
            _property = property;
            _getter = property.Compile();
        }

        public virtual void Set(Action<TProperty> set)
        {
            _setter = set;
        }

        internal Property<TObj, TProperty> GetProperty()
        {
            return new Property<TObj, TProperty>(_getter, _setter);
        }
    }
}