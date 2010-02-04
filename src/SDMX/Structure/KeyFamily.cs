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
