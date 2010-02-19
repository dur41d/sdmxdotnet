using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;

namespace SDMX.Parsers
{
    public class LanguageConverter : SimpleTypeConverterBase<Language>
    {
        public LanguageConverter()
        {
            Map(Language.English, "en");
        }
    }
}
