using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Common;

namespace SDMX
{
    public class AttributeValueCollection : IEnumerable<AttributeValue>
    {
        private Dictionary<ID, AttributeValue> values = new Dictionary<ID, AttributeValue>();
        private KeyFamily _keyFamily;
        private AttachmentLevel _attachmentLevel;

        internal AttributeValueCollection(KeyFamily keyFamily, AttachmentLevel attachmentLevel)
        {
            _keyFamily = keyFamily;
            _attachmentLevel = attachmentLevel;
        }

        public AttributeValue this[ID conceptID]
        {
            get
            {
                Contract.AssertNotNull(() => conceptID);

                var attributeValue = values.GetValueOrDefault(conceptID, null);
                if (attributeValue == null)
                {
                    var attribute = _keyFamily.Attributes.Get(conceptID);

                    if (attribute.AttachementLevel != _attachmentLevel)
                    {
                        throw new SDMXException("Attribute '{0}' has attachment level '{1}' and not '{2}'.",
                            conceptID, attribute.AttachementLevel, _attachmentLevel);
                    }

                    return new AttributeValue(attribute, this);
                }

                return attributeValue;
            }
        }

        internal void Include(AttributeValue attributeValue)
        {
            values[attributeValue.Attribute.Concept.ID] = attributeValue;
        }

        #region IEnumerable<Attribute> Members

        public IEnumerator<AttributeValue> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
