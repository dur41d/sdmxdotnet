using System;
using System.Collections.Generic;
using System.Linq;

namespace OXM
{
    public abstract class SimpleTypeConverter<T> : ISimpleTypeConverter
    {
        public abstract string ToXml(T obj);
        public abstract T ToObj(string s);

        public virtual bool CanConvertToXml(T obj)
        {
            return true;
        }

        public abstract bool CanConvertToObj(string s);

        string ISimpleTypeConverter.ToXml(object obj)
        {
            T o = Cast(obj);
            return ToXml(o);
        }

        object ISimpleTypeConverter.ToObj(string s)
        {
            return ToObj(s);
        }

        bool ISimpleTypeConverter.CanConvertToXml(object obj)
        {
            T result = default(T);
            if (TryCast(obj, out result))
            {
                return CanConvertToXml(result);
            }
            else
            {
                return false;
            }
        }

        bool ISimpleTypeConverter.CanConvertToObj(string s)
        {
            return CanConvertToObj(s);
        }

        bool IsNullable(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        T Cast(object value)
        {
            string error = null;
            T result = default(T);
            if (!TryCast(value, out result, out error))
            {
                throw new ParseException(error);
            }
            else
            {
                return result;
            }
        }

        bool TryCast(object value, out T result)
        {
            string error = null;
            return TryCast(value, out result, out error);
        }

        bool TryCast(object value, out T result, out string error)
        {
            error = null;
            result = default(T);

            bool isNull = object.ReferenceEquals(value, null);

            if (!isNull && !(value is T))
            {
                error = string.Format("Cannot convert object type '{0}'. Expected object type '{1}'.", value.GetType().Name, typeof(T).Name);
                return false;
            }

            if (isNull)
            {
                Type type = typeof(T);

                // if null and is value type and not nullable then cast will not work
                if (type.IsValueType && !IsNullable(type))
                {
                    error = string.Format("Cannot convert null to type '{0}'.", type.Name);
                    return false;
                }
            }

            result = (T)value;
            return true;
        }
    }
}
