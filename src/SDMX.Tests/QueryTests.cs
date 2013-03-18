using NUnit.Framework;
using System;
using SDMX.Parsers;
using System.Xml.Linq;
using System.Text;
using System.Xml;

namespace SDMX.Tests
{
    [TestFixture]
    public class QueryTests
    {
        [Test]
        public void ApiDesign()
        {
            var query = new DataQuery();
            var and = new AndCriterion();
            and.Add(new DimensionCriterion() { Name = "JD_CATEGORY", Value = "A" });
            and.Add(new DimensionCriterion() { Name = "FREQ", Value = "A" });
            and.Add(new DimensionCriterion() { Name = "FREQ", Value = "M" });

            and.Add(new TimePeriodCriterion()
                {
                    StartTime = TimePeriod.FromDate(new DateTime(2000, 1, 1)),
                    EndTime = TimePeriod.FromDate(new DateTime(2000, 12, 31))
                });
            var or = new OrCriterion();
            or.Add(new DataSetCriterion() { Name = "JD014" });
            and.Add(or);

            query.Criterion = and;
        }

        [Test]
        public void ParseQuerySample()
        {
            string samplePath = Utility.GetPath("lib\\QuerySample.xml");

            var message = QueryMessage.Load(samplePath);

            
            var sb = new StringBuilder();
            var settings = new XmlWriterSettings() { Indent = true };                        
            message.Write(sb);
            var doc = XDocument.Parse(sb.ToString());

           

            //Assert.IsTrue(Utility.IsValidMessage(doc));

            Action<string, System.Xml.Schema.XmlSchemaException> action = (m, e) => Console.WriteLine(m + e.LineNumber);

            using (var reader = doc.CreateReader())
                Assert.IsTrue(MessageValidator.ValidateXml(reader, null, action, action));
            //Console.Write(doc.ToString());
            //doc.Save(Utility.GetPath("lib\\QuerySample2.xml"));
        }
    }
}
