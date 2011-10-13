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
