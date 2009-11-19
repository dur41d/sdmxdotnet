using System.Collections.Generic;
using System.Linq;
using System;
using System.Xml.Linq;
using SDMX.Parsers;
using Common;

namespace SDMX
{
    public class GroupValueCollection : IEnumerable<GroupValue>
    {
        private Dictionary<GroupKey, GroupValue> groups = new Dictionary<GroupKey, GroupValue>();
        DataSet _dataSet;

        Group _group;
        
        public ID GroupID 
        {
            get
            {
                return _group.ID;
            }
        }

        internal GroupValueCollection(DataSet dataSet, Group group)
        {
            _dataSet = dataSet;
            _group = group;
        }

        public GroupValue this[GroupKey key]
        {
            get
            {   
                var group = groups.GetValueOrDefault(key, null);
                if (group == null)
                {
                    group = new GroupValue(key);
                }

                return group;
            }
        }

        public GroupKey CreateKey()
        {
            return new GroupKey(_dataSet, _group);
        }


        #region IEnumerable<GroupValue> Members

        public IEnumerator<GroupValue> GetEnumerator()
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
