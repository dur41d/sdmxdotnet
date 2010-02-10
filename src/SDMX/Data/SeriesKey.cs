using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

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

    public class Key : IEnumerable<KeyValuePair<ID, IValue>>, IEquatable<Key>, ICloneable
    {
        private Dictionary<ID, IValue> _keyValues;
        private KeyFamily _keyFamily;

        internal Key(KeyFamily keyFamily)
        {
            _keyValues = new Dictionary<ID, IValue>();
            _keyFamily = keyFamily;
        }

        public IValue this[ID concept]
        {
            get
            {
                return _keyValues[concept];
            }
            set
            {
                Contract.AssertNotNull(() => value);

                if (value is ID)
                {                    
                    var dim = _keyFamily.Dimensions.Get(concept);
                    if (dim == null)
                    {
                        throw new SDMXException("Dimension is not found for concept '{0}'.", concept);
                    }
                    if (dim.CodeList == null)
                    {
                        throw new SDMXException("Dimension '{0}' does not have code list and thus cannot be assigned a value using id '{1}'."
                            , concept, (ID)value);
                    }
                    value = dim.CodeList.Get((ID)value);
                    if (value == null)
                    {
                        throw new SDMXException("Value '{0}' is not found in the code list of dimension '{1}'.",
                            (ID)value, concept);
                    }
                }

                _keyValues[concept] = value;
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

        public IEnumerator<KeyValuePair<ID, IValue>> GetEnumerator()
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
            if (Count != other.Count)
            {
                return false;
            }

            foreach (var item in this)
            {
                var otherValue = other[item.Key];
                if (!item.Value.Equals(otherValue))
                {
                    return false;
                }
            }

            return true;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Key)) return false;
            return Equals((Key)obj);
        }

        public override int GetHashCode()
        {
            int hash = 0;
            _keyValues.ForEach(k => hash = hash ^ 37 ^ k.Key.GetHashCode() ^ k.Value.GetHashCode());
            return hash;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            _keyValues.ForEach(k => builder.AppendFormat("{0}={1},", k.Key, k.Value));
            return builder.Remove(builder.Length -1, 1).ToString();
        }

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            throw new NotImplementedException();
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
