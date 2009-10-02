using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    
    
    public class Series
    {
        //public SeriesKey Key { get; private set; }

        private Dictionary<object, Observation> observations = new Dictionary<object, Observation>();
        public DataSet DataSet { get; internal set; }
        public SeriesKey Key { get; internal set; }

        public AttributeValues Attributes { get; private set; }
        public IList<Annotation> Annotations { get; private set; }

        internal Series()
        {
            Attributes = new AttributeValues();
            Annotations = new List<Annotation>();
            Key = new SeriesKey(this);
        }

        internal Series(DataSet dataSet)
            : this()
        {
            this.DataSet = dataSet;
        }

        internal void Add(Observation observation)
        {
            if (observations.Keys.Contains(observation.Time))
            {
                throw new SDMXException("observation with time period already exsists '{0}'".F(observation.Time));
            }

            observations.Add(observation.Time, observation);

        }
    }
}