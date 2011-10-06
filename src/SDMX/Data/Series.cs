using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Collections;

namespace SDMX
{   
    public class Series : AnnotableArtefact, IEnumerable<Observation>
    {
        private Dictionary<TimePeriod, Observation> _observations = new Dictionary<TimePeriod, Observation>();
        
        public DataSet DataSet { get; internal set; }
        public ReadOnlyKey Key { get; internal set; }

        public AttributeValueCollection Attributes { get; private set; }      
        
        internal Series(DataSet dataSet, ReadOnlyKey key)
        {               
            dataSet.KeyFamily.ValidateSeriesKey(key);

            Key = key;
            DataSet = dataSet;
            Attributes = new AttributeValueCollection(dataSet.KeyFamily, AttachmentLevel.Series);
        }     

        public Observation TryGet(TimePeriod timePeriod)
        {
            Contract.AssertNotNull(timePeriod, "timePeriod");
            return _observations.GetValueOrDefault(timePeriod, null);
        }

        public Observation Get(TimePeriod timePeriod)
        {
            Contract.AssertNotNull(timePeriod, "timePeriod");
            Observation obs;
            if (!_observations.TryGetValue(timePeriod, out obs))
            {
                throw new SDMXException("Observation with time '{0}' not found. Use TryGet or Contains instead.", timePeriod);
            }
            return obs;
        }

        public Observation Create(TimePeriod timePeriod)
        {
            Contract.AssertNotNull(timePeriod, "timePeriod");           
            return new Observation(this, timePeriod);
        }

        public bool Contains(TimePeriod timePeriod)
        {
            Contract.AssertNotNull(timePeriod, "timePeriod");
            return _observations.ContainsKey(timePeriod);
        }
     
        public void Add(Observation obs)
        {
            Contract.AssertNotNull(obs, "obs");

            if (obs.Series != this)
            {
                SDMXException.Throw("This observation wasn't created for this series and thus cannot be added to it.");
            }
            if (obs.Value == null)
            {
                SDMXException.Throw("Observation value is null. Observation must have a value to be added to a series.");
            }
            if (_observations.ContainsKey(obs.Time))
            {
                SDMXException.Throw("Observation {0} already exists for series {1}.", obs.Time, Key);
            }
            DataSet.KeyFamily.AssertHasManatoryAttributes(obs.Attributes, AttachmentLevel.Observation);
            _observations.Add(obs.Time, obs);
        }

        public int Count
        {
            get { return _observations.Count; }
        }       

        #region IEnumerable Members

        public IEnumerator<Observation> GetEnumerator()
        {
            foreach (var item in _observations)
            {
                yield return item.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}