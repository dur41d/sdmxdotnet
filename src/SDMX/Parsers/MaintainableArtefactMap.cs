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
        protected AttributeMap<T, ID> _agencyIDMap;

        public MaintainableArtefactMap()
        {
            _agencyIDMap = MapAttribute<ID>("agencyID", true)
                .Getter(o => o.AgencyID)
                .Parser(s => new ID(s));
            
            MapAttribute<bool>("isFinal", false)
                .Getter(o => o.IsFinal)
                .Setter(p => Instance.IsFinal = p)
                .Parser(s => bool.Parse(s));        
        }
    }
}
