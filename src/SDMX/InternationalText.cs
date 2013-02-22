using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace SDMX
{
    public class InternationalText : IEnumerable<InternationalString>
    {
        private Dictionary<string, string> _localizedStrings = new Dictionary<string, string>();
        
        public void Add(InternationalString iString)
        {
            _localizedStrings.Add(iString.Language, iString.Value);
        }

        public void Remove(string language)
        {
            _localizedStrings.Remove(language);
        }       
        
        public string this[string language]
        {
            get
            {
                string text = _localizedStrings.GetValueOrDefault(language, null);
                return text;
            }
            set
            {
                _localizedStrings[language] = value;
            }
        }

        public override string ToString()
        {
            if (_localizedStrings.ContainsKey("en"))
            {
                return this["en"];
            }
            else if (_localizedStrings.Count > 0)
            {
                return _localizedStrings.First().Value;
            }
            else
            {
                return "";
            }
        }

        public IEnumerator<InternationalString> GetEnumerator()
        {
            foreach (var item in _localizedStrings)
            {
                yield return new InternationalString(item.Key, item.Value);
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
