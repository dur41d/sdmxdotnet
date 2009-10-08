using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Common;

namespace OXM
{
    public class ObjectMap<T1, T2>
    {
        private Dictionary<T1, T2> map1 = new Dictionary<T1, T2>();
        private Dictionary<T2, T1> map2 = new Dictionary<T2, T1>();

        public void Map(T1 value1, T2 value2)
        {
            map1.Add(value1, value2);
            map2.Add(value2, value1);
        }

        public T2 this[T1 value1]
        {
            get
            {
                return map1[value1];
            }
        }

        public T1 this[T2 value2]
        {
            get
            {
                return map2[value2];
            }
        }
    }
}
