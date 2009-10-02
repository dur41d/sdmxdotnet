using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public class InternationalString
    {
        private Dictionary<Language, string> localizedStrings = new Dictionary<Language, string>();
        
        public string this[Language language]
        {
            get
            {
                string text = localizedStrings.GetValueOrDefault(language, null);
                return text;
            }
            set
            {
                localizedStrings[language] = value;
            }
        }
    }
}
