using System.Collections.Generic;

namespace OXM
{
    internal class NameCounter<T>
    {
        Dictionary<T, int> counts = new Dictionary<T, int>();

        public void Increment(T name)
        {
            counts[name] = 1;
        }

        public int Get(T name)
        { 
            return counts.ContainsKey(name) ? 1 : 0;
        }
    }
}
