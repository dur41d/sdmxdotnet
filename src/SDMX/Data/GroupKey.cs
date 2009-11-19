using System.Collections.Generic;
using System.Linq;
using System;
using System.Xml.Linq;
using SDMX.Parsers;
using Common;

namespace SDMX
{
    public class GroupKey : IEnumerable<DimensionValue>
    {
        private Dictionary<string, DimensionValue> _values = new Dictionary<string, DimensionValue>();
        private KeyFamily _keyFamily;
        internal Group Group { get; set; }

        internal GroupKey(DataSet dataSet, Group group)
        {
            Contract.AssertNotNull(() => dataSet);
            Contract.AssertNotNull(() => group);
                        
            _keyFamily = dataSet.KeyFamily;
            Group = group;
        }

        public void Add(string concept, string value)
        {
            Contract.AssertNotNull(() => concept);
            Contract.AssertNotNull(() => value);

            var dimension = _keyFamily.GetDimension(concept);
            object dimValue = dimension.GetValue(value);

            _values.Add(concept, new DimensionValue(dimension, dimValue));
        }

        public object this[string concept]
        {
            get
            {
                Contract.AssertNotNull(() => concept);
                var dimValue = _values.GetValueOrDefault(concept, null);

                if (dimValue == null)
                {
                    throw new SDMXException("Group key does not contain dimension '{0}'", concept);
                }

                return dimValue.Value;
            }
        }

        public bool IsValid()
        {
            return Group.Dimensions.Count() == _values.Count;
        }

        #region IEnumerable<DimensionValue> Members

        public IEnumerator<DimensionValue> GetEnumerator()
        {
            foreach (var item in _values)
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
