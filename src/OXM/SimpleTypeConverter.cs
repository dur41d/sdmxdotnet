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
            return ToXml((T)value);
        }

        object ISimpleTypeConverter.ToObj(string value)
        {
            return ToObj(value);
        }

        bool ISimpleTypeConverter.CanConvertToXml(object value)
        {
            return CanConvertToXml((T)value);
        }

        bool ISimpleTypeConverter.CanConvertToObj(string value)
        {
            return CanConvertToObj(value);
        }
    }
}
