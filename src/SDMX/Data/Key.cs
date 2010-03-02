using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Collections;

namespace SDMX
{
    public class Key : IEnumerable<IDValuePair>, IEquatable<Key>
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

                var dim = _keyFamily.Dimensions.TryGet(concept);
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

        #region IEnumerable<KeyItem> Members

        public IEnumerator<IDValuePair> GetEnumerator()
        {
            foreach (var item in _keyValues)
                yield return new IDValuePair(item.Key, item.Value);
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
}
