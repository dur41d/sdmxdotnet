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
            XMeasures = new Collection<CrossSectionalMeasure>();
        }

        public TimeDimension TimeDimension { get; internal set; }
        public PrimaryMeasure PrimaryMeasure { get; internal set; }
        public Collection<Dimension> Dimensions { get; private set; }
        public Collection<GroupDescriptor> Groups { get; private set; }
        public Collection<Attribute> Attributes { get; private set; }
        public Collection<CrossSectionalMeasure> XMeasures { get; set; }

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

        internal bool IsValidSeriesKey(ReadOnlyKey key, out string reason)
        {
            reason = null;            
            if (key.Count != Dimensions.Count)
            {                
                reason = string.Format("Key items count ({0}) is differnt than the dimension count ({1}).", key.Count, Dimensions.Count);
                return false;
            }
            foreach (var keyItem in key)
            {
                var dim = Dimensions.Get((ID)keyItem.Key);
                if (dim == null)
                {
                    reason = string.Format("Dimension is not found for key item '{0}'.", keyItem.Key);
                    return false;
                }                
                
                if (!dim.IsValid(keyItem.Value))
                {
                    reason = "Invalid value '{0}' for key '{1}'."
                        .F(keyItem.Value, keyItem.Key);
                    return false;
                }
            }

            return true;
        }

        public void ValidateSeriesKey(ReadOnlyKey key)
        {
            string reason;
            if (!IsValidSeriesKey(key, out reason))
            {
                throw new SDMXException("Invalid series key. reason: {0}, key: {1}", reason, key.ToString());
            }
        }

        internal void Validate(Observation obs)
        {
            if (obs == null)
            {
                throw new SDMXException("Observation is null.");
            }

            AssertHasManatoryAttributes(obs.Attributes, AttachmentLevel.Observation);
        }

        internal void AssertHasManatoryAttributes(AttributeValueCollection attributeValues, AttachmentLevel level)
        {
            foreach (var attribute in Attributes.Where(
                a => a.AssignmentStatus == AssignmentStatus.Mandatory
                && a.AttachementLevel == level))
            {
                if (attributeValues[attribute.Concept.ID] == null)
                {
                    throw new SDMXException("Value for attribute '{0}' is mandatory for the attachment level '{1}' but found missing."
                        , attribute.Concept.ID, level);
                }
            }
        }

        internal void ValidateAttribute(ID conceptID, Value value, AttachmentLevel level)
        {
            var attribute = Attributes.Get(conceptID);
            if (attribute == null)
            {
                throw new SDMXException("Invalid attribute '{0}'.", conceptID);
            }
            if (attribute.AttachementLevel != level)
            {
                throw new SDMXException("Attribute '{0}' has attachment level '{1}' but was attached to level '{2}'."
                    , conceptID, attribute.AttachementLevel, level);
            }
            if (!attribute.IsValid(value))
            {
                throw new SDMXException("Invalid value for attribute '{0}'. Value: {1}."
                    , conceptID, value);
            }
        }     

        internal Component GetComponent(ID id)
        {
            Component com = Dimensions.Get(id);
            if (com == null)
            {
                com = Attributes.Get(id);                
            }

            if (com == null)
            {
                throw new SDMXException("Did not find component with id '{0}'.", id);
            }
            return com;
        }
    }
}
