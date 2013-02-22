using System;
using System.Collections.Generic;
using System.Linq;

namespace OXM
{
    public abstract class MappingConverterBase<T> : SimpleTypeConverter<T>
    {
        private Dictionary<T, string> toStringMap = new Dictionary<T, string>();
        private Dictionary<string, T> fromStringMap = new Dictionary<string, T>();

        public void Map(T value, string xmlValue)
        {
            toStringMap.Add(value, xmlValue);
            fromStringMap.Add(xmlValue, value);
        }

        public override bool TrySerialize(T obj, out string s)
        {
            return toStringMap.TryGetValue(obj, out s);
        }

        public override bool TryParse(string s, out T obj)
        {
            return fromStringMap.TryGetValue(s, out obj);
        }
    }
}
