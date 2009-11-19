using System.Collections.Generic;
using System;
using System.Xml.Linq;
using System.Linq;
using SDMX.Parsers;
using Common;

namespace SDMX
{
    public class GroupCollection : IEnumerable<GroupValueCollection>
    {
        private Dictionary<ID, GroupValueCollection> groups = new Dictionary<ID, GroupValueCollection>();
        DataSet _dataSet;

        internal GroupCollection(DataSet dataSet)
        {
            _dataSet = dataSet;
        }

        public GroupValueCollection this[ID groupID]
        {
            get
            {
                Contract.AssertNotNull(() => groupID);

                var groupCollection = groups.GetValueOrDefault(groupID, null);
                if (groupCollection == null)
                {
                    var groupDescriptor = _dataSet.KeyFamily.Groups.Where(g => g.ID == groupID).SingleOrDefault();
                    if (groupDescriptor == null)
                    {
                        throw new SDMXException("Invalid group ID '{0}'", groupID);
                    }

                    groupCollection = new GroupValueCollection(_dataSet, groupDescriptor);
                }

                return groupCollection;
            }
        }

        internal void AddToDataSet(ID groupID, GroupValue value)
        {
            var groupValueCollection = groups.GetValueOrDefault(groupID, null);

            if (groupValueCollection == null)
            {

            }
        }

        #region IEnumerable<GroupValueCollection> Members

        public IEnumerator<GroupValueCollection> GetEnumerator()
        {
            foreach (var item in groups)
            {
                yield return item.Value;
            }
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
