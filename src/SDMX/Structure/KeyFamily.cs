using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using SDMX.Parsers;
using Common;

namespace SDMX
{
    public class KeyFamily : MaintainableArtefact
    {
        public KeyFamily(InternationalString name, ID id, ID agencyID)
            : base(id, agencyID)
        {
            Name.Add(name);
            Dimensions = new Collection<Dimension>();
            Attributes = new Collection<Attribute>();
            Groups = new Collection<GroupDescriptor>();
            XMeasures = new Collection<XMeasure>();
        }

        public TimeDimension TimeDimension { get; internal set; }
        public PrimaryMeasure PrimaryMeasure { get; internal set; }
        public Collection<Dimension> Dimensions { get; private set; }
        public Collection<GroupDescriptor> Groups { get; private set; }
        public Collection<Attribute> Attributes { get; private set; }
        public Collection<XMeasure> XMeasures { get; set; }

        public GroupDescriptor CreateNewGroup(ID groupID)
        {
            var group = new GroupDescriptor(groupID, this);
            Groups.Add(group);
            return group;
        }
    
        public override Uri Urn
        {
            get 
            {
                return new Uri(string.Format("{0}.keyfamily.KeyFamily={1}:{2}[{3}]".F(UrnPrefix, AgencyID, ID, Version)));
            }
        }

        internal bool IsValidSeriesKey(Key key, out string reason)
        {
            reason = null;            
            if (key.Count != Dimensions.Count)
            {                
                reason = string.Format("Key items count ({0}) is differnt than the dimension count ({1}).", key.Count, Dimensions.Count);
                return false;
            }
            foreach (var keyItem in key)
            {
                var dim = Dimensions.Get((ID)keyItem.Concept);
                if (dim == null)
                {
                    reason = string.Format("Dimension is not found for key item '{0}'.", keyItem.Concept);
                    return false;
                }

                IValue value = null;
                string parseReason;
                if (!dim.TryParse(keyItem.Value, keyItem.StartTime, out value, out parseReason))
                {
                    reason = "Faild to parse value: '{0}' startTime '{1}'. reason: '{2}'."
                        .F(keyItem.Value, keyItem.StartTime, parseReason);
                    return false;
                }
            }

            return true;
        }
    }
}
