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
        protected abstract void SetIsExternalReference(bool isExternalReference);

        public MaintainableArtefactMap()
        {
            Map(o => o.AgencyID).ToAttribute("agencyID", true)
                .Set(v => SetAgencyID(v))
                .Converter(new IDConverter());

            Map<bool?>(o => o.IsFinal ? o.IsFinal : (bool?)null).ToAttribute("isFinal", false)
                .Set(v => SetIsFinal(v.Value))
                .Converter(new NullableBooleanConverter());

            Map<bool?>(o => o.IsExternalReference ? true : (bool?)null).ToAttribute("isExternalReference", false)
               .Set(v => SetIsExternalReference(v.Value))
               .Converter(new NullableBooleanConverter());
        }
    }
}
