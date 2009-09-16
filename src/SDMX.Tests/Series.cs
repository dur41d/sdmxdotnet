using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX.Tests
{
    public class Series
    {
        //public SeriesKey Key { get; private set; }
        private Dictionary<Dimension, object> key = new Dictionary<Dimension,object>();

        public Observation Obs { get; set; }
        private DataSet dataSet;

        internal Series(DataSet dataSet)
        {
            this.dataSet = dataSet;
        }

        public void SetKeyValue(string conceptName, string dimensionValue)
        {
            Dimension dimension = dataSet.KeyFamily.GetDimension(conceptName);
            if (dimension == null)
            { 
                throw new SDMXException("Cannot find demension '{0}'".F(conceptName));
            }

            object value = dimension.GetValue(dimensionValue);

            key[dimension] = value;
        }

        public object GetKeyValue(string concept)
        {
            var dimension = dataSet.KeyFamily.GetDimension(concept);
            if (dimension == null)
            {
                throw new SDMXException("dimension doesn't exist in key family");
            }

            object value = key.GetValueOrDefault(dimension, null);

            if (value == null)
            {
                throw new SDMXException("value not found for dimension '{0}'".F(concept));
            }

            return value;
        }

        internal void AddObservation(Observation obs)
        {
            throw new NotImplementedException();
        }
    }
}
