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
    
    public class StructureMessageMap : RoolElementMap<StructureMessage>
    {
        public override XName Name
        {
            get { return Namespaces.Message + "Structure"; }
        }

        StructureMessage _message = new StructureMessage();

        public StructureMessageMap()
        {
            DSD dsd = new DSD();

            RegisterNamespace("common", Namespaces.Common);
            RegisterNamespace("structure", Namespaces.Structure);

            Map(o => o.Header).ToElement("Header", true)
                .Set(v => _message.Header = v)
                .ClassMap(new HeaderMap());

            MapContainer("CodeLists", false)
                .MapCollection(o => o.CodeLists).ToElement(Namespaces.Structure + "CodeList", true)
                    .Set(v => _message.CodeLists.Add(v))
                    .ClassMap(() => new CodeListMap());

            MapContainer("Concepts", false)
                .MapCollection(o => o.Concepts).ToElement(Namespaces.Structure + "Concept", true)
                    .Set(v => _message.Concepts.Add(v))
                    .ClassMap(() => new ConceptMap());

            MapContainer("KeyFamilies", false)
                .MapCollection(o => o.KeyFamilies).ToElement(Namespaces.Structure + "KeyFamily", true)
                    .Set(v => _message.KeyFamilies.Add(v))
                    .ClassMap(() => new KeyFamilyMap(_message));
        }

        protected override StructureMessage Return()
        {
            return _message;
        }
    }
}
