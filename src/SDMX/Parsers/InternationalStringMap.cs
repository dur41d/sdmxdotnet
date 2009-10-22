using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using System.Xml.Linq;

namespace SDMX.Parsers
{
    public class InternationalStringMap : ClassMap<KeyValuePair<Language, string>>
    {
        KeyValuePair<Language, string> item;
        Language lang;
        string value;

        public InternationalStringMap()
        {
            Map(o => o.Key).ToAttribute(XNamespace.Xml + "lang", true, "en")
                .Set(v => lang = v)
                .Converter(new LanguageConverter());

            Map(o => o.Value).ToContent()
                .Set(v => value = v)
                .Converter(new StringConverter());
        }


        protected override KeyValuePair<Language, string> Return()
        {
            return new KeyValuePair<Language, string>(lang, value);
        }
    }
}
