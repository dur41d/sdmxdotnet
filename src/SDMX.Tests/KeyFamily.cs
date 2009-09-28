using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX.Tests
{
    public class KeyFamily
    {
        private Dictionary<string, Dimension> dimensions = new Dictionary<string, Dimension>();
        private Dictionary<string, Attribute> attributes = new Dictionary<string, Attribute>();

        public TimeDimension TimeDimension { get; internal set; }
        public PrimaryMeasure PrimaryMeasure { get; internal set; }

        public string ID { get; private set; }

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
    }
}
