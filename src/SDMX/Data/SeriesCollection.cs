using System.Collections.Generic;
using System;
using System.Xml.Linq;
using SDMX.Parsers;

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

        //public Series this[SeriesKey key]
        //{
        //    get
        //    {
        //        var series = _collection.GetValueOrDefault(key, null);
        //        if (series == null)
        //        {
        //            series = new Series(_dataSet, key);
        //            _collection.Add(key, series);
        //        }

        //        return series;
        //    }
        //}

        public Series this[Key key]
        {
            get
            {
                string reason;
                if (!_dataSet.KeyFamily.IsValidSeriesKey(key, out reason))
                {
                    throw new SDMXException("Invalid series key. reason: {0}, key: {1}", reason, key.ToString());
                }
                
                var series = _collection.GetValueOrDefault(key, null);
                if (series == null)
                {
                    series = new Series(_dataSet, key);
                    _collection.Add(key, series);
                }

                return series;
            }
        }

        public SeriesKeyBuilder CreateKeyBuilder()
        {
            return new SeriesKeyBuilder(_dataSet);
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
