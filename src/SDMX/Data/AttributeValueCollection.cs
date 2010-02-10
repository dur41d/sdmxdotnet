using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Common;

namespace SDMX
{
    public class AttributeValueCollection : IEnumerable<KeyValuePair<ID, IValue>>
    {
        private Dictionary<ID, IValue> values = new Dictionary<ID, IValue>();
        private KeyFamily _keyFamily;
        private AttachmentLevel _attachmentLevel;

        internal AttributeValueCollection(KeyFamily keyFamily, AttachmentLevel attachmentLevel)
        {
            _keyFamily = keyFamily;
            _attachmentLevel = attachmentLevel;
        }

        public IValue this[ID conceptID]
        {
            get
            {
                return values.GetValueOrDefault(conceptID, null);                
            }
            set
            {
                Contract.AssertNotNull(() => value);

                _keyFamily.ValidateAttribute(conceptID, value, _attachmentLevel);

                values[conceptID] = value;
            }
        }
    
        #region IEnumerable<KeyValuePair<ID,IValue>> Members

        IEnumerator<KeyValuePair<ID, IValue>> IEnumerable<KeyValuePair<ID, IValue>>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
