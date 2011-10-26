using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using Common;
using System.Xml.Linq;

namespace SDMX.Parsers
{
    internal class StructureMessageMap : RootElementMap<StructureMessage>
    {
        public override XName Name
        {
            get { return Namespaces.Message + "Structure"; }
        }

        StructureMessage _message = new StructureMessage();

        public StructureMessageMap()
        {
            RegisterNamespace("common", Namespaces.Common);
            RegisterNamespace("structure", Namespaces.Structure);

            ElementsOrder("Header", "CodeLists", "HierarchicalCodelists", "Concepts", "KeyFamilies");

            Map(o => o.Header).ToElement("Header", true)
                .Set(v => _message.Header = v)
                .ClassMap(() => new HeaderMap());

            MapContainer("CodeLists", false)
                .MapCollection(o => o.CodeLists).ToElement(Namespaces.Structure + "CodeList", false)
                    .Set(v => _message.CodeLists.Add(v))
                    .ClassMap(() => new CodeListMap());

            MapContainer("HierarchicalCodelists", false)
              .MapCollection(o => o.HierarchicalCodeLists).ToElement(Namespaces.Structure + "HierarchicalCodelist", false)
                  .Set(v => _message.HierarchicalCodeLists.Add(v))
                  .ClassMap(() => new HierarchicalCodeListMap());

            var concepts = MapContainer("Concepts", false);

            concepts.MapCollection(o => o.Concepts).ToElement(Namespaces.Structure + "Concept", false)
                    .Set(v => _message.Concepts.Add(v))
                    .ClassMap(() => new ConceptMap());

            concepts.MapCollection(o => o.ConceptSchemes).ToElement(Namespaces.Structure + "ConceptScheme", false)
                    .Set(v => _message.ConceptSchemes.Add(v))
                    .ClassMap(() => new ConceptSchemeMap());

            MapContainer("KeyFamilies", false)
                .MapCollection(o => o.KeyFamilies).ToElement(Namespaces.Structure + "KeyFamily", false)
                    .Set(v => _message.KeyFamilies.Add(v))
                    .ClassMap(() => new KeyFamilyMap(_message));          
        }

        protected override StructureMessage Return()
        {
            return _message;
        }
    }
}
