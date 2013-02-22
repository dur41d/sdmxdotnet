using System;
using System.Collections.Generic;
using System.Linq;

namespace OXM
{
    public abstract class SimpleTypeConverter<T> : ISimpleTypeConverter
    {
        public abstract bool TrySerialize(T obj, out string s);
        public abstract bool TryParse(string s, out T obj);

        bool ISimpleTypeConverter.TryParse(string s, out object obj)
        {
            T result = default(T);
            if (TryParse(s, out result))
            {
                obj = result;
                return true;
            }
            else
            {
                obj = null;
                return false;
            }
        }

        bool ISimpleTypeConverter.TrySerialize(object obj, out string s)
        {
            return TrySerialize((T)obj, out s);
        }

        //bool ISimpleTypeConverter.TryParse(string s, out object obj)
        //{
        //    T result = default(T);
        //    if (TryParse(s, out result))
        //    {
        //        obj = result;
        //        return true;
        //    }
        //    else
        //    {
        //        obj = null;
        //        return false;
        //    }

        //}

        //bool ISimpleTypeConverter.TrySerialize(object obj, out string s)
        //{
        //    T result = default(T);
        //    string error = null;
        //    if (TryCast(obj, out result, out error))
        //    {
        //        return TrySerialize(result, out s);
        //    }
        //    else
        //    {
        //        s = null;
        //        return false;
        //    }
        //}


        //bool IsNullable(Type type)
        //{
        //    return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        //}

        //bool TryCast(object value, out T result, out string error)
        //{
        //    error = null;
        //    result = default(T);

        //    bool isNull = object.ReferenceEquals(value, null);

        //    if (!isNull && !(value is T))
        //    {
        //        error = string.Format("Cannot serialize object type '{0}'. Expected object type '{1}'.", value.GetType().Name, typeof(T).Name);
        //        return false;
        //    }

        //    if (isNull)
        //    {
        //        Type type = typeof(T);

        //        // if null and is value type and not nullable then cast will not work
        //        if (type.IsValueType && !IsNullable(type))
        //        {
        //            error = string.Format("Cannot serialize null to type '{0}'.", type.Name);
        //            return false;
        //        }
        //    }

        //    result = (T)value;
        //    return true;
        //}
    }
}
