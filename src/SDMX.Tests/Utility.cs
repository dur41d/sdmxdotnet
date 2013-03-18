using System;
using System.IO;
using System.Xml.Linq;
using System.Xml.Schema;
using Microsoft.XmlDiffPatch;
using System.Xml;
using System.Xml.Xsl;

namespace SDMX.Tests
{
    internal static class Utility
    {
        //internal static bool IsValidMessage(XDocument doc)
        //{
        //    return IsValidMessage(doc, null);
        //}

        //internal static bool IsValidMessage(XDocument doc, XDocument schema)
        //{
        //    var schemas = new XmlSchemaSet();

        //    schemas.Add("http://www.SDMX.org/resources/SDMXML/schemas/v2_0/message",
        //        GetPath("lib\\SDMXMessage.xsd"));


        //    if (schema != null)
        //    {
        //        using (var reader = schema.CreateReader())
        //        {
        //            schemas.Add(null, reader);
        //        }
        //    }

        //    bool isValid = true;

        //    Validate(doc, schemas, (s, args) =>
        //    {
        //        isValid = false;
        //        if (args.Severity == XmlSeverityType.Warning)

        //            Console.WriteLine("\tWarning: " + args.Message);
        //        else
        //            Console.WriteLine("\tError: " + args.Message);
        //    });

        //    return isValid;
        //}

        //public static void Validate(XDocument doc, XmlSchemaSet schemas, ValidationEventHandler handler)
        //{

        //    // configure the xmlreader validation to use inline schema.
        //    XmlReaderSettings config = new XmlReaderSettings();
        //    config.ValidationType = ValidationType.Schema;
        //    config.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
        //    config.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
        //    config.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;
        //    config.Schemas = schemas;
        //    config.ValidationEventHandler += handler;

        //    using (var reader = doc.CreateReader())
        //    {                
        //        while (reader.Read());
        //    }
        //}

        public static XmlReader GetComapctSchema(string dsdPath, string targetNamespace)
        {
            var dsd = XDocument.Load(dsdPath);
            var transform = new XslCompiledTransform(true);
            var param = new XsltArgumentList();

            param.AddParam("Namespace", "", targetNamespace);

            transform.Load(Utility.GetPath("\\lib\\StructureToCompact.xslt"));
            var schemaDoc = new XDocument();
            using (var writer = schemaDoc.CreateWriter())
            {
                transform.Transform(dsd.CreateReader(), param, writer);
            }
            return schemaDoc.CreateReader();
        }

        // Display any warnings or errors.
        private static void ValidationCallBack(object sender, ValidationEventArgs args)
        {
            if (args.Severity == XmlSeverityType.Warning)
                Console.WriteLine("\tWarning: Matching schema not found.  No validation occurred." + args.Message);
            else
                Console.WriteLine("\tValidation error: " + args.Message);

        }

        internal static string GetPath(string path)
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
