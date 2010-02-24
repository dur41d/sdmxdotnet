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
        string _declaringType;

        public MapList(string declaringTypeName)
        {
            _declaringType = declaringTypeName;
        }


        internal void Add(XName name, IMemberMap<T> map)
        {
            if (_list.ContainsKey(name))
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
                var intersection = order.Intersect(_list.Select(i => i.Key.LocalName));

                if (intersection.Count() != order.Length)
                {
                    string listTypeName = _list.ElementAt(0).Value.GetType().ToString();
                    string mappedList = _list.Aggregate("", (m, l) => m = m + l.Key.LocalName + ",");
                    string orderList = order.Aggregate((item, next) => item = item + "," + next);
                    if (listTypeName.Contains("Attribute"))
                    {
                        throw new OXMException("Mapped attribute names are different than the attribute order names in '{0}'. Make sure they are identical.\r\nMapped: {1}\r\nOrdered: {2}"
                           , _declaringType, mappedList, orderList);
                    }
                    else
                    {
                        throw new OXMException("Mapped element names are different than the element order names in '{0}'. Make sure they are identical.\r\nMapped: {1}\r\nOrdered: {2}"
                            , _declaringType , mappedList, orderList);
                    }
                }
        
                var orderedList = order.Join(_list, o => o, e => e.Key.LocalName, (o, e) => e.Value).ToList();
                return orderedList;
            }
        }

        internal IMemberMap<T> Get(XName name)
        {            
            if (!_list.ContainsKey(name))
            {
                throw new OXMException("'{0}' is not Mapped.", name);
            }
            return _list[name];
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
