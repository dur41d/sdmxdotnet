using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public class InternationalText : IEnumerable<InternationalString>
    {
        private Dictionary<Language, string> localizedStrings = new Dictionary<Language, string>();
        
        public void Add(InternationalString iString)
        {
            localizedStrings.Add(iString.Language, iString.Value);
        }

        public void Remove(Language language)
        {
            localizedStrings.Remove(language);   
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

        public override string ToString()
        {
            if (localizedStrings.Where(i => i.Key == Language.English).Any())
            {
                return this[Language.English];
            }
            else if (localizedStrings.Count > 0)
            {
                return localizedStrings[0];
            }
            else
            {
                return "";
            }
        }

        #region IEnumerable<InternationalString> Members

        public IEnumerator<InternationalString> GetEnumerator()
        {
            foreach (var item in localizedStrings)
            {
                yield return new InternationalString(item.Key, item.Value);
            }
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
