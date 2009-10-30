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
                if (order.Length != _list.Count)
                {
                    string listTypeName = _list.ElementAt(0).Value.GetType().ToString();                    
                    string mappedList = _list.Aggregate("", (m, l) => m = m + l.Key.LocalName + ",");
                    string orderList = order.Aggregate((item, next) => item = item + "," + next);
                    if (listTypeName.Contains("Attribute"))
                    {
                        throw new OXMException("There are '{0}' attributes mapped but '{1}' have been ordered\r\nMapped: {2}\r\nOrdered: {3}"
                            , _list.Count, order.Length, mappedList, orderList);
                    }
                    else
                    {
                        throw new OXMException("There are '{0}' elements mapped but '{1}' have been ordered\r\nMapped: {2}\r\nOrdered: {3}"
                            , _list.Count, order.Length, mappedList, orderList);
                    }
                }
                var orderedList = order.Join(_list, o => o, e => e.Key.LocalName, (o, e) => e.Value).ToList();
                return orderedList;
            }
        }

        internal IMemberMap<T> Get(XName name)
        {
            var map = _list.GetValueOrDefault(name, null);
            if (map == null)
            {
                throw new OXMException("'{0}' is not Mapped.", name);
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
