using System.Collections.Generic;
using System;
using System.Xml.Linq;
using SDMX.Parsers;

namespace SDMX
{
    public class SeriesCollection : IEnumerable<Series>
    {
        private Dictionary<SeriesKey, Series> _collection = new Dictionary<SeriesKey, Series>();
        private DataSet _dataSet;

        internal SeriesCollection(DataSet dataSet)
        {
            _dataSet = dataSet;
        }

        public Series this[SeriesKey key]
        {
            get
            {
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
