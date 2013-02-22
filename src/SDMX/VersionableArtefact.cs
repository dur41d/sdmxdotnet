using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public abstract class VersionableArtefact : IdentifiableArtefact
    {
        public VersionableArtefact(Id id)
            : base(id)
        {   
        }
        
        public string Version { get; internal set; }

        public TimePeriod ValidFrom { get; set; }
        public TimePeriod ValidTo { get; set; }       
    }
}
