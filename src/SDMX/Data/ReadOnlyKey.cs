using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Collections;

namespace SDMX
{
    public class ReadOnlyKey : IEnumerable<IdValuePair>, IEquatable<ReadOnlyKey>
    {
        private Dictionary<Id, Value> _keyValues;

        int _hash;
        string _string;

        internal ReadOnlyKey(Key key)
        {
            _keyValues = new Dictionary<Id, Value>();
            foreach (var item in key)
            {
                _keyValues[item.Id] = item.Value;
            }
        }

        public virtual Value this[Id concept]
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

        public IEnumerator<IdValuePair> GetEnumerator()
        {
            foreach (var item in _keyValues)
                yield return new IdValuePair(item.Key, item.Value);
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
