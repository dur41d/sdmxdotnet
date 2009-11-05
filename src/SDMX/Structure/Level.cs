using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public class Level : IdentifiableArtefact
    {
        public Level(int order, ID id)
            : base(id)
        {
            if (order <= 0)
            {
                throw new SDMXException("Order must be more than 0");
            }
            Order = order;
        }


        public int Order { get; private set; }
        public TextFormat CodingType { get; set; }

        public override Uri Urn
        {
            get { throw new NotImplementedException(); }
        }
    }
}
