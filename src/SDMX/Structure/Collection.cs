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

        public void Add(T item)
        {
            Contract.AssertNotNull(item, "item");
            items.Add(item.Id, item);
        }      

        public T TryGet(Id id)
        {
            Contract.AssertNotNull(id, "id");
            return items.GetValueOrDefault(id, default(T));
        }

        public T Get(Id id)
        {
            Contract.AssertNotNull(id, "id");
            T value = default(T);
            if (!items.TryGetValue(id, out value))
            {
                throw new SDMXException("Item not found for concept id '{0}'. Use TryGet or Contains instead.", id);
            }
            return value;
        }

        public void Remove(Id id)
        {
            Contract.AssertNotNull(id, "id");
            items.Remove(id);
        }

        public bool Contains(Id id)
        {
            Contract.AssertNotNull(id, "id");
            return items.ContainsKey(id);
        }

        public int Count
        {
            get
            {
                return items.Count;
            }
        }

        #region IEnumerable<Dimension> Members

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in items)
            {
                yield return item.Value;
            }
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion       
    }
}
