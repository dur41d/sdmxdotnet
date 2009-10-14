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
                .Setter(p => Instance.Version = p)
                .Parser(s => s);

            MapAttribute<TimePeriod>("validFrom", false)
                .Getter(o => o.ValidFrom)
                .Setter(p => Instance.ValidFrom = p)
                .Parser(s => new TimePeriod(DateTime.Parse(s)));

            MapAttribute<TimePeriod>("validTo", false)
                .Getter(o => o.ValidTo)
                .Setter(p => Instance.ValidTo = p)
                .Parser(s => new TimePeriod(DateTime.Parse(s)));
        }
    }
}
