using System.Collections.Generic;
using System;
using System.Linq;
using System.Xml.Linq;
using SDMX.Parsers;
using Common;

namespace SDMX
{
    public class DataSet : AnnotableArtefact, IAttachableArtefact
    {
        private Dictionary<ID, GroupValueCollection> groups = new Dictionary<ID, GroupValueCollection>();

        public KeyFamily KeyFamily { get; private set; }     
        public AttributeValueCollection Attributes { get; private set; }

        public DataSet(KeyFamily keyFamily)
        {
            KeyFamily = keyFamily;
            Series = new SeriesCollection(this);
            Attributes = new AttributeValueCollection(keyFamily, AttachmentLevel.DataSet);
        }
        
        public SeriesCollection Series { get; private set; }

        public GroupValueCollection Group(ID groupID)
        {
            Contract.AssertNotNull(() => groupID);

            var groupCollection = groups.GetValueOrDefault(groupID, null);
            if (groupCollection == null)
            {
                var groupDescriptor = KeyFamily.Groups.Where(g => g.ID == groupID).SingleOrDefault();
                if (groupDescriptor == null)
                {
                    throw new SDMXException("Invalid group ID '{0}'", groupID);
                }

                groupCollection = new GroupValueCollection(this, groupDescriptor);

                groups.Add(groupID, groupCollection);
            }

            return groupCollection;
        }

        public IEnumerable<GroupValueCollection> Groups()
        {
            return groups.Values.AsEnumerable();
        }
    }
}
