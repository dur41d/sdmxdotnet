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
        //private Dictionary<Id, GroupValueCollection> groups = new Dictionary<Id, GroupValueCollection>();

        internal KeyFamily KeyFamily { get; private set; }     
        public AttributeValueCollection Attributes { get; private set; }

        public DataSet(KeyFamily keyFamily)
        {
            KeyFamily = keyFamily;
            Series = new SeriesCollection(this);
            Attributes = new AttributeValueCollection(keyFamily, AttachmentLevel.DataSet);
        }
        
        public SeriesCollection Series { get; private set; }

        //public GroupValueCollection Group(Id groupId)
        //{
        //    Contract.AssertNotNull(() => groupId);

        //    var groupCollection = groups.GetValueOrDefault(groupId, null);
        //    if (groupCollection == null)
        //    {
        //        var groupDescriptor = KeyFamily.Groups.Where(g => g.Id == groupId).SingleOrDefault();
        //        if (groupDescriptor == null)
        //        {
        //            throw new SDMXException("Invalid group Id '{0}'", groupId);
        //        }

        //        groupCollection = new GroupValueCollection(this, groupDescriptor);

        //        groups.Add(groupId, groupCollection);
        //    }

        //    return groupCollection;
        //}

        //public IEnumerable<GroupValueCollection> Groups()
        //{
        //    return groups.Values.AsEnumerable();
        //}

        public Key NewKey()
        {
            return new Key(KeyFamily);
        }
    }
}
