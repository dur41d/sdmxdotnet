using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;

namespace SDMX.Parsers
{
    public abstract class MaintainableArtefactMap<T> : VersionableArtefactMap<T>
        where T : MaintainableArtefact
    {
        protected abstract void SetAgencyID(ID  agencyId);
        protected abstract void SetIsFinal(bool isFinal);

        public MaintainableArtefactMap()
        {
            Map(o => o.AgencyID).ToAttribute("agencyID", true)
                .Set(v => SetAgencyID(v))
                .Converter(new IDConverter());

            Map(o => o.IsFinal).ToAttribute("isFinal", false)
                .Set(v => SetIsFinal(v))
                .Converter(new BooleanConverter());  
        }
    }
}
