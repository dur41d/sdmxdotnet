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

            var code = new Code("CAN");
            code.Description["en"] = "Canada";
            codelist.Add(code);

            code = new Code("USA");
            code.Description["en"] = "United States of America";
            codelist.Add(code);

            var message = new StructureMessage();
            message.Header = new Header("MSD_HDR", new Party("UIS")) { Prepared = DateTime.Now };            
            message.CodeLists.Add(codelist);
            message.Save("CL_COUNTRY.xml");
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
