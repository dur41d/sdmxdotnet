using System.Collections.Generic;
using System;
using System.Xml.Linq;
using SDMX.Parsers;
using Common;

namespace SDMX
{
    public class SeriesCollection : IEnumerable<Series>
    {
        private Dictionary<ReadOnlyKey, Series> _collection = new Dictionary<ReadOnlyKey, Series>();
        private DataSet _dataSet;

        internal SeriesCollection(DataSet dataSet)
        {
            _dataSet = dataSet;
        }

        public Series TryGet(ReadOnlyKey key)
        {
            Contract.AssertNotNull(key, "key");
            return _collection.GetValueOrDefault(key, null);
        }  

        public Series Get(ReadOnlyKey key)
        {
            Contract.AssertNotNull(key, "key");
            Series series;
            if (!_collection.TryGetValue(key, out series))
            {
                throw new SDMXException("Series with key '{0}' not found. Use TryGet or Contains instead.", key);
            }
            return series;
        }       

        public Series Create(ReadOnlyKey key)
        {
            Contract.AssertNotNull(key, "key");
            return new Series(_dataSet, key);
        }

        public bool Contains(ReadOnlyKey key)
        {
            Contract.AssertNotNull(key, "key");
            return _collection.ContainsKey(key);
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
            if (series.Count == 0)
            {
                throw new SDMXException("The series is empty. Series must have at least one observation to be added.");
            }
            if (_collection.ContainsKey(series.Key))
            {
                throw new SDMXException("Series already exists: {0}.", series.Key);
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
