using System;
using System.Collections.Generic;
using System.Linq;

namespace OXM
{
    public class EnumConverter<T> : SimpleTypeConverter<T>
    {
        public override bool TrySerialize(T obj, out string s)
        {
            if ((object)obj == null)
            {
                s = null;
                return false;
            }

            s = obj.ToString();
            return true;
        }

        public override bool TryParse(string s, out T obj)
        {
            Type enumType = typeof(T);
            if (enumType.IsGenericType && enumType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                var nonNullableType = Nullable.GetUnderlyingType(enumType);
                if (Enum.IsDefined(nonNullableType, s))
                {
                    obj = (T)Enum.Parse(nonNullableType, s);
                    return true;
                }
                else
                {
                    obj = default(T);
                    return false;
                }
            }
            else
            {
                if (Enum.IsDefined(enumType, s))
                {
                    obj = (T)Enum.Parse(enumType, s);
                    return true;
                }
                else
                {
                    obj = default(T);
                    return false;
                }
            }
        }
    }
}
