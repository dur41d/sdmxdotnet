using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Xml.Linq;
using SDMX.Parsers;

namespace SDMX.Tests
{
    [TestFixture]
    public class OXMTests
    {
        [Test]
        public void ToXml()
        {
            var map = new DimensionMap(new DSD());
            var concept = new Concept("conceptID", "agencyID");
            var dimension = new Dimension(concept);
            dimension.IsMeasureDimension = true;
            dimension.TextFormat = new TextFormat();
            dimension.TextFormat.TextType = TextType.Double;

            XElement element = new XElement("Dimension");
            map.ToXml(dimension, element);

            Assert.IsNotNull(element);
            Assert.IsNotNull(element.Attribute("isMeasureDimension"));
            bool actual = bool.Parse(element.Attribute("isMeasureDimension").Value);
            Assert.AreEqual(dimension.IsMeasureDimension, actual);
            var textFormatElement = element.Element("TextFormat");
            Assert.IsNotNull(textFormatElement);
            Assert.IsNotNull(textFormatElement.Attribute("textType"));
            Assert.AreEqual("Double", textFormatElement.Attribute("textType").Value);
        }

        [Test]
        public void ToObj()
        {
            var element = new XElement("Dimension",
                new XAttribute("conceptRef", "FREQ"),
                new XAttribute("isMeasureDimension", true),
                new XElement("TextFormat",
                    new XAttribute("textType", "Double"))
                );

            var map = new DimensionMap(new DSD());

            var dimension = map.ToObj(element);

            Assert.IsNotNull(dimension);
            Assert.IsNotNull(dimension.Concept);
            Assert.AreEqual("FREQ", dimension.Concept.ID);
            Assert.AreEqual(true, dimension.IsMeasureDimension);
            Assert.IsNotNull(dimension.TextFormat);
            Assert.IsTrue(dimension.TextFormat.TextType == TextType.Double);
        }


        [Test]
        public void KeyFamily_ToObj()
        {
            var element =
                new XElement("KeyFamily",
                    new XElement("Components",
                        new XElement("Dimension", new XAttribute("conceptRef", "FREQ"), new XAttribute("isMeasureDimension", true),
                            new XElement("TextFormat", new XAttribute("textType", "Double"))),
                        new XElement("Dimension", new XAttribute("conceptRef", "REF_AREA"))));

            var map = new KeyFamilyMap(new DSD());

            var keyFamily = map.ToObj(element);

            Assert.IsNotNull(keyFamily);
            Assert.AreEqual(2, keyFamily.Dimensions.Count());
            var dimension = keyFamily.GetDimension("FREQ");
            Assert.IsNotNull(dimension);
            Assert.AreEqual("FREQ", dimension.Concept.ID);
            Assert.AreEqual(true, dimension.IsMeasureDimension);
            Assert.IsNotNull(dimension.TextFormat);
            Assert.IsTrue(dimension.TextFormat.TextType == TextType.Double);
            dimension = keyFamily.GetDimension("REF_AREA");
            Assert.NotNull(dimension);
        }

        [Test]
        public void KeyFamily_ToXml()
        {
            var keyFamily = new KeyFamily("KeyID", "agencyID");
            var dimension = new Dimension(new Concept("FREQ", "agencyID"));
            dimension.TextFormat = new TextFormat();
            dimension.TextFormat.TextType = TextType.Double;
            keyFamily.AddDimension(dimension);
            dimension = new Dimension(new Concept("REF_AREA", "agencyID"));
            keyFamily.AddDimension(dimension);

            var annotation = new Annotation();
            annotation.Title = "Anno Title";
            annotation.Type = "Internal";
            annotation.Text[Language.English] = "English Text";

            keyFamily.Annotations.Add(annotation);

            var map = new KeyFamilyMap(new DSD());

            var element = new XElement("KeyFamily");
            map.ToXml(keyFamily, element);

            Assert.NotNull(element.Element("Components"));
            Assert.AreEqual(2, element.Element("Components").Elements("Dimension").Count());
            
        }

        [Test]
        public void GenericSampleKeyFamily_ToObj()
        {
            string dsdPath = Utility.GetPathFromProjectBase("lib\\StructureSample.xml");
            XDocument dsdXml = XDocument.Load(dsdPath);

            var keyFamilyElement = (from e in dsdXml.Descendants()
                                  where e.Name.LocalName == "KeyFamily"
                                  select e).Single();

            var map = new KeyFamilyMap(new DSD(dsdXml));

            var keyFamily = map.ToObj(keyFamilyElement);

            var keyFamilyElement2 = new XElement("KeyFamily");
            map.ToXml(keyFamily, keyFamilyElement2);
        }


    }



}