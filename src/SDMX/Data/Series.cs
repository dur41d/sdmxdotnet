using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{   
    public class Series : AnnotableArtefact, IEnumerable<Observation>
    {
        private Dictionary<TimePeriod, Observation> observations = new Dictionary<TimePeriod, Observation>();
        
        public DataSet DataSet { get; internal set; }
        public SeriesKey Key { get; internal set; }

        public AttributeValueCollection Attributes { get; private set; }

        internal Series()
        {
            Attributes = new AttributeValueCollection(DataSet.KeyFamily, AttachmentLevel.Series);
        }      

        internal Series(SeriesKey key)
            : this()
        {
            Key = key;            
        }

        public Observation this[TimePeriod timePeriod]
        {
            get
            {
                return observations.GetValueOrDefault(timePeriod, null);
            }
        }

        internal void Add(Observation observation)
        {
            if (observations.Keys.Contains(observation.Time))
            {
                throw new SDMXException("observation with time period already exsists '{0}'".F(observation.Time));
            }



            observations.Add(observation.Time, observation);

        }

        #region IEnumerable<Observation> Members

        public IEnumerator<Observation> GetEnumerator()
        {
            foreach (var item in observations)
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