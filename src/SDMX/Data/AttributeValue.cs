using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Common;

namespace SDMX
{
    public class AttributeValue
    {
        private AttributeValueCollection _collection;
        private IValue _value;

        public Attribute Attribute { get; private set; }
        
        public IValue Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (!Attribute.IsValid(value))
                {
                    throw new SDMXException("Invalid value '{0}' for attribute '{1}'.", value, Attribute.Concept.ID);   
                }

                _value = value;

                _collection.Include(this);
            }
        }
      
        internal AttributeValue(Attribute attribute, AttributeValueCollection collection)
        {
            Attribute = attribute;
            _collection = collection;
        }

        public override string ToString()
        {
            return _value.ToString();
        }

        public void Parse(string value)
        {
            Contract.AssertNotNullOrEmpty(() => value);
            _value = Attribute.Parse(value);
        }
    }
}
