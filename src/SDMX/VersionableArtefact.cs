using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public abstract class VersionableArtefact : IdentifiableArtefact
    {   
        public string Version { get; protected set; }

        private DateTime? validFrom;
        private DateTime? validTo;   

        /// <summary>
        /// Only Contains the Date component
        /// </summary>
        public DateTime? ValidFrom
        {
            get
            {
                return validFrom;
            }
            set
            {
                validFrom = value.HasValue ? value.Value.Date : value;
            }
        }

        public DateTime? ValidTo
        {
            get
            {
                return validTo;
            }
            set
            {
                validTo = value.HasValue ? value.Value.Date : value;
            }
        }
    }
}
