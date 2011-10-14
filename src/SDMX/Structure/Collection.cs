using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace SDMX
{
    public class Collection<T> : IEnumerable<T> where T : Item
    {
        private Dictionary<Id, T> items = new Dictionary<Id, T>();

        /// <summary>
        /// Add item to the collection.
        /// </summary>
        public void Add(T item)
        {
            Contract.AssertNotNull(item, "item");
            items.Add(item.Id, item);
        }

        /// <summary>
        /// Find item by id. Returns null if item is not found.
        /// </summary>
        public T Find(Id id)
        {
            Contract.AssertNotNull(id, "id");
            return items.GetValueOrDefault(id, default(T));
        }

        /// <summary>
        /// Get item by id. Throws exception if item is not found.
        /// </summary>
        public T Get(Id id)
        {
            Contract.AssertNotNull(id, "id");
            T value = default(T);
            if (!items.TryGetValue(id, out value))
            {
                throw new SDMXException("Item not found for concept id '{0}'. Use Find or Contains instead.", id);
            }
            return value;
        }

        /// <summary>
        /// Remove item by id.
        /// </summary>
        public void Remove(Id id)
        {
            Contract.AssertNotNull(id, "id");
            items.Remove(id);
        }

        /// <summary>
        /// Tests if item exists in collection.
        /// </summary>
        public bool Contains(Id id)
        {
            Contract.AssertNotNull(id, "id");
            return items.ContainsKey(id);
        }

        /// <summary>
        /// Number of items in the collection.
        /// </summary>
        public int Count
        {
            get
            {
                return items.Count;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in items)
            {
                yield return item.Value;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        } 
    }
}
