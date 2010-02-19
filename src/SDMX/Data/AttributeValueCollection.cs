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
                    value = ID.Create(value as string);
                }

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
                    var code = att.CodeList.Get((ID)value);
                    if (code == null)
                    {
                        throw new SDMXException("Value '{0}' is not found in the code list of attribute '{1}'.",
                            (ID)value, concept);
                    }
                    value = code;
                }
                else
                {
                    if (!(value is IValue))
                    {
                        throw new SDMXException("Value must be IValue or ID type.");
                    }
                    _keyFamily.ValidateAttribute(concept, (IValue)value, _attachmentLevel);
                }

               

                values[concept] = (IValue)value;
            }
        }

        public int Count
        {
            get { return values.Count; }
        }
    
        #region IEnumerable Members

        public IEnumerator<KeyValuePair<ID, IValue>> GetEnumerator()
        {
            foreach (var item in values)
                yield return item;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        #endregion
    }
}
