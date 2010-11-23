using OXM;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace SDMX.Parsers
{
    internal static class IEnumerableExtensions
    {
        public static IEnumerable<T> Filter<T>(this IEnumerable list)
        {
            foreach (var item in list)
                if (item is T)
                    yield return (T)item;
        }
    }
}
