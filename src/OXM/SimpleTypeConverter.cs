using System;
using System.Collections.Generic;
using System.Linq;

namespace OXM
{
    public abstract class SimpleTypeConverter<T> : ISimpleTypeConverter
    {
        public abstract string ToXml(T value);
        public abstract T ToObj(string value);

        public virtual bool CanConvertToXml(T value)
        {
            return true;
        }

        public virtual bool CanConvertToObj(string value)
        {
            return true;
        }

        string ISimpleTypeConverter.ToXml(object value)
        {
            Type type = typeof(T);
            if (value != null && !(value is T))
                throw new ParseException("Cannot convert object type '{0}'. Expected object type '{1}'."
                    , value.GetType().Name, type.Name);

            if (object.ReferenceEquals(value, null) && type.IsValueType && !IsNullable(type))
                throw new ParseException("Cannot convert null to type '{0}'."
                    , type.Name);


            return ToXml((T)value);
        }

        object ISimpleTypeConverter.ToObj(string value)
        {
            return ToObj(value);
        }

        bool ISimpleTypeConverter.CanConvertToXml(object value)
        {
            return value is T && CanConvertToXml((T)value);
        }

        bool ISimpleTypeConverter.CanConvertToObj(string value)
        {
            return CanConvertToObj(value);
        }

        bool IsNullable(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }
}
