using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX.Parsers
{
    public abstract class VersionableArtefactMap<T> : IdentifiableArtefactMap<T> 
        where T : VersionableArtefact
    {
        public VersionableArtefactMap()
        {
            MapAttribute<string>("version", false)
                .Getter(o => o.Version)
                .Setter((o, p) => o.Version = p)
                .Parser(s => s);

            MapAttribute<TimePeriod>("validFrom", false)
                .Getter(o => o.ValidFrom)
                .Setter((o, p) => o.ValidFrom = p)
                .Parser(s => new TimePeriod(DateTime.Parse(s)));

            MapAttribute<TimePeriod>("validTo", false)
                .Getter(o => o.ValidTo)
                .Setter((o, p) => o.ValidTo = p)
                .Parser(s => new TimePeriod(DateTime.Parse(s)));
        }
    }
}
