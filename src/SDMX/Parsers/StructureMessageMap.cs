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
        public static readonly XNamespace Generic = "";
        public static readonly XNamespace Structure = "http://www.SDMX.org/resources/SDMXML/schemas/v2_0/structure";
         
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

            MapContainer("KeyFamilies", false)
                .MapCollection(o => o.KeyFamilies).ToElement(Namespaces.Structure + "KeyFamily", true)
                    .Set(v => v.ForEach(i => _message.KeyFamilies.Add(i)))
                    .ClassMap(new KeyFamilyMap(dsd));
        }

        protected override StructureMessage Return()
        {
            return _message;
        }
    }
}
