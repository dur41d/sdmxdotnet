using NUnit.Framework;
using System;
using SDMX.Parsers;
using System.Xml.Linq;

namespace SDMX.Tests
{
    [TestFixture]
    public class QueryTests
    {
        //[Test]
        //public void ApiDesign()
        //{
        //    var query = new DataQuery();
        //    var and = new AndCriterion();
        //    and.Add(new DimensionCriterion() { Name = "JD_CATEGORY", Value = "A" });
        //    and.Add(new DimensionCriterion() { Name = "FREQ", Value = "A" });
        //    and.Add(new DimensionCriterion() { Name = "FREQ", Value = "M" });

        //    and.Add(new TimePeriodCriterion() 
        //        { 
        //            StartTime = new DateValue(new DateTime(2000, 1, 1)), 
        //            EndTime = new DateValue(new DateTime(2000, 12, 31)) 
        //        });
        //    var or = new OrCriterion();
        //    or.Add(new DataSetCriterion() { Name = "JD014" });
        //    and.Add(or);

        //    query.Criterion = and;
        //}

        [Test]
        public void ParseQuerySample()
        {
            string samplePath = Utility.GetPath("lib\\QuerySample.xml");

            var message = QueryMessage.Load(samplePath);

            var doc = new XDocument();
            using (var writer = doc.CreateWriter())
            {
                message.Write(writer);
            }

            Assert.IsTrue(Utility.IsValidMessage(doc));
            //Console.Write(doc.ToString());
            doc.Save(Utility.GetPath("lib\\QuerySample2.xml"));
        }
    }
}
