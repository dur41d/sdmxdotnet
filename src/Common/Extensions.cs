using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public static class Extensions
    {
        public static string F(this string @this, params object[] args)
        {
            return string.Format(@this, args);
        }

        public static TValue GetValueOrDefault<TKey,TValue>(this Dictionary<TKey, TValue> @this, TKey key, TValue defaultValue)
        {
            TValue result = defaultValue;
            @this.TryGetValue(key, out result);
            return result;
        }
    }

    internal class Equality<T> where T : class
    { 
        public static bool Equal(T x, T y)
        {
            if (object.ReferenceEquals(x, y))
                return true;

            if ((object)x == null || (object)y == null)
                return false;

            return x.Equals(y);
        }
    }
}
