using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Collections;

namespace SDMX
{
    //public class KeyItem
    //{
    //    public ID Concept { get; private set; }
    //    public string Value { get; private set; }
    //    public string StartTime { get; set; }

    //    public KeyItem(ID key, string value)
    //    {            
    //        Contract.AssertNotNullOrEmpty(() => value);
    //        Concept = key;
    //        Value = value;
    //    }
    //}

    public class ReadOnlyKey : IEnumerable<KeyValuePair<ID, Value>>, IEquatable<ReadOnlyKey>
    {
        private Dictionary<ID, Value> _keyValues;

        int _hash;
        string _string;

        internal ReadOnlyKey(Key key)
        {
            _keyValues = new Dictionary<ID, Value>();
            foreach (var item in key)
            {
                _keyValues[item.Key] = item.Value;
            }
        }

        public virtual Value this[ID concept]
        {
            get
            {
                return _keyValues[concept];
            }
        }

        public int Count
        {
            get
            {
                return _keyValues.Count;
            }
        }

        #region IEnumerable<KeyValuePair<ID,IValue>> Members

        public IEnumerator<KeyValuePair<ID, Value>> GetEnumerator()
        {
            foreach (var item in _keyValues)
                yield return item;
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region IEquatable<Key> Members

        public bool Equals(ReadOnlyKey other)
        {
            return GetHashCode() == other.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ReadOnlyKey)) return false;
            return Equals((ReadOnlyKey)obj);
        }

        public override int GetHashCode()
        {
            if (_hash == 0)
            {
                _hash = ToString().GetHashCode();
            }
            return _hash;
        }

        public override string ToString()
        {
            if (_string == null)
            {
                var builder = new StringBuilder();
                _keyValues.ForEach(k => builder.AppendFormat("{0}={1},", k.Key, k.Value));
                _string = builder.Remove(builder.Length - 1, 1).ToString();
            }
            return _string;
        }

        #endregion
    }

    public class Key : IEnumerable<KeyValuePair<ID, Value>>, IEquatable<Key>
    {
        private Dictionary<ID, Value> _keyValues;
        private KeyFamily _keyFamily;

        internal Key(KeyFamily keyFamily)
        {
            _keyValues = new Dictionary<ID, Value>();
            _keyFamily = keyFamily;
        }

        public virtual object this[ID concept]
        {
            get
            {
                return _keyValues[concept];
            }
            set
            {
                Contract.AssertNotNull(value, "value");                               

                if (value is string)
                {
                    value = CodeValue.Create(value as string);
                }

                if (!(value is Value))
                {
                    throw new SDMXException("Key value must be of type 'SDMX.Value'.");
                }              
               
                Value val = (Value)value;                

                var dim = _keyFamily.Dimensions.Get(concept);
                if (dim == null)
                {
                    throw new SDMXException("Dimension is not found for concept '{0}'.", concept);
                }
                dim.Validate(val);


                _keyValues[concept] = val;
            }
        }

        public int Count
        {
            get
            {
                return _keyValues.Count;
            }
        }

        #region IEnumerable<KeyValuePair<ID,IValue>> Members

        public IEnumerator<KeyValuePair<ID, Value>> GetEnumerator()
        {
            foreach (var item in _keyValues)
                yield return item;
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEquatable<Key> Members

        public bool Equals(Key other)
        {
            return GetHashCode() == other.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Key)) return false;
            return Equals((Key)obj);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            _keyValues.ForEach(k => builder.AppendFormat("{0}={1},", k.Key, k.Value));
            return builder.Remove(builder.Length - 1, 1).ToString();
        }

        #endregion
    }

    //public class SeriesKey : IEnumerable<KeyValuePair<ID, IValue>>
    //{
    //    private Dictionary<ID, IValue> _keyValues;
    //    private KeyFamily _keyFamily;

    //    internal SeriesKey(KeyFamily keyFamily)
    //    {
    //        _keyFamily = keyFamily;
    //        _keyValues = new Dictionary<ID, IValue>();            
    //    }    

    //    public IValue this[ID id]
    //    {
    //        get
    //        {                
    //            return _keyValues[id];
    //        }
    //        set
    //        {                
    //            Contract.AssertNotNull(() => value);
    //            _keyValues[id] = value;   
    //        }
    //    }

    //    public int Count
    //    {
    //        get
    //        {
    //            return _keyValues.Count;
    //        }
    //    }

    //    #region IEnumerable<DimensionValue> Members

    //    public IEnumerator<KeyValuePair<ID, IValue>> GetEnumerator()
    //    {
    //        foreach (var item in _keyValues)
    //        {
    //            yield return item;
    //        }
    //    }

    //    #endregion

    //    #region IEnumerable Members

    //    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    //    {
    //        return GetEnumerator();
    //    }

    //    #endregion
    //}
}
