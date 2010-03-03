using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Collections;

namespace SDMX
{
    public class ReadOnlyKey : IEnumerable<IDValuePair>, IEquatable<ReadOnlyKey>
    {
        private Dictionary<ID, Value> _keyValues;

        int _hash;
        string _string;

        internal ReadOnlyKey(Key key)
        {
            _keyValues = new Dictionary<ID, Value>();
            foreach (var item in key)
            {
                _keyValues[item.ID] = item.Value;
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
    

        #region IEnumerable<KeyItem> Members

        public IEnumerator<IDValuePair> GetEnumerator()
        {
            foreach (var item in _keyValues)
                yield return new IDValuePair(item.Key, item.Value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion


        #region IEquatable<Key> Members

        public bool Equals(ReadOnlyKey other)
        {
            return this.Equals(other, () => GetHashCode() == other.GetHashCode());
        }

        public override bool Equals(object obj)
        {            
            return Equals(obj as ReadOnlyKey);
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
}
