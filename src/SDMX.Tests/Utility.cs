using System;
using System.IO;
using System.Xml.Linq;
using System.Xml.Schema;
using Microsoft.XmlDiffPatch;
using System.Xml;
using System.Xml.Xsl;
using Common;
using NUnit.Framework;

namespace SDMX.Tests
{
    internal static class Utility
    {
        internal static bool IsValidMessage(XDocument doc)
        {
            return IsValidMessage(doc, null);
        }

        // TODO: use the Message.Validate() method and remove this one
        internal static bool IsValidMessage(XDocument doc, XDocument schema)
        {
            var schemas = new XmlSchemaSet();

            schemas.Add("http://www.SDMX.org/resources/SDMXML/schemas/v2_0/message",
                GetPath("lib\\SDMXMessage.xsd"));

            
            if (schema != null)
            {
                using (var reader = schema.CreateReader())
                {
                    schemas.Add(null, reader);
                }
            }

            bool isValid = true;

            doc.Validate(schemas, (s, args) =>
            {
                isValid = false;
                if (args.Severity == XmlSeverityType.Warning)

                    Console.WriteLine("\tWarning: " + args.Message);
                else
                    Console.WriteLine("\tError: " + args.Message);
            });

            return isValid;
        }        

        public static XDocument GetComapctSchema(string dsdPath, string targetNamespace)
        {
            var dsd = XDocument.Load(GetPath(dsdPath));
            var transform = new XslCompiledTransform(true);
            var param = new XsltArgumentList();

            param.AddParam("Namespace", "", targetNamespace);

            transform.Load(Utility.GetPath("\\lib\\StructureToCompact.xslt"));
            var schemaDoc = new XDocument();
            using (var writer = schemaDoc.CreateWriter())
            {
                transform.Transform(dsd.CreateReader(), param, writer);
            }
            return schemaDoc;
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

        internal static void AssertDataMessageEqual(DataMessage message, DataMessage message2)
        {
            foreach (var series in message.DataSet.Series)
            {
                var series2 = message2.DataSet.Series.Get(series.Key);               
                foreach (var att in series.Attributes)
                {
                    var att2 = series2.Attributes[att.Id];
                    Assert.IsNotNull(att2, "Attribute not found '{0}'.", att.Id);
                    Assert.AreEqual(att.Value, att2);
                }

                foreach (var obs in series)
                {
                    var obs2 = series2.Get(obs.Time);                    
                    Assert.AreEqual(obs.Value, obs2.Value);

                    foreach (var att in obs.Attributes)
                    {
                        var att2 = obs2.Attributes[att.Id];
                        Assert.IsNotNull(att2, "Attribute not found '{0}'.", att.Id);
                        Assert.AreEqual(att.Value, att2);
                    }
                }
            }
        }
    }
}
