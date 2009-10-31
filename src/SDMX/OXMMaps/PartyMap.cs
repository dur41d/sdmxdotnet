using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using Common;

namespace SDMX.Parsers
{
    internal class PartyMap : ClassMap<Party>
    {
        Party party;

        public PartyMap()
        {
            Map(o => o.ID).ToAttribute("id", true)
                .Set(v => party = new Party(v))
                .Converter(new IDConverter());

            MapCollection(o => o.Name).ToElement("Name", false)
                .Set(v => party.Name.Add(v))
                .ClassMap(() => new InternationalStringMap());

            MapCollection(o => o.Contacts).ToElement("Contact", false)
                .Set(v => party.Contacts.Add(v))
                .ClassMap(() => new ContactMap());
        }

        protected override Party Return()
        {
            return party;
        }
    }
}
