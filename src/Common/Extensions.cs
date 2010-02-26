using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Common
{
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

        public static bool ReadNextElement(this XmlReader reader)
        {
            do
            {
                if (reader.NodeType == XmlNodeType.Element)
                    return true;
            } 
            while (reader.Read());

            return false;
        }

        public static bool NameEquals(this XmlReader reader, XName name)
        {
            return reader.Name == name.LocalName && reader.NamespaceURI == name.NamespaceName;
        }

        public static XName GetXName(this XmlReader reader)
        {
            XNamespace ns = reader.NamespaceURI;
            string localName = reader.Name.Contains(':') ?
                reader.Name.Split(new[] {':'}, 2)[1] : reader.Name;

            XName name = ns + localName;
            return name;
        }

        public static bool Equals<T>(this T x, T y, Func<bool> equals) where T : IEquatable<T>
        {
            if (object.Equals(y, null)) return false;
            return equals();
        }

    }

    //public class Equality<T> where T : class, IEquatable<T>
    //{
    //    public static bool Equals(T x, T y, Func<bool> equals)
    //    {
    //        if (object.Equals(y, null)) return false;
    //        return equals();
    //    }
    //}



}
