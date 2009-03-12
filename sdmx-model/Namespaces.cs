using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

    public enum  AttachmentLevel
    {
        DataSet,
        Group,
        Series,
        Observation,
        Any 
    }

    public enum AssignmentStatus
    {
        Mandatory,
        Conditional
    }

namespace SDMX_ML.Framework
{
    internal class Namespaces
    {
        private static string nsurl = "http://www.SDMX.org/resources/SDMXML/schemas/v2_0/";
        private static string nsxsi = "http://www.w3.org/2001/XMLSchema-instance";

 
        internal static XNamespace GetNS(string name)
        {
            return nsurl + name;
        }
        
        internal static XNamespace GetLocation()
        {
            return nsurl + "message SDMXMessage.xsd";
        }

        internal static XNamespace GetXSI()
        {
            return nsxsi;
        }

        internal static XElement GetAllNamespaces(string name)
        {
            return new XElement(Namespaces.GetNS("message") + name,
                new XAttribute(XNamespace.Xmlns + "generic", GetNS("generic")),
                new XAttribute(XNamespace.Xmlns + "common", GetNS("common")),
                new XAttribute(XNamespace.Xmlns + "compact", GetNS("compact")),
                new XAttribute(XNamespace.Xmlns + "cross", GetNS("cross")),
                new XAttribute(XNamespace.Xmlns + "query", GetNS("query")),
                new XAttribute(XNamespace.Xmlns + "structure", GetNS("structure")),
                new XAttribute(XNamespace.Xmlns + "utility", GetNS("utility")),
                new XAttribute(XNamespace.Xmlns + "xsi", GetXSI()),
                new XAttribute(Namespaces.GetXSI() + "schemaLocation", GetLocation()));
        }

    }
}
