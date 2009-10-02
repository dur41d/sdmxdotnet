using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public abstract class MaintainableArtefact : VersionableArtefact
    {
        public ID AgencyID { get; protected set; }
        public bool Final { get; set; }
    }
}
