using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using SDMX.Parsers;

namespace SDMX
{
    public class KeyFamily : MaintainableArtefact
    {
        public KeyFamily(ID id, ID agencyID)
            : base(id, agencyID)
        {   
        }

        private Dictionary<string, Dimension> dimensions = new Dictionary<string, Dimension>();
        private Dictionary<string, Attribute> attributes = new Dictionary<string, Attribute>();

        public TimeDimension TimeDimension { get; internal set; }
        public PrimaryMeasure PrimaryMeasure { get; internal set; }

        public IEnumerable<Dimension> Dimensions
        {
            get
            {
                return dimensions.Values.AsEnumerable();
            }
        }

        public IEnumerable<Attribute> Attributes
        {
            get
            {
                return attributes.Values.AsEnumerable();
            }
        }

        public Dimension GetDimension(string conceptName)
        {
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
            dimensions.Add(dimension.Concept.Id, dimension);
        }

        public void AddAttribute(Attribute attribute)
        {
            attributes.Add(attribute.Concept.Id, attribute);
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
