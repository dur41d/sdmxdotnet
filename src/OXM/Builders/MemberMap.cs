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
        Func<TObj, TProperty> _property;  
        
        Action<TProperty> _setter;        

        public MemberMap(Func<TObj, TProperty> property)
        {
            _property = property;
        }

        public virtual void Set(Action<TProperty> set)
        {
            _setter = set;
        }

        internal Property<TObj, TProperty> GetProperty()
        {
            return new Property<TObj, TProperty>(_property, _setter);
        }
    }
}