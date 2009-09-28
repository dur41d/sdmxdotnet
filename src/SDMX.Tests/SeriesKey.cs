using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX.Tests
{
    public class SeriesKey
    {
        private Series series;
        private object[] key;

        internal SeriesKey(Series series)
        {
            this.series = series;
            key = new object[series.DataSet.KeyFamily.Dimensions.Count()];
        }

        public object this[Dimension dimension]
        {
            get
            {
                return key[dimension.Order];
            }
            set
            {
                if (value == null)
                {
                    throw new SDMXException("value cannot be null");
                }
                if (key[dimension.Order] != null)
                {
                    throw new SDMXException("Key already has value for dimension '{0}'".F(dimension));
                }
                key[dimension.Order] = value;
            }
        }
    }
}
