using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;

namespace SDMX.Parsers
{
    public static class LanguageMap
    {
        static ObjectMap<Language, string> map = new ObjectMap<Language, string>();

        static LanguageMap()
        {
            map.Map(Language.English, "en");
        }

        public static Language Get(string lang)
        {
            return map[lang];
        }

        public static string Get(Language lang)
        {
            return map[lang];
        }
    }
}
