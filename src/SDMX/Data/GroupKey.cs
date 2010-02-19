using System.Collections.Generic;
using System.Linq;
using System;
using System.Xml.Linq;
using SDMX.Parsers;
using Common;

namespace SDMX
{
    //public class GroupKey : IEnumerable<DimensionValue>
    //{
    //    private Dictionary<ID, DimensionValue> _keyValues = new Dictionary<ID, DimensionValue>();
    //    private KeyFamily _keyFamily;        

    //    internal GroupKey(KeyFamily keyFamily, Dictionary<ID, DimensionValue> keyValues)
    //    {
    //        _keyFamily = keyFamily;            
    //        _keyValues = keyValues;
    //    }

    //    public DimensionValue this[ID concept]
    //    {
    //        get
    //        {
    //            Contract.AssertNotNull(() => concept);
    //            return _keyValues.GetValueOrDefault(concept, null);
    //        }
    //    }

    //    #region IEnumerable<DimensionValue> Members

    //    public IEnumerator<DimensionValue> GetEnumerator()
    //    {
    //        foreach (var item in _keyValues)
    //        {
    //            yield return item.Value;
    //        }
    //    }

    //    #endregion

    //    #region IEnumerable Members

    //    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    //    {
    //        return GetEnumerator();
    //    }

    //    #endregion
    //}
}
