using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX.Tests
{
    internal static class Extensions
    {
        internal static string F(this string @this, params object[] args)
        {
            return string.Format(@this, args);
        }

        internal static TValue GetValueOrDefault<TKey,TValue>(this Dictionary<TKey, TValue> @this, TKey key, TValue defaultValue)
        {
            TValue result = defaultValue;
            @this.TryGetValue(key, out result);
            return result;
        }
    }
}
