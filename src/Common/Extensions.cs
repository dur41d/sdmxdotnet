using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Common
{
    public static class Continuation
    {
        public static void Do<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var i in source)
            {
                action(i);
            }
        }
    }

    public static class Hash
    {
        public static int HashWith<T, U>(this T value, U other)
        {
            return 37 ^ value.GetHashCode() ^ other.GetHashCode();
        }
    }
    
    public static class Extensions
    {
        public static string F(this string source, params object[] args)
        {
            return string.Format(source, args);
        }

        public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> source, TKey key, TValue defaultValue)
        {
            TValue result = defaultValue;
            source.TryGetValue(key, out result);
            return result;
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T element in source)
                action(element);
        }

        public static bool ReadNextStartElement(this XmlReader reader)
        {
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                    return true;
            }

            return false;
        }

        public static bool NameEquals(this XmlReader reader, XName name)
        {
            return reader.Name == name.LocalName && reader.NamespaceURI == name.NamespaceName;
        }

        public static XName GetXName(this XmlReader reader)
        {
            XNamespace ns = reader.NamespaceURI;

            int index = reader.Name.IndexOf(':');

            if (index >= 0)
            {
                return ns + reader.Name.Substring(index + 1);
            }
            else
            {
                return ns + reader.Name;
            }
        }

        public static bool Equals<T>(T x, T y) where T : IEquatable<T>
        {
            if (object.ReferenceEquals(x, y)) return true;
            if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null)) return false;
            return x.Equals(y);
        }

        public static bool Equals<T>(this T x, T y, Func<bool> equals) where T : IEquatable<T>
        {
            if (object.ReferenceEquals(y, null)) return false;
            return equals();
        }

    }
}
