using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public class InternationalString
    {
        private Dictionary<Language, string> localizedStrings = new Dictionary<Language, string>();

        public IEnumerable<Language> Languages
        {
            get
            {
                return localizedStrings.Keys.AsEnumerable();
            }
        }

        public IEnumerable<string> LocalizedStrings
        {
            get
            {
                return localizedStrings.Values.AsEnumerable();
            }
        }
        
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
