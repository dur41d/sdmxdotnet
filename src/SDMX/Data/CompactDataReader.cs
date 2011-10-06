using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Common;
using SDMX.Parsers;
using System.Text;

namespace SDMX
{
    public class CompactDataReader : DataReader
    {
        public CompactDataReader(XmlReader xmlReader, KeyFamily keyFamily)
            : base(xmlReader, keyFamily)
        { }

        public override bool Read()
        {
            while (_xmlReader.Read())
            {
                if (!_xmlReader.IsStartElement())
                    continue;

                var group = KeyFamily.Groups.TryGet(_xmlReader.LocalName);

                if (group != null)
                {   
                    var dict = new Dictionary<string, object>();
                    while (_xmlReader.MoveToNextAttribute())
                    {
                        var component = KeyFamily.GetComponent(_xmlReader.LocalName);
                        dict[_xmlReader.LocalName] = _converter.Parse(component, _xmlReader.Value, null);
                    }

                    ReadGroupValues(group, dict);
                }
                else if (_xmlReader.LocalName == "Series")
                {
                    _record.Clear();
                    while (_xmlReader.MoveToNextAttribute())
                    {
                        var component = KeyFamily.GetComponent(_xmlReader.LocalName);
                        _record[_xmlReader.LocalName] = _converter.Parse(component, _xmlReader.Value, null);
                    }

                    SetGroupValues();
                }
                else if (_xmlReader.LocalName == "Obs")
                {
                    ClearObsAttributes();

                    while (_xmlReader.MoveToNextAttribute())
                    {
                        var component = KeyFamily.GetComponent(_xmlReader.LocalName);
                        _record[_xmlReader.LocalName] = _converter.Parse(component, _xmlReader.Value, null);
                    }

                    return true;
                }
            }

            return false;
        }
    }
}
