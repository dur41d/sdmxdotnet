using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Collections;

namespace SDMX
{
    public class Key : IEnumerable<IdValuePair>, IEquatable<Key>
    {
        private Dictionary<Id, Value> _keyValues;
        private KeyFamily _keyFamily;
        private int _hash;
        private string _toString;

        internal Key(KeyFamily keyFamily)
        {
            _keyValues = new Dictionary<Id, Value>();
            _keyFamily = keyFamily;
        }

        public virtual Value this[Id concept]
        {
            get
            {
                return _keyValues[concept];
            }
            set
            {
                Contract.AssertNotNull(value, "value");
                var dim = _keyFamily.Dimensions.Get(concept);
                dim.Validate(value);
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

        #region IEnumerable<KeyItem> Members

        public IEnumerator<IdValuePair> GetEnumerator()
        {
            foreach (var item in _keyValues)
                yield return new IdValuePair(item.Key, item.Value);
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
            return this.Equals(other, () => GetHashCode() == other.GetHashCode());
        }

        public override bool Equals(object obj)
        {           
            return Equals(obj as Key);
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
            if (_toString == null)
            {
                var builder = new StringBuilder();
                _keyValues.ForEach(k => builder.AppendFormat("{0}={1},", k.Key, k.Value));
                _toString = builder.Remove(builder.Length - 1, 1).ToString();
            }
            return _toString;
        }

        #endregion
    }
}
