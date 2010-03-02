using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Common;

namespace SDMX
{
    public class AttributeValueCollection : IEnumerable<IDValuePair>
    {
        private Dictionary<ID, Value> values = new Dictionary<ID, Value>();
        private KeyFamily _keyFamily;
        private AttachmentLevel _attachmentLevel;

        internal AttributeValueCollection(KeyFamily keyFamily, AttachmentLevel attachmentLevel)
        {
            _keyFamily = keyFamily;
            _attachmentLevel = attachmentLevel;
        }

        public object this[ID concept]
        {
            get
            {
                return values.GetValueOrDefault(concept, null);
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

                _keyFamily.ValidateAttribute(concept, val, _attachmentLevel);
                values[concept] = val;
            }
        }

        public int Count
        {
            get { return values.Count; }
        }

        #region IEnumerable Members

        public IEnumerator<IDValuePair> GetEnumerator()
        {
            foreach (var item in values)
                yield return new IDValuePair(item.Key, item.Value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        #endregion
    }
}
