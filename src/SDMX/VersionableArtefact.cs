using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public abstract class VersionableArtefact : IdentifiableArtefact
    {
        public VersionableArtefact(ID id)
            : base(id)
        {   
        }
        
        public string Version { get; internal set; }

        public ITimePeriod ValidFrom { get; set; }
        public ITimePeriod ValidTo { get; set; }       
    }
}
