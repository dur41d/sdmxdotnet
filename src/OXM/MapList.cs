using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Common;
using System.Runtime.Serialization;

namespace OXM
{
    internal class MapList<T> : IEnumerable<IMemberMap<T>>
    {
        private Dictionary<XName, IMemberMap<T>> _list = new Dictionary<XName, IMemberMap<T>>();

        internal void Add(XName name, IMemberMap<T> map)
        {
            if (_list.GetValueOrDefault(name, null) != null)
            {
                throw new OXMException("'{0}' has been already mapped.".F(name));
            }
            _list.Add(name, map);
        }

        internal IEnumerable<IMemberMap<T>> GetOrderedList(string[] order)
        {
            // order is optional            
            if (order == null)
            {
                return _list.Values.AsEnumerable();
            }
            else
            {
                return order.Join(_list, o => o, e => e.Key, (o, e) => e.Value);
            }
        }

        internal IMemberMap<T> Get(XName name)
        {
            var map = _list.GetValueOrDefault(name, null);
            if (map == null)
            {
                throw new OXMException("Map not found '{0}'.", name);
            }
            return map;
        }

        #region IEnumerable<T> Members

        public IEnumerator<IMemberMap<T>> GetEnumerator()
        {
            return _list.Values.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _list.Values.GetEnumerator();
        }

        #endregion
    }
}
