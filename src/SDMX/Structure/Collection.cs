using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace SDMX
{
    public interface Item
    {
        ID ID { get; }
    }

    public class Collection<T> : IEnumerable<T> where T : Item
    {
        private Dictionary<ID, T> items = new Dictionary<ID, T>();

        public void Add(T item)
        {
            Contract.AssertNotNull(() => item);
            items.Add(item.ID, item);
        }

        public T Get(ID conceptID)
        {
            Contract.AssertNotNull(() => conceptID);
            return items.GetValueOrDefault(conceptID, default(T));
        }

        public void Remove(ID conceptID)
        {
            Contract.AssertNotNull(() => conceptID);
            items.Remove(conceptID);
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
