using System.Collections.Generic;
using System;
using System.Xml.Linq;
using SDMX.Parsers;
using Common;

namespace SDMX
{
    public class GroupValue : AnnotableArtefact
    {
        public string Type { get; set; }
        public DataSet DataSet { get; internal set; }
        public GroupKey Key { get; internal set; }
        

        public AttributeValueCollection Attributes { get; private set; }
              
        internal GroupValue(GroupKey key)
        {
            Contract.AssertNotNull(() => key);

            if (!key.IsValid())
            {
                throw new SDMXException("Group key is not valid '{0}'.", key);
            }
            
            Attributes = new AttributeValueCollection(DataSet.KeyFamily, AttachmentLevel.Group, this);
            Key = key;            
        }

        internal void AddToDataSet()
        { 
             
        }
    }
}
