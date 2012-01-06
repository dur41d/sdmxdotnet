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

            while (XmlReader.Read())
            {
                if (!XmlReader.IsStartElement())
                    continue;

                var group = KeyFamily.Groups.Find(XmlReader.LocalName);

                if (group != null)
                {   
                    var dict = new Dictionary<string, object>();
                    while (XmlReader.MoveToNextAttribute())
                    {
                        var component = KeyFamily.GetComponent(XmlReader.LocalName);
                        dict[XmlReader.LocalName] = component.Parse(XmlReader.Value, null);
                    }

                    SetGroup(group, dict);
                }
                else if (XmlReader.LocalName == "Series")
                {
                    ClearSeries();
                    while (XmlReader.MoveToNextAttribute())
                    {
                        var component = KeyFamily.GetComponent(XmlReader.LocalName);
                        SetSeries(XmlReader.LocalName, component.Parse(XmlReader.Value, null));
                    }
                }
                else if (XmlReader.LocalName == "Obs")
                {
                    ClearObs();

                    while (XmlReader.MoveToNextAttribute())
                    {
                        var component = KeyFamily.GetComponent(XmlReader.LocalName);
                        SetObs(XmlReader.LocalName, component.Parse(XmlReader.Value, null));
                    }

                    SetRecord();

                    return true;
                }
            }

            return false;
        }
    }
}
