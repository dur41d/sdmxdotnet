using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;

namespace SDMX.Parsers
{
    internal abstract class VersionableArtefactMap<T> : IdentifiableArtefactMap<T> 
        where T : VersionableArtefact
    {
        protected abstract void SetVersion(string version);
        protected abstract void SetValidTo(TimePeriod validTo);
        protected abstract void SetValidFrom(TimePeriod validFrom);

        
        public VersionableArtefactMap()
        {
            Map(o => o.Version).ToAttribute("version", false)
                .Set(v => SetVersion(v))
                .Converter(new StringConverter());

            Map(o => o.ValidFrom).ToAttribute("validFrom", false)
                .Set(v => SetValidFrom(v))
                .Converter(new TimePeriodConverter());

            Map(o => o.ValidTo).ToAttribute("validTo", false)
                .Set(v => SetValidTo(v))
                .Converter(new TimePeriodConverter());
        }
    }
}
