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
        }

        private Dictionary<string, Dimension> dimensions = new Dictionary<string, Dimension>();
        private Dictionary<string, Attribute> attributes = new Dictionary<string, Attribute>();
        private Dictionary<ID, Group> groups = new Dictionary<ID,Group>();
        private Dictionary<ID, CrossSectionalMeasure> crossSectionalMeasures = new Dictionary<ID, CrossSectionalMeasure>();

        public TimeDimension TimeDimension { get; internal set; }
        public PrimaryMeasure PrimaryMeasure { get; internal set; }

        public IEnumerable<Dimension> Dimensions
        {
            get
            {
                return dimensions.Values.AsEnumerable();
            }
        }

         public IEnumerable<Group> Groups
        {
            get
            {
                return groups.Values.AsEnumerable();
            }
        }

        public IEnumerable<Attribute> Attributes
        {
            get
            {
                return attributes.Values.AsEnumerable();
            }
        }

        public IEnumerable<CrossSectionalMeasure> CrossSectionalMeasures
        {
            get
            {
                return crossSectionalMeasures.Values.AsEnumerable();
            }
        }

        public Group CreateNewGroup(ID groupID)
        {
            var group = new Group(groupID, this);
            groups.Add(groupID, group);
            return group;
        }

        public void RemoveGroup(ID groupID)
        {		    
		    groups.Remove(groupID);
        }

        public void RemoveGroup(Group group)
        {
            if (group.KeyFamily != this)
            {
                throw new SDMXException("This group belongs to another key family '{0}'.".F(group.KeyFamily.Urn));
            }
            groups.Remove(group.ID);
        }

        public Dimension GetDimension(string conceptName)
        {
            Contract.AssertNotNull(() => conceptName);
            var dimension = dimensions.GetValueOrDefault(conceptName, null);
            if (dimension == null)
            {
                throw new SDMXException("Dimension not found: '{0}'".F(conceptName));
            }
            return dimension;
        }

        public Attribute GetAttribute(string conceptName)
        {
            var attribute = attributes.GetValueOrDefault(conceptName, null);
            if (attribute == null)
            {
                throw new SDMXException("Attribute not found: '{0}'".F(conceptName));
            }
            return attribute;
        }

        public void AddDimension(Dimension dimension)
        {
            dimensions.Add(dimension.Concept.ID, dimension);
        }

        public void AddAttribute(Attribute attribute)
        {
            attributes.Add(attribute.Concept.ID, attribute);
        }

        public void AddMeasure(CrossSectionalMeasure measure)
        {
            crossSectionalMeasures.Add(measure.Concept.ID, measure);
        }


        public static KeyFamily Parse(XDocument dsdXml)
        {
            KeyFamilyParser parser = new KeyFamilyParser();
            DSDDocument dsd = new DSDDocument(dsdXml);
            return parser.Parse(dsd);
        }

        public override Uri Urn
        {
            get 
            {
                return new Uri(string.Format("{0}.keyfamily.KeyFamily={1}:{2}[{3}]".F(UrnPrefix, AgencyID, ID, Version)));
            }
        }
    }
}
