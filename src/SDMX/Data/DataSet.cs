using System.Collections.Generic;
using System;
using System.Xml.Linq;
using SDMX.Parsers;
using Common;

namespace SDMX
{
    public class DataSet : AnnotableArtefact, IAttachableArtefact
    {
        public KeyFamily KeyFamily { get; internal set; }     
        public AttributeValueCollection Attributes { get; internal set; }

        public GroupCollection Groups { get; private set; }
        public SeriesCollection Series { get; private set; }

        public DataSet(KeyFamily keyFamily)
        {            
            this.KeyFamily = keyFamily;

            Groups = new GroupCollection(this);
            Series = new SeriesCollection(this);

            Attributes = new AttributeValueCollection(keyFamily, AttachmentLevel.DataSet);
        }
    }
}
