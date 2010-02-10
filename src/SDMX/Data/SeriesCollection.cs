using System.Collections.Generic;
using System;
using System.Xml.Linq;
using SDMX.Parsers;
using Common;

namespace SDMX
{
    public class SeriesCollection : IEnumerable<Series>
    {
        private Dictionary<Key, Series> _collection = new Dictionary<Key, Series>();
        private DataSet _dataSet;

        internal SeriesCollection(DataSet dataSet)
        {
            _dataSet = dataSet;
        }

        public Series Get(Key key)
        {
            Contract.AssertNotNull(() => key);

            var series = _collection.GetValueOrDefault(key, null);
            if (series == null)
            {
                return new Series(_dataSet, key);
            }

            return series;
        }
        
        public void Add(Series series)
        {
            if (series == null)
            {
                throw new SDMXException("Series is null.");
            }
            if (series.DataSet != _dataSet)
            {
                throw new SDMXException("This series wasn't created for this dataset and thus cannot be added to it.");
            }

            _dataSet.KeyFamily.AssertHasManatoryAttributes(series.Attributes, AttachmentLevel.Series);
            _collection[series.Key] = series;
        }

        public int Count
        {
            get
            {
                return _collection.Count;
            }
        }

       
      

        #region IEnumerable<Series> Members

        public IEnumerator<Series> GetEnumerator()
        {
            foreach (var item in _collection)
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
