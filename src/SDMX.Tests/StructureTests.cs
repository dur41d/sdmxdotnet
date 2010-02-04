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
            message.Header.Name[Language.English] = "New Name";

            var sb = new StringBuilder();
            var settings = new XmlWriterSettings() { Indent = true };
            using (var writer = XmlWriter.Create(sb, settings))
            {
                map.WriteXml(writer, message);
            }

            var doc = XDocument.Parse(sb.ToString());
            doc.Save(@"c:\temp\dsdsample.xml");
            Assert.IsTrue(Utility.ValidateMessage(doc));
        }
     
        [Test]
        public void HeirarchicalCodeListSample()
        {
            string dsdPath = Utility.GetPathFromProjectBase("lib\\HeirarchicalCodeListSample.xml");
            var sample = XDocument.Load(dsdPath);
            Utility.ValidateMessage(sample);

            StructureMessageMap map = new StructureMessageMap();

            StructureMessage message = StructureMessage.Load(dsdPath);

            message.Save(Utility.GetPathFromProjectBase("lib\\HeirarchicalCodeListSample2.xml"));         

            var doc = XDocument.Load(Utility.GetPathFromProjectBase("lib\\HeirarchicalCodeListSample2.xml"));
            Assert.IsTrue(Utility.ValidateMessage(doc));
        }

        [Test]
        public void WBDSD_HierarchicalCodeList()
        {
            // load the DSD
            string dsdPath = Utility.GetPathFromProjectBase("lib\\DSD_WB.xml");

            var dsd = StructureMessage.Load(dsdPath);
            
            // create hierarchical code list and add it to the DSD
            var hlist = new HierarchicalCodeList((ID)"MDG_Regions", (ID)"MDG");
            hlist.Name[Language.English] = "MDG Regions";
            dsd.HierarchicalCodeLists.Add(hlist);

            // get REF_AREA code list from the DSD and add it to the hierarchical code list
            var refAreaCodeList = dsd.CodeLists.Where(codeList => codeList.ID == (ID)"CL_REF_AREA_MDG").Single();
            hlist.AddCodeList(refAreaCodeList, (ID)"REF_AREA_MDG");

            // get parent country code
            var MDG_DEVELOPED = refAreaCodeList.Where(code => code.ID == (ID)"MDG_DEVELOPED").Single();

            // child country codes
            string[] ids = new[] { "ALB", "AND", "AUS", "AUT", "BEL", "BIH", "BMU", "BGR", "HRV", "CAN", "CZE", "DNK", "EST", "FRO", "FIN", "FRA", "DEU", "GRC", "HUN", "ISL", "IRL", "ITA", "JPN", "LVA", "LIE", "LTU", "LUX", "MKD", "MLT", "MCO", "MNE", "NLD", "NZL", "NOR", "POL", "PRT", "ROU", "SVK", "SMR", "SVN", "SRB", "ESP", "SWE", "CHE", "GBR", "USA" };
            var countryCodes = (from c in refAreaCodeList
                                join cid in ids on c.ID.ToString() equals cid
                                select c).ToList();

            // create hirarchy with parent code and child code and add it to
            // the hierarchical code list
            var hierarchy = new Hierarchy((ID)"Developed_Countries", new CodeRef(MDG_DEVELOPED, countryCodes));
            hierarchy.Name[Language.English] = "Developed Countries";
            hlist.Add(hierarchy);

            // create another hierarchy and add it to the hierarchical code list
            var MDG_NAFR = refAreaCodeList.Where(code => code.ID == (ID)"MDG_NAFR").Single();

            ids = new[] { "DZA", "EGY", "LBY", "MAR", "TUN" };

            countryCodes = (from c in refAreaCodeList
                            join cid in ids on c.ID.ToString() equals cid
                            select c).ToList();

            hierarchy = new Hierarchy((ID)"MDG_NAFR", new CodeRef(MDG_NAFR, () => countryCodes));
            hierarchy.Name[Language.English] = "MDG Northern Africa Countries";
            hlist.Add(hierarchy);

            // save the DSD
            dsd.Save(Utility.GetPathFromProjectBase("lib\\DSD_WB2.xml"));

            string messageText = dsd.ToString();

            var doc = XDocument.Parse(messageText);
            Assert.IsTrue(Utility.ValidateMessage(doc));
        }
        
        [Test]
        public void HierarchicalList_InvalidAlias()
        {
            var hList = new HierarchicalCodeList(new InternationalString(Language.English, "Europe"), (ID)"HCL_Europe", (ID)"SDMX");
            var countryCL = GetCountryCodeList();
            var regionsCL = GetRegionsCodeList();

            // don't add the country code list to the hierarchy in order to throw the exception
            //hList.AddCodeList(countryCL, "countryCLAlias");
            hList.AddCodeList(regionsCL, (ID)"RegionsAlias");

            Hierarchy hierarchy = new Hierarchy((ID)"id",
                new CodeRef(regionsCL.Get((ID)"Europe"),
                    new CodeRef(countryCL.Get((ID)"Germany")),
                    new CodeRef(countryCL.Get((ID)"UK"))));

            Assert.Throws<SDMXException>(() => hList.Add(hierarchy));
        }



        [Test]
        public void Create_HierarchicalList()
        {
            var countryCL = GetCountryCodeList();
            var regionsCL = GetRegionsCodeList();

            var hList = new HierarchicalCodeList(new InternationalString(Language.English, "Europe"), (ID)"HCL_Europe", (ID)"SDMX");
            hList.AddCodeList(countryCL, (ID)"countryCLAlias");
            hList.AddCodeList(regionsCL, (ID)"RegionsAlias");

            Hierarchy hierarchy = new Hierarchy((ID)"id",
               new CodeRef(regionsCL.Get((ID)"Europe"),
                   new CodeRef(countryCL.Get((ID)"Germany")),
                   new CodeRef(countryCL.Get((ID)"UK"))));

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
                Assert.IsTrue(codeRef.CodeListRef.Alias == (ID)"countryCLAlias" || codeRef.CodeListRef.Alias == (ID)"RegionsAlias");
            }

            var europeCode = hirchy.GetCodeRefs().Where(i => i.CodeID == (ID)"Europe").Single();
            Assert.IsNull(europeCode.Parent);
            var germany = hirchy.GetCodeRefs().Where(i => i.CodeID == (ID)"Germany").Single();
            var uk = hirchy.GetCodeRefs().Where(i => i.CodeID == (ID)"UK").Single();
            Assert.AreSame(germany.Parent, europeCode);
            Assert.AreSame(uk.Parent, europeCode);
        }

        [Test]
        public void Hierarchy_GetCodeRefs()
        {
            var countryCL = GetCountryCodeList();
            var regionsCL = GetRegionsCodeList();

            Hierarchy hierarchy = new Hierarchy((ID)"id",
                new CodeRef(regionsCL.Get((ID)"Europe"),
                    new CodeRef(countryCL.Get((ID)"Germany")),
                    new CodeRef(countryCL.Get((ID)"UK"))));

            var codeRefs = hierarchy.GetCodeRefs();

            Assert.AreEqual(3, codeRefs.Count());
        }

        private static CodeList GetCountryCodeList()
        {
            var codeList = new CodeList(new InternationalString(Language.English, "Countries"), (ID)"CL_Country", (ID)"SDMX");
            codeList.Add(new Code((ID)"Japan"));
            codeList.Add(new Code((ID)"USA"));
            codeList.Add(new Code((ID)"UK"));
            codeList.Add(new Code((ID)"Germany"));
            codeList.Add(new Code((ID)"Brazil"));
            return codeList;
        }

        private static CodeList GetRegionsCodeList()
        {
            var codeList = new CodeList(new InternationalString(Language.English, "Regions"), (ID)"CL_Regions", (ID)"SDMX");
            codeList.Add(new Code((ID)"Europe"));
            codeList.Add(new Code((ID)"North_America"));
            codeList.Add(new Code((ID)"Asia"));
            codeList.Add(new Code((ID)"Africa"));
            return codeList;
        }
    }
}