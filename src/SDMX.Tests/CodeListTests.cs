using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Xml.Linq;

namespace SDMX.Tests
{
    [TestFixture]
    public class CodeListTests
    {
        [Test]
        public void CreateCodeList()
        {
            var codelist = new CodeList(new InternationalString("en", "Countries"), "CL_COUNTRY", "UIS");            

            codelist.Add(new Code("CAN"));
            codelist.Add(new Code("USA"));

            var message = new StructureMessage();
            message.Header = BuildHeader();
            message.CodeLists.Add(codelist);

            var doc = new XDocument();
            using (var writer = doc.CreateWriter())
            {
                message.Write(writer);
            }
        }


        private Header BuildHeader()
        {
            return new Header("MSD_HDR", new Party("UIS")) 
                { 
                    Prepared = DateTime.Now
                };
        }
    }
}
