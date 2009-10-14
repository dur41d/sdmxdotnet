using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using Common;

namespace SDMX.Parsers
{
    public class StructureMessageMap : RoolElementMap<StructureMessage>
    {
        public override string Name
        {
            get { return "Structure"; }
        }

        public StructureMessageMap()
        {
            DSD dsd = new DSD();

            MapElementContainer("KeyFamilies", false)
                .MapElementCollection<KeyFamily>("KeyFamily", true)
                    .Getter(o => o.KeyFamilies)
                    .Setter(p => Instance.KeyFamilies.Add(p))
                    .Parser(() => new KeyFamilyMap(dsd));
        }

        //protected override StructureMessage CreateObject()
        //{
        //    return new StructureMessage();
        //}
    }
}
