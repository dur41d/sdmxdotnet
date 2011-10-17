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

        public override string ToXml(T value)
        {
            return toStringMap[value];
        }

        public override T ToObj(string value)
        {
            return fromStringMap[value];
        }
    }
}
