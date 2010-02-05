using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{   
    public class Series : AnnotableArtefact, IEnumerable<Observation>
    {
        private Dictionary<ITimePeriod, Observation> _observations = new Dictionary<ITimePeriod, Observation>();
        
        public DataSet DataSet { get; internal set; }
        public Key Key { get; internal set; }

        public AttributeValueCollection Attributes { get; private set; }

        internal Series(DataSet dataSet, Key key)
        {
            Key = key;
            DataSet = dataSet;
            Attributes = new AttributeValueCollection(dataSet.KeyFamily, AttachmentLevel.Series);
        }

        public Observation this[ITimePeriod timePeriod]
        {
            get
            {
                var obs = _observations.GetValueOrDefault(timePeriod, null);
                if (obs == null)
                {
                    return new Observation(this, timePeriod);
                }
                return obs;
            }
        }

        internal void Include(Observation observation)
        {
            _observations[observation.Time] = observation;
        }

        #region IEnumerable<Observation> Members

        public IEnumerator<Observation> GetEnumerator()
        {
            foreach (var item in _observations)
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