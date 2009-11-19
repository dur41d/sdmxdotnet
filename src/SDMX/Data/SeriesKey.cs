using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace SDMX
{
    public class SeriesKey : IEnumerable<DimensionValue>
    {
        //private Series series;
        private object[] key;
        private KeyFamily keyFamily;

        internal SeriesKey(Series series)
        {
            //  this.series = series;
            key = new object[series.DataSet.KeyFamily.Dimensions.Count()];
        }
       
        internal SeriesKey(DataSet dataSet)
        {
            //  this.series = series;
            this.keyFamily = dataSet.KeyFamily;
            key = new object[dataSet.KeyFamily.Dimensions.Count()];
        }

        public void Add(string concept, string value)
        {
            Contract.AssertNotNull(() => concept);
            Contract.AssertNotNull(() => value);

            var dimension = keyFamily.GetDimension(concept);
            object dimValue = dimension.GetValue(value);

            key[dimension.Order] = dimValue;
        }

        public object this[string concept]
        {
            get
            {
                Contract.AssertNotNull(() => concept);
                var dimension = keyFamily.GetDimension(concept);
                return key[dimension.Order];
            }          
        }

        public bool IsValid()
        {
            return !key.Any(i => i == null);
        }

        #region IEnumerable<DimensionValue> Members

        public IEnumerator<DimensionValue> GetEnumerator()
        {
            for (int i = 0; i < key.Length; i++)
            {
                yield return new DimensionValue(keyFamily.Dimensions.ElementAt(i), key[i]);
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
