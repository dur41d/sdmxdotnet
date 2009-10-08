using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }

    //internal class Equality<T> where T : class
    //{ 
    //    public static bool Equal(T x, T y)
    //    {
    //        if (object.ReferenceEquals(x, y))
    //            return true;

    //        if ((object)x == null || (object)y == null)
    //            return false;

    //        return x.Equals(y);
    //    }
    //}


}
