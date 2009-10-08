using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public abstract class MaintainableArtefact : VersionableArtefact
    {
        public MaintainableArtefact(ID id, ID agencyID)
            : base(id)
        {
            AgencyID = agencyID;
        }
        
        public ID AgencyID { get; private set; }
        public bool IsFinal { get; set; }
    }
}
