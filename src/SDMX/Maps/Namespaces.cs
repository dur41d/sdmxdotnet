using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using Common;
using System.Xml.Linq;

namespace SDMX.Parsers
{
    internal static class Namespaces
    {
        public static readonly XNamespace Message = "http://www.SDMX.org/resources/SDMXML/schemas/v2_0/message";
        public static readonly XNamespace Common = "http://www.SDMX.org/resources/SDMXML/schemas/v2_0/common";
        public static readonly XNamespace Compact = "http://www.SDMX.org/resources/SDMXML/schemas/v2_0/compact";
        public static readonly XNamespace Structure = "http://www.SDMX.org/resources/SDMXML/schemas/v2_0/structure";
        public static readonly XNamespace Cross = "http://www.SDMX.org/resources/SDMXML/schemas/v2_0/cross";
        public static readonly XNamespace Generic = "http://www.SDMX.org/resources/SDMXML/schemas/v2_0/generic";
        public static readonly XNamespace Query = "http://www.SDMX.org/resources/SDMXML/schemas/v2_0/query";
        public static readonly XNamespace Utility = "http://www.SDMX.org/resources/SDMXML/schemas/v2_0/utility";
        public static readonly XNamespace Xsi = "http://www.w3.org/2001/XMLSchema-instance";
    }
}
