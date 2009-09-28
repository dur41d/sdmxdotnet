using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX.Tests
{
    public class Text
    {
        private Dictionary<Language, string> texts = new Dictionary<Language, string>();

        public Text()
        { }

        public Text(string text)
            : this(Language.English, text)
        { }

        public Text(Language language, string text)
        {
            Add(language, text);
        }

        public void Add(Language language, string text)
        {
            texts[language] = text;
        }

        public string this[Language language]
        {
            get
            {
                string text = texts.GetValueOrDefault(language, null);
                return text;
            }
            set
            {
                texts[language] = value;
            }
        }
    }
}
