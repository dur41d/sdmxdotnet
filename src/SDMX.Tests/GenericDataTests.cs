using System;
using System.Collections.Generic;
using NUnit.Framework;
using Common = SDMX_ML.Framework.Common;
using Message = SDMX_ML.Framework.Message;
using Messages = SDMX_ML.Framework.Messages;
using Query = SDMX_ML.Framework.Query;
using Generic = SDMX_ML.Framework.Generic;
using System.Xml.Linq;
using System.Linq;
using System.Runtime.InteropServices;

namespace SDMX.Tests
{
    [TestFixture]
    public class GenericDataTests
    {
        [Test]
        public void Create_GenericData_from_xml()
        {
            string samplePath = Utility.GetPathFromProjectBase("lib\\GenericSample.xml");
            XDocument loadedXml = XDocument.Load(samplePath);

            var message = new Messages.GenericData(loadedXml.ToString());

            XDocument generatedXml = XDocument.Parse(message.ToXml());

            Assert.IsTrue(Utility.CompareXML(loadedXml, generatedXml));
        }

        [Test]
        public void Create_Structure_from_xml()
        {
            string samplePath = Utility.GetPathFromProjectBase("lib\\StructureSample.xml");
            XDocument loadedXml = XDocument.Load(samplePath);

            var message = new Messages.Structure(loadedXml.ToString());

            XDocument generatedXml = XDocument.Parse(message.ToXml());

            Assert.IsTrue(Utility.CompareXML(loadedXml, generatedXml));
        }

        [Test]
        public void Create_Copmpact_from_xml()
        {
            string samplePath = Utility.GetPathFromProjectBase("lib\\CompactSample.xml");
            XDocument loadedXml = XDocument.Load(samplePath);

            int bytes = Marshal.SizeOf(loadedXml);
            int kbytes = bytes / 1024;
            int Mbytes = kbytes / 1024;

            var message = new Messages.CompactData(loadedXml.ToString());

            XDocument generatedXml = XDocument.Parse(message.ToXml());

            Assert.IsTrue(Utility.CompareXML(loadedXml, generatedXml));
        }

        [Test, Ignore("TODO: To be implemented")]
        public void Create_Utility_from_xml()
        {
            string samplePath = Utility.GetPathFromProjectBase("lib\\GenericSample.xml");
            XDocument loadedXml = XDocument.Load(samplePath);

            var message = new Messages.UtilityData();

            XDocument generatedXml = XDocument.Parse(message.ToXml());

            Assert.IsTrue(Utility.CompareXML(loadedXml, generatedXml));
        }

        [Test]
        public void Create_GenericDataSet()
        {
            var dataset = new Generic.DataSetType();

            var series = new Generic.SeriesType()
            {
                Serieskey = new List<Generic.ValueType>()
                {
                    new Generic.ValueType() { Concept = new Common.IDType("FREQ"), Value = "A" },
                    new Generic.ValueType() { Concept = new Common.IDType("SEX"), Value = "M" },
                    new Generic.ValueType() { Concept = new Common.IDType("AGE"), Value = "15" }
                },
                Obs = new List<Generic.ObsType>()
                {
                    new Generic.ObsType() 
                    { 
                        Time = new Common.TimePeriodType() { TimePeriod = "2007" },
                        ObsValue = new Generic.ObsValueType() { Value = 1 }
                    },
                    new Generic.ObsType() 
                    { 
                        Time = new Common.TimePeriodType() { TimePeriod = "2008" },
                        ObsValue = new Generic.ObsValueType() { Value = 2 }
                    },
                    new Generic.ObsType() 
                    { 
                        Time = new Common.TimePeriodType() { TimePeriod = "2009" },
                        ObsValue = new Generic.ObsValueType() { Value = 3 }
                    }
                }
            };

            dataset.Series.Add(series);
        }

        [Test]
        public void Create_CodeList()
        {
            // create code list 
            var frequencyList = new CodeList("FREQ");
            frequencyList.AddCode(new Code("A"));
            frequencyList.AddCode(new Code("B"));
            frequencyList.AddCode(new Code("C"));
            frequencyList.AddCode(new Code("D"));

            var code = frequencyList["A"];

            
        }

        [Test]
        public void Create_series_key()
        {
            var values = new List<KeyValuePair<string, string>>();
            values.Add(new KeyValuePair<string, string>("FREQ", "A"));

            var keyFamily = CreateKeyFamily();
            var dataSet = new DataSet(keyFamily);
            var series = dataSet.CreateEmptySeries();
            foreach (var value in values)
            {
                series.AddKeyValue(value.Key, value.Value);
            }            

            object keyValue = series.GetKeyValue("FREQ");

            Assert.IsTrue(keyValue is Code);
            Assert.IsTrue(((Code)keyValue).Value == "A");

        }

        [Test]
        public void Add_observateion()
        {
            var seriesValues = new List<KeyValuePair<string, string>>();
            seriesValues.Add(new KeyValuePair<string, string>("FREQ", "A"));
            string timePeriod = "1998";
            string obsValue = "9.993489";

            var keyFamily = CreateKeyFamily();
            var dataSet = new DataSet(keyFamily);
            var series = dataSet.CreateEmptySeries();
            foreach (var value in seriesValues)
            {
                series.AddKeyValue(value.Key, value.Value);
            }

            Observation obs = Observation.Create(timePeriod, obsValue);

            series.AddObservation(obs);

        }

        private static KeyFamily CreateKeyFamily()
        {
            var frequencyList = new CodeList("FREQ");
            frequencyList.AddCode(new Code("A"));
            frequencyList.AddCode(new Code("B"));
            frequencyList.AddCode(new Code("C"));
            frequencyList.AddCode(new Code("D"));

            var keyFamily = new KeyFamily();

            Concept freqConcept = new Concept("FREQ");

            keyFamily.AddDimension(new Dimension(freqConcept, frequencyList));
            return keyFamily;
        }

    
    }
}
