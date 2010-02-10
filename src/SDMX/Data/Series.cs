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
        private Dictionary<ITimePeriod, Observation> _observations = new Dictionary<ITimePeriod, Observation>();
        
        public DataSet DataSet { get; internal set; }
        public Key Key { get; internal set; }

        public AttributeValueCollection Attributes { get; private set; }

        internal Series(DataSet dataSet, Key key)
        {               
            dataSet.KeyFamily.ValidateSeriesKey(key);

            Key = key;
            DataSet = dataSet;
            Attributes = new AttributeValueCollection(dataSet.KeyFamily, AttachmentLevel.Series);
        }

        //public Observation this[ITimePeriod timePeriod]
        //{
        //    get
        //    {
        //        var obs = _observations.GetValueOrDefault(timePeriod, null);
        //        if (obs == null)
        //        {
        //            return new Observation(this, timePeriod);
        //        }
        //        return obs;
        //    }
        //}

        public Observation Get(ITimePeriod time)
        {
            Contract.AssertNotNull(() => time);

            var obs = _observations.GetValueOrDefault(time, null);
            if (obs == null)
            {
                return new Observation(this, time);
            }

            return obs;
        }

        public void Add(Observation obs)
        {
            Contract.AssertNotNull(() => obs);
            if (obs.Series != this)
            {
                throw new SDMXException("This observation wasn't created for this series and thus cannot be added to it.");
            }
            if (obs.Value == null)
            {
                throw new SDMXException("Observation value is null. Observation must have a value to be added to a series.");
            }
            DataSet.KeyFamily.AssertHasManatoryAttributes(obs.Attributes, AttachmentLevel.Observation);
            _observations[obs.Time] = obs;
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