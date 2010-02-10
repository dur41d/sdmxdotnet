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

        public IValue this[ID concept]
        {
            get
            {
                return values.GetValueOrDefault(concept, null);                
            }
            set
            {
                Contract.AssertNotNull(() => value);

                if (value is ID)
                {
                    var att = _keyFamily.Attributes.Get(concept);
                    if (att == null)
                    {
                        throw new SDMXException("Attribute is not found for concept '{0}'.", concept);
                    }
                    if (att.CodeList == null)
                    {
                        throw new SDMXException("Attribute '{0}' does not have code list and thus cannot be assigned a value using id '{1}'."
                            , concept, (ID)value);
                    }
                    value = att.CodeList.Get((ID)value);
                    if (value == null)
                    {
                        throw new SDMXException("Value '{0}' is not found in the code list of attribute '{1}'.",
                            (ID)value, concept);
                    }
                }
                else
                {
                    _keyFamily.ValidateAttribute(concept, value, _attachmentLevel);
                }

                values[concept] = value;
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
