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
        protected AttributeMap<T, ID> _id;
        
        public IdentifiableArtefactMap()
        {
            _id = MapAttribute<ID>("id", true)
                .Getter(o => o.ID)                
                .Parser(s => new ID(s));

            MapAttribute<Uri>("uri", false)
                .Getter(o => o.Uri)
                .Setter((o, p) => o.Uri = p)
                .Parser(s => new Uri(s));

            MapElementCollection<KeyValuePair<Language, string>>("Name", true)
                .Getter(o =>
                     {
                         var list = new List<KeyValuePair<Language, string>>();
                         foreach (var lang in o.Name.Languages)
                         {
                             var item = new KeyValuePair<Language, string>(lang, o.Name[lang]);
                             list.Add(item);
                         }
                         return list;
                     })
                .Setter((o, list) => list.ForEach(item => o.Name[item.Key] = item.Value))
                .Parser(new InternationalStringMap());

            MapElementCollection<KeyValuePair<Language, string>>("Description", false)
                .Getter(o =>
                    {
                        var list = new List<KeyValuePair<Language, string>>();
                        foreach (var lang in o.Description.Languages)
                        {
                            var item = new KeyValuePair<Language, string>(lang, o.Description[lang]);
                            list.Add(item);
                        }        
                        return list;
                    })
                .Setter((o, list) => list.ForEach(item => o.Description[item.Key] = item.Value))
                .Parser(new InternationalStringMap());


        }

    }
}
