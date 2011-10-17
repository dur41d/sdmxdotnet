using System;
using System.Collections.Generic;
using System.Linq;

namespace OXM
{
    public class EnumConverter<T> : SimpleTypeConverter<T>
    {
        public override string ToXml(T value)
        {
            if ((object)value == null)
                return null;

            return value.ToString();
        }

        public override T ToObj(string value)
        {
            Type enumType = typeof(T);
            if (enumType.IsGenericType && enumType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                var nonNullableType = Nullable.GetUnderlyingType(enumType);
                return (T)Enum.Parse(nonNullableType, value);
            }
            else
            {
                return (T)Enum.Parse(typeof(T), value);
            }
        }
    }
}
