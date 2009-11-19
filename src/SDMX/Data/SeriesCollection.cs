using System.Collections.Generic;
using System;
using System.Xml.Linq;
using SDMX.Parsers;

namespace SDMX
{
    public class SeriesCollection : IEnumerable<Series>
    {
        private Dictionary<SeriesKey, Series> series = new Dictionary<SeriesKey, Series>();
        DataSet _dataSet;

        internal SeriesCollection(DataSet dataSet)
        {
            _dataSet = dataSet;
        }

        public Series this[SeriesKey key]
        {
            get
            {
                var s = series.GetValueOrDefault(key, null);
                if (s == null)
                {
                    return new Series(key);
                }

                return s;
            }
        }

        public SeriesKey CreateKey()
        {
            return new SeriesKey(_dataSet);
        }


        #region IEnumerable<Series> Members

        public IEnumerator<Series> GetEnumerator()
        {
            foreach (var item in series)
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
