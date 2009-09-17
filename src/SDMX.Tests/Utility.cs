using System;
using System.IO;
using System.Xml.Linq;
using System.Xml.Schema;
using Microsoft.XmlDiffPatch;
using System.Xml;

namespace SDMX.Tests
{
    internal static class Utility
    {
        internal static bool ValidateMessage(string xml)
        {
            var doc = XDocument.Parse(xml);
            var schemas = new XmlSchemaSet();
            //schemas.Add("http://www.SDMX.org/resources/SDMXML/schemas/v2_0/message",
            //    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\..\\lib\\SDMXMessage.xsd"));

            bool isValid = true;

            doc.Validate(schemas, (s, args) =>
            {
                isValid = false;
                if (args.Severity == XmlSeverityType.Warning)

                    Console.WriteLine("\tWarning: Matching schema not found.  No validation occurred." + args.Message);
                else
                    Console.WriteLine("\tValidation error: " + args.Message);
            });

            return isValid;
        }

        // Display any warnings or errors.
        private static void ValidationCallBack(object sender, ValidationEventArgs args)
        {
            if (args.Severity == XmlSeverityType.Warning)
                Console.WriteLine("\tWarning: Matching schema not found.  No validation occurred." + args.Message);
            else
                Console.WriteLine("\tValidation error: " + args.Message);

        }

        internal static string GetPathFromProjectBase(string path)
        {
            return "..\\..\\..\\..\\" + path;
        }

        internal static bool CompareXML(XDocument xmlDocument1, XDocument xmlDocument2)
        {
            XmlDiff diff = new XmlDiff();
            diff.IgnoreChildOrder = true;
            diff.IgnoreXmlDecl = true;
            diff.IgnoreWhitespace = true;
            diff.IgnoreComments = true;
            diff.IgnorePI = true;
            diff.IgnoreDtd = true;
            var doc1 = new XmlDocument();
            doc1.LoadXml(xmlDocument1.ToString());
            var doc2 = new XmlDocument();
            doc2.LoadXml(xmlDocument2.ToString());

            bool result = diff.Compare(doc1, doc2);

            return result;
        }
    }
}
