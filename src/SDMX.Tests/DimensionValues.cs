using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SDMX.Tests
{
    public class DimensionValues : ComponenetValues<Dimension>
    {
        public IEnumerable<Dimension> Dimensions
        {
            get
            {
                return values.Keys.AsEnumerable();
            }
        }
    }
}
