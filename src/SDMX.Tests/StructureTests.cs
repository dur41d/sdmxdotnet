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
    public class StructureTests
    {
        [Test]
        [Ignore]
        public void StructureSampleTest()
        {
            string dsdPath = Utility.GetPath("lib\\StructureSample.xml");

            StructureMessageMap map = new StructureMessageMap();

            StructureMessage message;
            using (var reader = XmlReader.Create(dsdPath))
            {
                message = map.ReadXml(reader);
            }

            var output = new StringBuilder();
            var settings = new XmlWriterSettings() { Indent = true };
            using (var writer = XmlWriter.Create(output, settings))
            {
                map.WriteXml(writer, message);
            }

            var doc = XDocument.Parse(output.ToString());
            Assert.IsTrue(Utility.IsValidMessage(doc));
        }

        [Test]
        public void LoadWriteLoad()
        {
            string dsdPath = Utility.GetPath("lib\\StructureSample.xml");
            var message = StructureMessage.Load(dsdPath);

            message.Header.Test = false;

            var stream = new MemoryStream();
            message.Write(stream);            



            stream.Position = 0;

            var doc = new XDocument();
            using (var writer = doc.CreateWriter())
                message.Write(writer);

            // Console.Write(doc);

            Assert.IsTrue(Validator.ValidateMessageXml(stream, w => Console.WriteLine(w), e => Console.WriteLine(e)));

            stream.Position = 0;

            var message2 = StructureMessage.Read(stream);

        }
     
        [Test]
        [Ignore]
        public void HeirarchicalCodeListSample()
        {
            string dsdPath = Utility.GetPath("lib\\HeirarchicalCodeListSample.xml");
            var sample = XDocument.Load(dsdPath);
            Utility.IsValidMessage(sample);

            StructureMessageMap map = new StructureMessageMap();

            StructureMessage message = StructureMessage.Load(dsdPath);

            message.Save(Utility.GetPath("lib\\HeirarchicalCodeListSample2.xml"));         

            var doc = XDocument.Load(Utility.GetPath("lib\\HeirarchicalCodeListSample2.xml"));
            Assert.IsTrue(Utility.IsValidMessage(doc));
        }

        [Test]
        [Ignore]
        public void WBDSD_HierarchicalCodeList()
        {
            // load the DSD
            string dsdPath = Utility.GetPath("lib\\DSD_WB.xml");

            var dsd = StructureMessage.Load(dsdPath);
            
            // create hierarchical code list and add it to the DSD
            var hlist = new HierarchicalCodeList("MDG_Regions", "MDG");
            hlist.Name["en"] = "MDG Regions";
            dsd.HierarchicalCodeLists.Add(hlist);

            // get REF_AREA code list from the DSD and add it to the hierarchical code list
            var refAreaCodeList = dsd.CodeLists.Where(codeList => codeList.Id == "CL_REF_AREA_MDG").Single();
            hlist.AddCodeList(refAreaCodeList, "REF_AREA_MDG");

            // get parent country code
            var MDG_DEVELOPED = refAreaCodeList.Where(code => code.Id == "MDG_DEVELOPED").Single();

            // child country codes
            string[] ids = new[] { "ALB", "AND", "AUS", "AUT", "BEL", "BIH", "BMU", "BGR", "HRV", "CAN", "CZE", "DNK", "EST", "FRO", "FIN", "FRA", "DEU", "GRC", "HUN", "ISL", "IRL", "ITA", "JPN", "LVA", "LIE", "LTU", "LUX", "MKD", "MLT", "MCO", "MNE", "NLD", "NZL", "NOR", "POL", "PRT", "ROU", "SVK", "SMR", "SVN", "SRB", "ESP", "SWE", "CHE", "GBR", "USA" };
            var countryCodes = (from c in refAreaCodeList
                                join cid in ids on c.Id.ToString() equals cid
                                select c).ToList();

            // create hirarchy with parent code and child code and add it to
            // the hierarchical code list
            var hierarchy = new Hierarchy("Developed_Countries", new CodeRef(MDG_DEVELOPED, countryCodes));
            hierarchy.Name["en"] = "Developed Countries";
            hlist.Add(hierarchy);

            // create another hierarchy and add it to the hierarchical code list
            var MDG_NAFR = refAreaCodeList.Where(code => code.Id == "MDG_NAFR").Single();

            ids = new[] { "DZA", "EGY", "LBY", "MAR", "TUN" };

            countryCodes = (from c in refAreaCodeList
                            join cid in ids on c.Id.ToString() equals cid
                            select c).ToList();

            hierarchy = new Hierarchy("MDG_NAFR", new CodeRef(MDG_NAFR, () => countryCodes));
            hierarchy.Name["en"] = "MDG Northern Africa Countries";
            hlist.Add(hierarchy);

            // save the DSD
            dsd.Save(Utility.GetPath("lib\\DSD_WB2.xml"));

            string messageText = dsd.ToString();

            var doc = XDocument.Parse(messageText);
            Assert.IsTrue(Utility.IsValidMessage(doc));
        }
        
        [Test]
        public void HierarchicalList_InvalidAlias()
        {
            var hList = new HierarchicalCodeList(new InternationalString("en", "Europe"), "HCL_Europe", "SDMX");
            var countryCL = GetCountryCodeList();
            var regionsCL = GetRegionsCodeList();

            // don't add the country code list to the hierarchy in order to throw the exception
            //hList.AddCodeList(countryCL, "countryCLAlias");
            hList.AddCodeList(regionsCL, "RegionsAlias");

            Hierarchy hierarchy = new Hierarchy("id",
                new CodeRef(regionsCL.Get("Europe"),
                    new CodeRef(countryCL.Get("Germany")),
                    new CodeRef(countryCL.Get("UK"))));

            Assert.Throws<SDMXException>(() => hList.Add(hierarchy));
        }



        [Test]
        public void Create_HierarchicalList()
        {
            var countryCL = GetCountryCodeList();
            var regionsCL = GetRegionsCodeList();

            var hList = new HierarchicalCodeList(new InternationalString("en", "Europe"), "HCL_Europe", "SDMX");
            hList.AddCodeList(countryCL, "countryCLAlias");
            hList.AddCodeList(regionsCL, "RegionsAlias");

            Hierarchy hierarchy = new Hierarchy("id",
               new CodeRef(regionsCL.Get("Europe"),
                   new CodeRef(countryCL.Get("Germany")),
                   new CodeRef(countryCL.Get("UK"))));

            hList.Add(hierarchy);

            Assert.IsNotNull(hList);
            Assert.AreEqual("Europe", hList.Name["en"]);
            //Assert.AreEqual( Id("HCL_Europe"), hList.Id);
            //Assert.AreEqual(new Id("SDMX"), hList.AgencyId);
            //Assert.AreEqual(2, hList.CodeListRefs.Count());
            //var codeListRef = hList.CodeListRefs.ElementAt(0);
            //Assert.AreEqual(new Id("CL_Country"), codeListRef.Id);
            //Assert.AreEqual(new Id("SDMX"), codeListRef.AgencyId);
            //Assert.AreEqual(new Id("countryCLAlias"), codeListRef.Alias);

            //codeListRef = hList.CodeListRefs.ElementAt(1);
            //Assert.AreEqual(new Id("CL_Regions"), codeListRef.Id);
            //Assert.AreEqual(new Id("SDMX"), codeListRef.AgencyId);
            //Assert.AreEqual(new Id("RegionsAlias"), codeListRef.Alias);

            Assert.AreEqual(1, hList.Count());
            var hirchy = hList.ElementAt(0);
            //Assert.AreEqual(new Id("id"), hirchy.Id);
            Assert.AreEqual(3, hirchy.GetCodeRefs().Count());

            foreach (var codeRef in hirchy.GetCodeRefs())
            {
                Assert.IsNotNull(codeRef.CodeListRef);
                Assert.IsNotNull(codeRef.CodeListRef.Alias);
                Assert.IsTrue(codeRef.CodeListRef.Alias == "countryCLAlias" || codeRef.CodeListRef.Alias == "RegionsAlias");
            }

            var europeCode = hirchy.GetCodeRefs().Where(i => i.CodeId == "Europe").Single();
            Assert.IsNull(europeCode.Parent);
            var germany = hirchy.GetCodeRefs().Where(i => i.CodeId == "Germany").Single();
            var uk = hirchy.GetCodeRefs().Where(i => i.CodeId == "UK").Single();
            Assert.AreSame(germany.Parent, europeCode);
            Assert.AreSame(uk.Parent, europeCode);
        }

        [Test]
        public void Hierarchy_GetCodeRefs()
        {
            var countryCL = GetCountryCodeList();
            var regionsCL = GetRegionsCodeList();

            Hierarchy hierarchy = new Hierarchy("id",
                new CodeRef(regionsCL.Get("Europe"),
                    new CodeRef(countryCL.Get("Germany")),
                    new CodeRef(countryCL.Get("UK"))));

            var codeRefs = hierarchy.GetCodeRefs();

            Assert.AreEqual(3, codeRefs.Count());
        }

        private static CodeList GetCountryCodeList()
        {
            var codeList = new CodeList(new InternationalString("en", "Countries"), "CL_Country", "SDMX");
            codeList.Add(new Code("Japan"));
            codeList.Add(new Code("USA"));
            codeList.Add(new Code("UK"));
            codeList.Add(new Code("Germany"));
            codeList.Add(new Code("Brazil"));
            return codeList;
        }

        private static CodeList GetRegionsCodeList()
        {
            var codeList = new CodeList(new InternationalString("en", "Regions"), "CL_Regions", "SDMX");
            codeList.Add(new Code("Europe"));
            codeList.Add(new Code("North_America"));
            codeList.Add(new Code("Asia"));
            codeList.Add(new Code("Africa"));
            return codeList;
        }
    }
}