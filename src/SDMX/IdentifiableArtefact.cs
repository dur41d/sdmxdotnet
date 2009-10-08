using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public abstract class IdentifiableArtefact : AnnotableArtefact
    {
        public IdentifiableArtefact(ID id)
        {
            ID = id;
            Name = new InternationalString();
            Description = new InternationalString();
        }
        
        public ID ID { get; private set; }
        public Uri Uri { get; set; }        
        public InternationalString Name { get; set; }
        public InternationalString Description { get; set; }
        public abstract Uri Urn { get; }

        protected readonly string UrnPrefix = "urn:sdmx:org.sdmx.infomodel.";
    }
}