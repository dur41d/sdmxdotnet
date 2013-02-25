using System;
using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;

namespace SDMX.Tests
{
    [TestFixture]   
    [Ignore]
    public class BIAccelerator
    {
        /// <summary>
        /// Issue# 1&2
        /// Read the dsd and fix in memory and read the file successfully
        /// </summary>
        [Test]        
        public void Fix_dsd_add_time_format()
        {
            string dsdPath = @"C:\Temp\ei_bsin_m.sdmx\ei_bsin_m.dsd.xml";
            string dataPath = @"C:\Temp\ei_bsin_m.sdmx\ei_bsin_m.sdmx.xml";

            // load the DSD in XDocument to fix programatically
            var dsdDoc = XDocument.Load(dsdPath);

            // add TIME_FORMAT concept
            // find TIME_PERIOD node and add TIME_FORMAT
            var concept = dsdDoc.Descendants().Where(i => i.Name.LocalName == "Concept" && i.Attribute("id").Value == "TIME_PERIOD").First();
            var copy = new XElement(concept);
            copy.SetAttributeValue("id", "TIME_FORMAT");
            concept.AddAfterSelf(copy);

            
            StructureMessage dsd = null;
            using (var reader = dsdDoc.CreateReader())
            {
                // add Console.WriteLine action to avoid throwing excpetions
                // Note: no need to remove the TextFormat="String" because the library ignores it
                dsd = StructureMessage.Read(reader, v => Console.WriteLine(v.Message));
            }

            var keyFamily = dsd.KeyFamilies[0];

            using (var reader = DataReader.Create(dataPath, keyFamily))
            {
                var header = reader.ReadHeader();

                reader.ThrowExceptionIfNotValid = false;

                while (reader.Read())
                {
                    if (reader.IsValid)
                    {
                        //Console.WriteLine("FREQ={0},indic={1},s_adj={2},unit={3},geo={4},TIME_PERIOD={5},OBS_VALUE={6},TIME_FORMAT={7},OBS_STATUS={8}",
                        //                reader["FREQ"],
                        //                reader["indic"],
                        //                reader["s_adj"],
                        //                reader["unit"],
                        //                reader["geo"],
                        //                reader["TIME_PERIOD"],
                        //                reader["OBS_VALUE"],
                        //                reader["TIME_FORMAT"],
                        //                reader["OBS_STATUS"]);
                    }
                    else
                    {
                        WriteErrors(reader);

                        Assert.Fail();
                    }
                }
            }
        }
     
        /// <summary>
        /// Issue# 3,4,5
        /// Read the data file and recover from errors
        /// </summary>
        [Test]
        public void Invalid_data_file()
        {
            string dsdPath = @"C:\Temp\ert_bil_eur_d.sdmx\ert_bil_eur_d.dsd.xml";
            string dataPath = @"C:\Temp\ert_bil_eur_d.sdmx\ert_bil_eur_d.sdmx.xml";

            var dsdDoc = XDocument.Load(dsdPath);
            var concept = dsdDoc.Descendants().Where(i => i.Name.LocalName == "Concept" && i.Attribute("id").Value == "TIME_PERIOD").First();
            var copy = new XElement(concept);
            copy.SetAttributeValue("id", "TIME_FORMAT");
            concept.AddAfterSelf(copy);

            StructureMessage dsd = null;
            using (var reader = dsdDoc.CreateReader())
            {
                dsd = StructureMessage.Read(reader, v => Console.WriteLine(v.Message));
            }

            var keyFamily = dsd.KeyFamilies[0];

            //using (var customReader = new CustomXmlReader(dataPath))
            using (var reader = DataReader.Create(dataPath, keyFamily))
            {
                var header = reader.ReadHeader();

                reader.ThrowExceptionIfNotValid = false;

                while (reader.Read())
                {
                    if (!reader.IsValid)
                    {
                        foreach (var error in reader.Errors)
                        {
                            bool errorCorrected = false;
                            if (error is ParseError)
                            {
                                var parseError = (ParseError)error;
                                if (parseError.Name == "TIME_PERIOD")
                                {
                                    string year = parseError.Value.Substring(0, 4);
                                    string month = parseError.Value.Substring(4, 2);
                                    string day = parseError.Value.Substring(6, 2);

                                    // recover time value
                                    var timePeriodValue = new DateTime(int.Parse(year), int.Parse(month), int.Parse(day));
                                    errorCorrected = true;
                                    //Console.WriteLine("FREQ={0},otp={1},unit={2},currency={3},TIME_PERIOD={4},OBS_VALUE={5},TIME_FORMAT={6},OBS_STATUS={7}",
                                    //   reader["FREQ"],
                                    //   reader["otp"],
                                    //   reader["unit"],
                                    //   reader["currency"],
                                    //   timePeriodValue,
                                    //   reader["OBS_VALUE"],
                                    //   reader["TIME_FORMAT"],
                                    //   reader["OBS_STATUS"]);
                                }
                                else if (parseError.Name == "OBS_STATUS")
                                {
                                    if (parseError.Value == "iz")
                                    {
                                        // do something to correct the error
                                        errorCorrected = true;
                                    }
                                }
                            }
                            else if (error is MandatoryComponentMissing)
                            {
                                var missingError = (MandatoryComponentMissing)error;

                                if (missingError.Name == "OBS_VALUE")
                                {
                                    // do something to correct the error
                                    errorCorrected = true;
                                }
                            }

                            if (!errorCorrected)
                            {
                                Console.WriteLine(error.Message);
                                Assert.Fail();
                            }
                        }
                    }
                    else
                    {
                        // WriteRecord(reader);
                    }
                }
            }
        }

        /// <summary>
        /// BIS file
        /// </summary>
        [Test]
        public void BIS()
        {
            string dsdPath = @"C:\Temp\WEBSTATS_IBLR_DATAFLOW-1361479813975\WEBSTATS_IBLR_DATAFLOW-1361479877306.xml";
            string dataPath = @"C:\Temp\WEBSTATS_IBLR_DATAFLOW-1361479813975\BISWEB-WEBSTATS_IBLR_DATAFLOW_formatted.xml";
            
            var dsd = StructureMessage.Load(dsdPath);

            var keyFamily = dsd.KeyFamilies[0];
            
            using (var reader = DataReader.Create(dataPath, keyFamily))
            {
                var header = reader.ReadHeader();

                reader.ThrowExceptionIfNotValid = false;

                while (reader.Read())
                {
                    if (!reader.IsValid)
                    {
                        WriteErrors(reader);
                        Assert.Fail();                     
                    }
                    else
                    {
                        // WriteRecord(reader);
                    }
                }
            }
        }

        [Test]
        public void WebService()
        {
            string dataPath = Utility.GetPath("lib\\MessageGroupSample3.xml");

            string dsdPath = Utility.GetPath("lib\\StructureSample.xml");
            var dsd = StructureMessage.Load(dsdPath);
            var keyFamily = dsd.KeyFamilies[0];

            int counter = 0;
            using (var reader = new MessageGroupReader(dataPath, keyFamily))
            {
                var header = reader.ReadHeader();

                Assert.IsNotNull(header);

                while (reader.Read())
                {
                    Assert.AreEqual(17, reader.Count());
                    WriteRecord(reader);
                    counter++;
                }
            }

            Assert.AreEqual(13, counter);
        }

        void WriteErrors(ISDMXDataReader reader)
        {
            foreach (var error in reader.Errors)
            {
                Console.WriteLine(error.Message);
            }
        }

        void WriteRecord(ISDMXDataReader reader)
        {
            foreach (var item in reader)
            {
                Console.Write("{0}={1},", item.Key, item.Value);
            }
            Console.WriteLine();
        }
    }
}
