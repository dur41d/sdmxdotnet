using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace SDMX
{
    public class KeyItem
    {
        public ID Concept { get; private set; }
        public string Value { get; private set; }
        public string StartTime { get; set; }
        
        public KeyItem(ID key, string value)
        {            
            Contract.AssertNotNullOrEmpty(() => value);
            Concept = key;
            Value = value;
        }
    }

    public class Key : IEnumerable<KeyItem>
    {
        private Dictionary<string, KeyItem> _keyValues;

        public Key()
        {
            _keyValues = new Dictionary<string, KeyItem>();            
        }

        public string this[string key]
        {
            get
            { 
                Contract.AssertNotNullOrEmpty(() => key);
                return _keyValues[key].Value;
            }
            set
            {                
                _keyValues[key] = new KeyItem((ID)key, value);
            }
        }

        internal void Add(KeyItem keyItem)
        {
            Contract.AssertNotNull(() => keyItem);
            _keyValues.Add(keyItem.Concept.ToString(), keyItem);
        }

        public void SetStartTime(string key, string startTime)
        {
            Contract.AssertNotNullOrEmpty(() => key);
            Contract.AssertNotNullOrEmpty(() => startTime);

            if (!_keyValues.ContainsKey(key))
            {
                throw new SDMXException("key does not exist '{0}'.", key);    
            }

            _keyValues[key].StartTime = startTime;
        }

        public int Count
        {
            get
            {
                return _keyValues.Count;
            }
        }

        #region IEnumerable<KeyItem> Members

        public IEnumerator<KeyItem> GetEnumerator()
        {
            foreach (var item in _keyValues)
                yield return item.Value;
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
    
    public class SeriesKey : IEnumerable<KeyValuePair<Dimension, IValue>>
    {
        private Dictionary<Dimension, IValue> _keyValues;       

        internal SeriesKey()
        {
            _keyValues = new Dictionary<Dimension, IValue>();            
        }

        public IValue this[Dimension dim]
        {
            get
            {
                Contract.AssertNotNull(() => dim);
                return _keyValues[dim];
            }
            set
            {
                Contract.AssertNotNull(() => dim);
                Contract.AssertNotNull(() => value);
                _keyValues[dim] = value;   
            }
        }

        #region IEnumerable<DimensionValue> Members

        public IEnumerator<KeyValuePair<Dimension, IValue>> GetEnumerator()
        {
            foreach (var item in _keyValues)
            {
                yield return item;
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
