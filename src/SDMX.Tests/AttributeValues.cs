using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SDMX.Tests
{
    public class AttributeValues : ComponenetValues<Attribute>
    {
        public IEnumerable<Attribute> Attributes
        {
            get
            {
                return values.Keys.AsEnumerable();
            }
        }
    }
}
