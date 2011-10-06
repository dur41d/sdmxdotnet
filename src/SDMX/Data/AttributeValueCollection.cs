using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Common;

namespace SDMX
{
    public class AttributeValueCollection : IEnumerable<IdValuePair>
    {
        private Dictionary<Id, Value> values = new Dictionary<Id, Value>();
        private KeyFamily _keyFamily;
        private AttachmentLevel _attachmentLevel;

        internal AttributeValueCollection(KeyFamily keyFamily, AttachmentLevel attachmentLevel)
        {
            _keyFamily = keyFamily;
            _attachmentLevel = attachmentLevel;
        }

        public Value this[Id concept]
        {
            get
            {
                return values.GetValueOrDefault(concept, null);
            }
            set
            {
                Contract.AssertNotNull(value, "value");
                _keyFamily.ValidateAttribute(concept, value, _attachmentLevel);
                values[concept] = value;
            }
        }

        public int Count
        {
            get { return values.Count; }
        }

        #region IEnumerable Members

        public IEnumerator<IdValuePair> GetEnumerator()
        {
            foreach (var item in values)
                yield return new IdValuePair(item.Key, item.Value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        #endregion
    }
}
