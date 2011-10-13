using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using OXM;

namespace SDMX.Parsers
{
    internal abstract class IdentifiableArtefactMap<T> : AnnotableArtefactMap<T> where T : IdentifiableArtefact
    {   
        protected abstract void SetId(Id id);
        protected abstract void SetUri(Uri uri);
        protected abstract void SetName(InternationalString name);
        protected abstract void SetDescription(InternationalString description);

        public IdentifiableArtefactMap()
        {
            Map(o => o.Id).ToAttribute("id", true)
                .Set(v => SetId(v))
                .Converter(new IdConverter());

            Map(o => o.Uri).ToAttribute("uri", false)
                .Set(v => SetUri(v))
                .Converter(new UriConverter());

            int count = 0;
            MapCollection(o => { count = o.Name.Count(); return o.Name; }).ToElement("Name", true)
                .Set(v => SetName(v))
                .ClassMap(() => new InternationalStringMap(count));

            int desCount = 0;
            MapCollection(o => { desCount = o.Description.Count(); return o.Description; }).ToElement("Description", false)
               .Set(v => SetDescription(v))
               .ClassMap(() => new InternationalStringMap(desCount));
        }
    }
}
