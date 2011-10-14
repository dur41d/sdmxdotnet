using System.Collections.Generic;
using System.Xml;

namespace SDMX
{
    public class GenericDataReader : DataReader
    {
        public GenericDataReader(XmlReader xmlReader, KeyFamily keyFamily)
            : base(xmlReader, keyFamily)
        { }

        public override bool Read()
        {
            CheckDisposed();

            while (_xmlReader.Read())
            {
                if (_xmlReader.LocalName == "Group" && _xmlReader.IsStartElement())
                {
                    string groupID = _xmlReader.GetAttribute("type");

                    if (groupID == null)
                    {
                        var xml = _xmlReader as IXmlLineInfo;
                        throw new SDMXException("Parse error at ({0},{1}): Group is missing type attribute.", xml.LineNumber, xml.LinePosition);
                    }

                    var group = KeyFamily.Groups.Get(groupID);
                    var dict = new Dictionary<string, object>();
                    while (_xmlReader.Read() && _xmlReader.LocalName != "Series")
                    {
                        if (_xmlReader.LocalName == "Value" && _xmlReader.IsStartElement())
                        {
                            string concept = _xmlReader.GetAttribute("concept");
                            string value = _xmlReader.GetAttribute("value");
                            string startTime = _xmlReader.GetAttribute("startTime");
                            var component = KeyFamily.GetComponent(concept);
                            dict[concept] = component.Parse(value, startTime);
                        }
                    }

                    ReadGroupValues(group, dict);
                } 


                if (_xmlReader.LocalName == "Series" && _xmlReader.IsStartElement())
                {
                    ClearRecord();
                }               
                else if (_xmlReader.LocalName == "Obs" && _xmlReader.IsStartElement())
                {                    
                    ClearObsAttributes();
                }
                else if (_xmlReader.LocalName == "Time" && _xmlReader.IsStartElement())
                {
                    string value = _xmlReader.ReadString();
                    SetRecord(KeyFamily.TimeDimension.Concept.Id.ToString(), KeyFamily.TimeDimension.Parse(value, null));
                }
                else if (_xmlReader.LocalName == "ObsValue" && _xmlReader.IsStartElement())
                {
                    string value = _xmlReader.GetAttribute("value");
                    SetRecord(KeyFamily.PrimaryMeasure.Concept.Id.ToString(), KeyFamily.PrimaryMeasure.Parse(value, null));
                }
                else if (_xmlReader.LocalName == "Value" && _xmlReader.IsStartElement())
                {
                    string concept = _xmlReader.GetAttribute("concept");
                    string value = _xmlReader.GetAttribute("value");
                    string startTime = _xmlReader.GetAttribute("startTime");
                    var component = KeyFamily.GetComponent(concept);
                    SetRecord(concept, component.Parse(value, startTime));
                }
                else if (_xmlReader.LocalName == "Obs" && !_xmlReader.IsStartElement()) // end of Obs tag
                {
                    SetGroupValues();
                    FillMissingAttributes();
                    return true;
                }
            }

            return false;
        }
    }
}
