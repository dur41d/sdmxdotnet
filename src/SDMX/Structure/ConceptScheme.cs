using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public class ConceptScheme : ItemScheme<Concept>
    {
        public ConceptScheme(ID id, ID agencyID)
            : base(id, agencyID)
        {
        }

        public override Uri Urn
        {
            get
            {
                return new Uri(string.Format("{0}.conceptScheme={1}:{2}[{3}]".F(UrnPrefix, AgencyID, ID, Version)));
            }
        }

        public Concept this[ID conceptID]
        {
            get
            {
                return items[conceptID];
            }
        }
    }
}
