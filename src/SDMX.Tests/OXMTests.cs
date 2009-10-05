using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Xml.Linq;

namespace SDMX.Tests
{
    public class DimensionTest
    {
        public Concept Concept { get; set; }
        public bool IsMeasureDimension { get; set; }
    }
    [TestFixture]
    public class OXMTests
    {
        [Test]
        public void ToXml()
        {
            var map = new DimensionMap(new DSD());
            var concept = new Concept("conceptID");
            var dimension = new Dimension(concept);
            dimension.IsMeasureDimension = true;
            
            XElement element = map.ToXml(dimension);

            Assert.IsNotNull(element);
            Assert.IsNotNull(element.Attribute("isMeasureDimension"));
            bool actual = bool.Parse(element.Attribute("isMeasureDimension").Value);
            Assert.AreEqual(dimension.IsMeasureDimension, actual);
        }

        [Test]
        public void ToObj()
        {
            var element = new XElement("Dimension", 
                new XAttribute("conceptRef", "FREQ"),
                new XAttribute("isMeasureDimension", true)
                );
            var map = new DimensionMap(new DSD());            
            var dimension = map.ToObj(element);

            Assert.IsNotNull(dimension);
            Assert.IsNotNull(dimension.Concept);
            Assert.AreEqual("FREQ", dimension.Concept.Id);
            Assert.AreEqual(true, dimension.IsMeasureDimension);
        }
    }


    public class DSD
    {
        public Concept GetConcept(ID concept, ID conceptAgency)
        {
            return new Concept(concept);
        }
    }
}