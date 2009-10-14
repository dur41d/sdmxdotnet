using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;

namespace SDMX.Parsers
{
    public class InternationalStringMap : ClassMap<KeyValuePair<Language, string>>
    {
        AttributeMap<KeyValuePair<Language, string>, Language> _langMap;
        ValueMap<KeyValuePair<Language, string>, string> _stringMap;

        public InternationalStringMap()
        {
            _langMap = MapAttribute<Language>("lang", true, Language.English)
                           .Getter(o => o.Key)
                           .ToString(p => LanguageMap.Get(p))
                           .Parser(s => LanguageMap.Get(s));

            _stringMap = MapValue<string>()
                           .Getter(o => o.Value)
                           .Parser(s => s);
        }

        //protected override KeyValuePair<Language, string> CreateObject()
        //{
        //    return new KeyValuePair<Language, string>(_langMap.Value, _stringMap.Value);
        //}
    }
}
