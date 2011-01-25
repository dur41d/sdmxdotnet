using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Common;
using SDMX.Parsers;

namespace SDMX
{
    public class CompactDataReader : DataReader
    {
        public CompactDataReader(XmlReader xmlReader, KeyFamily keyFamily)
        {
            Contract.AssertNotNull(xmlReader, "xmlReader");
            Contract.AssertNotNull(keyFamily, "keyFamily");

            _xmlReader = xmlReader;
            KeyFamily = keyFamily;
        }

        public override bool Read()
        {
            while (_xmlReader.Read())
            {
                if (!_xmlReader.IsStartElement())
                    continue;

                if (_xmlReader.LocalName == "Header")
                {

                    var map = new OXM.FragmentMap<Header>(Namespaces.Message + "Header", new HeaderMap());
                    Value = map.ReadXml(_xmlReader);
                    return true;
                }
                else if (_xmlReader.LocalName == "Series")
                {
                    _record.Clear();
                    while (_xmlReader.MoveToNextAttribute())
                    {
                        var component = KeyFamily.GetComponent(_xmlReader.LocalName);
                        _record[_xmlReader.LocalName] = _converter.Parse(component, _xmlReader.Value, null);
                    }
                }
                else if (_xmlReader.LocalName == "Obs")
                {
                    ClearObsValues();

                    while (_xmlReader.MoveToNextAttribute())
                    {
                        var component = KeyFamily.GetComponent(_xmlReader.LocalName);
                        _record[_xmlReader.LocalName] = _converter.Parse(component, _xmlReader.Value, null);
                    }

                    Value = null;
                    return true;
                }
            }

            return false;
        }
    }
}
