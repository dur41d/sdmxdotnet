using System.Collections.Generic;
using System.Linq;
using System;
using System.Xml.Linq;
using SDMX.Parsers;
using Common;

namespace SDMX
{
    public class GroupKeyBuilder
    {
        private Dictionary<ID, DimensionValue> _keyValues = new Dictionary<ID, DimensionValue>();
        private KeyFamily _keyFamily;
        private Group _group;

        internal GroupKeyBuilder(DataSet dataSet, Group group)
        {
            _keyFamily = dataSet.KeyFamily;
            _group = group;
        }

        public void Add(ID conceptID, string value)
        {
            Add(conceptID, value, null);
        }

        public void Add(ID conceptID, string value, string startTime)
        {
            Contract.AssertNotNull(() => conceptID);
            Contract.AssertNotNullOrEmpty(() => value);

            var dimension = _keyFamily.GetDimension(conceptID);
            IValue dimValue = dimension.Parse(value, startTime);

            _keyValues.Add(conceptID, new DimensionValue(dimension, dimValue));
        }

        public GroupKey Build()
        {
            ValidateKey();
            return new GroupKey(_keyFamily, _keyValues);
        }

        private void ValidateKey()
        {
            if (_keyValues.Count != _group.Dimensions.Count())
            {
                throw new SDMXException("Cannot build valid group key '{0}'.", _keyValues.ToString());
            }
            foreach (var dimension in _group.Dimensions)
            {
                var keyDimension = _keyValues.GetValueOrDefault(dimension.Concept.ID, null);
                if (keyDimension == null)
                {
                    throw new SDMXException("Group key does not contain a value for dimension '{0}'.", dimension.Concept.ID);
                }
            }
        }
    }
}
