using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace SDMX
{
    public class SeriesKey : IEnumerable<DimensionValue>
    {
        private Dictionary<ID, DimensionValue> _keyValues;
        private KeyFamily _keyFamily;

        internal SeriesKey(KeyFamily keyFamily, Dictionary<ID, DimensionValue> key)
        {
            _keyFamily = keyFamily;
            _keyValues = key;
        }      

        public object this[ID conceptID]
        {
            get
            {
                Contract.AssertNotNull(() => conceptID);                
                return _keyValues[conceptID];
            }          
        }

        #region IEnumerable<DimensionValue> Members

        public IEnumerator<DimensionValue> GetEnumerator()
        {
            foreach (var item in _keyValues)
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
