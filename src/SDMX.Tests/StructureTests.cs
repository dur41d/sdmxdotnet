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
        public void StructureSampleTest()
        {   
            string dsdPath = Utility.GetPathFromProjectBase("lib\\StructureSample.xml");

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
            Assert.IsTrue(Utility.ValidateMessage(doc));
        }

        [Test]
        public void WBDSD()
        {
            string dsdPath = Utility.GetPathFromProjectBase("lib\\DSD_WB.xml");

            StructureMessageMap map = new StructureMessageMap();

            StructureMessage message;
            using (var reader = XmlReader.Create(dsdPath))
            {
                message = map.ReadXml(reader);
            }

            var sb = new StringBuilder();
            var settings = new XmlWriterSettings() { Indent = true };
            using (var writer = XmlWriter.Create(sb, settings))
            {
                map.WriteXml(writer, message);
            }

            var doc = XDocument.Parse(sb.ToString());            
            Assert.IsTrue(Utility.ValidateMessage(doc));
        }

        private void Get()
        { 
            
        }

        [Test]
        public void HeirarchicalCodeListSample()
        {
            string dsdPath = Utility.GetPathFromProjectBase("lib\\HeirarchicalCodeListSample.xml");
            var sample = XDocument.Load(dsdPath);
            Utility.ValidateMessage(sample);

            StructureMessageMap map = new StructureMessageMap();

            StructureMessage message;
            using (var reader = XmlReader.Create(dsdPath))
            {
                message = map.ReadXml(reader);
            }

            var sb = new StringBuilder();
            var settings = new XmlWriterSettings() { Indent = true };
            using (var writer = XmlWriter.Create(Utility.GetPathFromProjectBase("lib\\HeirarchicalCodeListSample2.xml"), settings))
            {
                map.WriteXml(writer, message);
            }

            var doc = XDocument.Load(Utility.GetPathFromProjectBase("lib\\HeirarchicalCodeListSample2.xml"));
            Assert.IsTrue(Utility.ValidateMessage(doc));
        }
    
        [Test]
        public void HierarchicalList_InvalidAlias()
        {
            var hList = new HierarchicalCodeList("Europe", "HCL_Europe", "SDMX");
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

            var hList = new HierarchicalCodeList("Europe", "HCL_Europe", "SDMX");
            hList.AddCodeList(countryCL, "countryCLAlias");
            hList.AddCodeList(regionsCL, "RegionsAlias");

            Hierarchy hierarchy = new Hierarchy("id",
               new CodeRef(regionsCL.Get("Europe"),
                   new CodeRef(countryCL.Get("Germany")),
                   new CodeRef(countryCL.Get("UK"))));

            hList.Add(hierarchy);

            Assert.IsNotNull(hList);
            Assert.AreEqual("Europe", hList.Name[Language.English]);
            Assert.AreEqual(new ID("HCL_Europe"), hList.ID);
            Assert.AreEqual(new ID("SDMX"), hList.AgencyID);
            Assert.AreEqual(2, hList.CodeListRefs.Count());
            var codeListRef = hList.CodeListRefs.ElementAt(0);
            Assert.AreEqual(new ID("CL_Country"), codeListRef.ID);
            Assert.AreEqual(new ID("SDMX"), codeListRef.AgencyID);
            Assert.AreEqual(new ID("countryCLAlias"), codeListRef.Alias);

            codeListRef = hList.CodeListRefs.ElementAt(1);
            Assert.AreEqual(new ID("CL_Regions"), codeListRef.ID);
            Assert.AreEqual(new ID("SDMX"), codeListRef.AgencyID);
            Assert.AreEqual(new ID("RegionsAlias"), codeListRef.Alias);

            Assert.AreEqual(1, hList.Count());
            var hirchy = hList.ElementAt(0);
            Assert.AreEqual(new ID("id"), hirchy.ID);            
            Assert.AreEqual(3, hirchy.GetCodeRefs().Count());

            foreach (var codeRef in hirchy.GetCodeRefs())
            {
                Assert.IsNotNull(codeRef.CodeListRef);
                Assert.IsNotNull(codeRef.CodeListRef.Alias);
                Assert.IsTrue(codeRef.CodeListRef.Alias == "countryCLAlias" || codeRef.CodeListRef.Alias == "RegionsAlias");
            }

            var europeCode = hirchy.GetCodeRefs().Where(i => i.CodeID == "Europe").Single();
            Assert.IsNull(europeCode.Parent);
            var germany = hirchy.GetCodeRefs().Where(i => i.CodeID == "Germany").Single();
            var uk = hirchy.GetCodeRefs().Where(i => i.CodeID == "UK").Single();
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
            var codeList = new CodeList("Countries", "CL_Country", "SDMX");
            codeList.Add(new Code("Japan"));
            codeList.Add(new Code("USA"));
            codeList.Add(new Code("UK"));
            codeList.Add(new Code("Germany"));
            codeList.Add(new Code("Brazil"));
            return codeList;
        }

        private static CodeList GetRegionsCodeList()
        {
            var codeList = new CodeList("Regions", "CL_Regions", "SDMX");          
            codeList.Add(new Code("Europe"));
            codeList.Add(new Code("North_America"));
            codeList.Add(new Code("Asia"));
            codeList.Add(new Code("Africa"));
            return codeList;
        }
    }
}