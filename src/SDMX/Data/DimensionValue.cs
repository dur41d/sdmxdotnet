using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Common;

namespace SDMX
{
    public class DimensionValue
    {
        public Dimension Dimension { get; private set; }
        public object Value { get; private set; }

        public DimensionValue(Dimension dimension, object value)
        {
            Contract.AssertNotNull(() => dimension);
            Contract.AssertNotNull(() => value);

            Dimension = dimension;
            Value = value;
        }
    }
}
