using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace SDMX
{
    public class SeriesKeyBuilder
    {
        private Dictionary<ID, DimensionValue> _keyValues = new Dictionary<ID, DimensionValue>();
        private KeyFamily _keyFamily;

        internal SeriesKeyBuilder(DataSet dataSet)
        {
            _keyFamily = dataSet.KeyFamily;
        }

        public void Add(ID conceptID, string value)
        {
            Add(conceptID, value, null);
        }

        public void Add(ID conceptID, string value, string startTime)
        {
            Contract.AssertNotNull(() => conceptID);
            Contract.AssertNotNullOrEmpty(() => value);

            var dimension = _keyFamily.Dimensions.Get(conceptID);
            IValue dimValue = dimension.Parse(value, startTime);

            _keyValues.Add(conceptID, new DimensionValue(dimension, dimValue));
        }

        public SeriesKey Build()
        {
            ValidateKey();
            return new SeriesKey(_keyFamily, _keyValues);
        }

        private void ValidateKey()
        {
            if (_keyValues.Count != _keyFamily.Dimensions.Count())
            {
                throw new SDMXException("Cannot build valid series key '{0}'.", _keyValues.ToString());
            }
            foreach (var dimension in _keyFamily.Dimensions)
            {
                var keyDimension = _keyValues.GetValueOrDefault(dimension.Concept.ID, null);
                if (keyDimension == null)
                {
                    throw new SDMXException("Series key does not contain a value for dimension '{0}'.", dimension.Concept.ID);
                }
            }
        }
    }
}
