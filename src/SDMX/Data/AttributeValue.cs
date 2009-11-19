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
        public Attribute Attribute { get; private set; }
        public object Value { get; private set; }

        public AttributeValue(Attribute attribute, object value)
        {
            Contract.AssertNotNull(() => attribute);
            Contract.AssertNotNull(() => value);

            Attribute = attribute;
            Value = value;
        }
    }
}
