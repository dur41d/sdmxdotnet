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
        public bool IsMeasureDimension { get; set; }
    }
    [TestFixture]
    public class OXMTests
    {
        [Test]
        public void ToXml()
        {
            var map = new DimensionMap(new DSD());
            var dimension = new DimensionTest();
            dimension.IsMeasureDimension = true;
            var element = new XElement("Dimension");
            map.ToXml(element, dimension);

            Assert.IsNotNull(element);
            Assert.IsNotNull(element.Attribute("isMeasureDimension"));
            bool actual = bool.Parse(element.Attribute("isMeasureDimension").Value);
            Assert.AreEqual(dimension.IsMeasureDimension, actual);
        }

        [Test]
        public void ToObj()
        {
            var element = new XElement("Dimension", new XAttribute("isMeasureDimension", true));
            var map = new DimensionMap(new DSD());            
            var dimension = new DimensionTest();
            map.ToObj(element, dimension);

            Assert.IsNotNull(dimension);
            Assert.AreEqual(true, dimension.IsMeasureDimension);
        }
    }
}
