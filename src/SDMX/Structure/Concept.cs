using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public class Concept : MaintainableArtefact
    {        
        public Concept(InternationalString name, ID id, ID agencyID)
            : base(id, agencyID)
        {
            Name.Add(name);
        }

        public ConceptScheme ConceptScheme { get; internal set; }
        public Concept Parent { get; set; }
        public TextFormat TextFormat { get; set; }

        public override Uri Urn
        {
            get
            {
                return new Uri(string.Format("{0}.conceptScheme.Concept={1}:{2}.{3}[{4}]".F(UrnPrefix, ConceptScheme.AgencyID, ConceptScheme.ID, ID, ConceptScheme.Version)));
            }
        }
    }
}
