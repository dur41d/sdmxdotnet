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
        protected Dictionary<string, AttributeValue> values = new Dictionary<string, AttributeValue>();
        private KeyFamily _keyFamily;
        private AttachmentLevel _attachmentLevel;
        private GroupValue _groupValue;

        internal AttributeValueCollection(KeyFamily keyFamily, AttachmentLevel attachmentLevel)
        {
            _keyFamily = keyFamily;
            _attachmentLevel = attachmentLevel;
        }

        public AttributeValueCollection(KeyFamily keyFamily, AttachmentLevel attachmentLevel, GroupValue groupValue)
            : this(keyFamily, attachmentLevel)
        {
            _groupValue = groupValue;
        }

        public void Add(string concept, string value)
        {
            Contract.AssertNotNull(() => concept);
            Contract.AssertNotNull(() => value);

            if (values.ContainsKey(concept))
            {
                throw new SDMXException("Attribuet '{0}' already exists in the collection.", concept);
            }            
            
            var attribute = _keyFamily.GetAttribute(concept);
            if (attribute.AttachementLevel != _attachmentLevel)
            {
                throw new SDMXException("Invalid attachment level for attribute '{0}'. Expected '{1}' but was '{2}'.",
                    concept, _attachmentLevel, attribute.AttachementLevel);
            }

            object attValue = attribute.GetValue(value);
            values.Add(concept, new AttributeValue(attribute, attValue));

            // we only want to add the group value to the dataset if it has attributes
            // i.e. we want to avoid having groups that have no attributes because only function is to hold
            // attributes. 
            if (_attachmentLevel == AttachmentLevel.Group)
            {
                _groupValue.AddToDataSet();
            }
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
