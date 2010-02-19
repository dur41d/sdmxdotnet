using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Common;

namespace OXM
{
    public interface ISimpleTypeConverter<T>
    {
        string ToXml(T value);
        T ToObj(string value);
    }

    public abstract class SimpleTypeConverterBase<T> : ISimpleTypeConverter<T>
    {
        private Dictionary<T, string> toStringMap = new Dictionary<T, string>();
        private Dictionary<string, T> fromStringMap = new Dictionary<string, T>();

        public void Map(T value, string xmlValue)
        {
            toStringMap.Add(value, xmlValue);
            fromStringMap.Add(xmlValue, value);
        }

        public string ToXml(T value)
        {
            return toStringMap[value];
        }

        public T ToObj(string value)
        {
            return fromStringMap[value];
        }
    }
}
