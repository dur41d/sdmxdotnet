﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Xml.Linq;

namespace SDMX.Tests.NewModel
{
    [TestFixture]
    public class ParserTests
    {
        [Test]
        public void Can_parse_generic_format_dataset()
        {
            string samplePath = Utility.GetPathFromProjectBase("lib\\GenericSample.xml");
            XDocument loadedXml = XDocument.Load(samplePath);

            string dsdPath = Utility.GetPathFromProjectBase("lib\\StructureSample.xml");
            XDocument dsdXml = XDocument.Load(samplePath);

            var datasetElement = (from e in loadedXml.Elements().First().Elements()
                                  where e.Name.LocalName == "DataSet"
                                  select e).Single();

            var dsdParser = new DSDParser();
            KeyFamily keyFamiliy = dsdParser.Parse(dsdXml);

            var parser = new GenericDataSetParser(keyFamiliy);
            var dataSet = parser.Parse(datasetElement);

        }
     
        Dictionary<XElement, int> list = new Dictionary<XElement, int>();
        private void Parse(XElement xElement, int depth)
        {
            list.Add(xElement, depth);
            depth++;
            foreach (var element in xElement.Elements())
            {
                Parse(element, depth);
            }
        }

    }
}
