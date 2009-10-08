using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public interface IItemScheme
    { }

    public abstract class ItemScheme<T> : MaintainableArtefact, IItemScheme where T : Item
    {
        public ItemScheme(ID id, ID agencyID)
            : base(id, agencyID)
        {             
        }

        protected Dictionary<string, T> items = new Dictionary<string, T>();        

        public void Add(T item)
        {
            item.ItemScheme = this;
            items[item.Key] = item;
        }

        public void Remove(T item)
        {
            item.ItemScheme = null;
            items.Remove(item.Key);
        }

        public IEnumerable<T> Items
        {
            get
            {
                return items.Values.AsEnumerable();
            }
        }
    }
}
