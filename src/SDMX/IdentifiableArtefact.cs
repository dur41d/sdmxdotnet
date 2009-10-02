using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public abstract class IdentifiableArtefact : AnnotableArtefact
    {
        public ID ID { get; protected set; }
        public Uri Uri { get; protected set; }        
        public InternationalString Name { get; protected set; }
        public InternationalString Description { get; protected set; }
        public abstract Uri Urn { get; }

        protected readonly string UrnPrefix = "urn:sdmx:org.sdmx.infomodel.";
    }
}