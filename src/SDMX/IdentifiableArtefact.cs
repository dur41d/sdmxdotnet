using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public abstract class IdentifiableArtefact : AnnotableArtefact
    {
        public IdentifiableArtefact(Id id)
        {
            Id = id;
            Name = new InternationalText();
            Description = new InternationalText();
        }
        
        public Id Id { get; private set; }
        public Uri Uri { get; set; }        
        public InternationalText Name { get; set; }
        public InternationalText Description { get; set; }
        public abstract Uri Urn { get; }

        protected readonly string UrnPrefix = "urn:sdmx:org.sdmx.infomodel.";
    }
}