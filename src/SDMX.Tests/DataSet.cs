using System.Collections.Generic;

namespace SDMX.Tests
{
    public class DataSet
    {
        public IEnumerable<Series> Series
        {
            get
            {
                return series;
            }            
        }

        private List<Series> series = new List<Series>();
        public KeyFamily KeyFamily { get; private set; }


        public DataSet(KeyFamily keyFamily)
        {
            // TODO: Complete member initialization
            this.KeyFamily = keyFamily;
        }

        public Series CreateEmptySeries()
        {
            var series = new Series(this);
            return series;
        }
    }
}
