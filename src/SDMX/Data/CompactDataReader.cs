using System.Collections.Generic;
using System.Xml;

namespace SDMX
{
    public class CompactDataReader : DataReader
    {
        public CompactDataReader(XmlReader xmlReader, KeyFamily keyFamily)
            : base(xmlReader, keyFamily)
        { }

        public override bool Read()
        {
            CheckDisposed();

            while (_xmlReader.Read())
            {
                if (!_xmlReader.IsStartElement())
                    continue;

                var group = KeyFamily.Groups.Find(_xmlReader.LocalName);

                if (group != null)
                {   
                    var dict = new Dictionary<string, object>();
                    while (_xmlReader.MoveToNextAttribute())
                    {
                        var component = KeyFamily.GetComponent(_xmlReader.LocalName);
                        dict[_xmlReader.LocalName] = component.Parse(_xmlReader.Value, null);
                    }

                    SetGroup(group, dict);
                }
                else if (_xmlReader.LocalName == "Series")
                {
                    ClearSeries();
                    while (_xmlReader.MoveToNextAttribute())
                    {
                        var component = KeyFamily.GetComponent(_xmlReader.LocalName);
                        SetSeries(_xmlReader.LocalName, component.Parse(_xmlReader.Value, null));
                    }
                }
                else if (_xmlReader.LocalName == "Obs")
                {
                    ClearObs();

                    while (_xmlReader.MoveToNextAttribute())
                    {
                        var component = KeyFamily.GetComponent(_xmlReader.LocalName);
                        SetObs(_xmlReader.LocalName, component.Parse(_xmlReader.Value, null));
                    }

                    SetRecord();

                    return true;
                }
            }

            return false;
        }
    }
}
