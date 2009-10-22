using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using OXM;

namespace SDMX.Parsers
{
    public abstract class IdentifiableArtefactMap<T> : AnnotableArtefactMap<T> where T : IdentifiableArtefact
    {   
        protected abstract void SetID(ID id);
        protected abstract void SetUri(Uri uri);
        protected abstract void SetName(IEnumerable<KeyValuePair<Language,string>> name);
        protected abstract void SetDescription(IEnumerable<KeyValuePair<Language,string>> description);

        public IdentifiableArtefactMap()
        {
            Map(o => o.ID).ToAttribute("id", true)
                .Set(v => SetID(v))
                .Converter(new IDConverter());

            Map(o => o.Uri).ToAttribute("uri", false)
                .Set(v => SetUri(v))
                .Converter(new UriConverter());

            MapCollection(o => GetTextList(o.Description)).ToElement("Name", true)
                .Set(v => SetName(v))
                .ClassMap(new InternationalStringMap());

            MapCollection(o => GetTextList(o.Description)).ToElement("Description", false)
               .Set(v => SetDescription(v))
               .ClassMap(new InternationalStringMap());
        }

        IEnumerable<KeyValuePair<Language, string>> GetTextList(InternationalText text)
        {
            foreach (var lang in text.Languages)
            {
                yield return new KeyValuePair<Language, string>(lang, text[lang]);
            }
        }


    }
}
