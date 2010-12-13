using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Xml.Linq;
using SDMX.Parsers;
using System.Xml;
using System.IO;
using OXM;
using System.Xml.Serialization;

namespace SDMX.Tests
{

    [TestFixture]
    public class KeyFamilyTests
    {
        [Test]
        public void ValidateDataQuery_Success()
        {
            var keyFamily = GetKeyFamily();

            var dataQuery = new DataQuery();
            var and = new AndCriterion();
            and.Add(new DimensionCriterion() { Name = "JD_TYPE", Value = "P" });
            and.Add(new AttributeCriterion() { Name = "OBS_CONF", Value = "C" });
            and.Add(new TimePeriodCriterion()
            {
                StartTime = new YearValue(1999),
                EndTime = new YearValue(2002)
            });

            dataQuery.Criterion = and;

            Assert.IsTrue(keyFamily.ValidateDataQuery(dataQuery));
        }

        private KeyFamily GetKeyFamily()
        {
            string dsdPath = Utility.GetPath("lib\\StructureSample.xml");
            var message = StructureMessage.Load(dsdPath);
            return message.KeyFamilies[0];
        }
    }
}
