using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SDMX
{
    //public abstract class ComponenetValues<T> where T : Component
    //{
    //    protected Dictionary<T, object> values = new Dictionary<T, object>();

    //    public IEnumerable<object> Values
    //    {
    //        get
    //        {
    //            return values.Values.AsEnumerable();
    //        }
    //    }

    //    public object this[T item]
    //    {
    //        get
    //        {
    //            object value = values.GetValueOrDefault(item, null);

    //            if (values == null)
    //            {
    //                throw new SDMXException("Value for compenent '{0}' is not found".F(item));
    //            }
    //            return value;
    //        }
    //        set
    //        {
    //            if (value == null)
    //            {
    //                throw new SDMXException("value cannot be null");
    //            }

    //            object componentValue = values.GetValueOrDefault(item, null);
    //            if (componentValue != null)
    //            {
    //                throw new SDMXException("Attribute collection already has value for attribute '{0}'".F(item));
    //            }

    //            values[item] = value;
    //        }
    //    }
    //}
}
