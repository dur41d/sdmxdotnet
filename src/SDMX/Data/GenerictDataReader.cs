using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Common;
using SDMX.Parsers;

namespace SDMX
{
    public class GenericDataReader : DataReader
    {
        public GenericDataReader(XmlReader xmlReader, KeyFamily keyFamily)
        {
            Contract.AssertNotNull(xmlReader, "xmlReader");
            Contract.AssertNotNull(keyFamily, "keyFamily");

            _xmlReader = xmlReader;
            KeyFamily = keyFamily;
        }

        public override bool Read()
        {
            Value = null;

            while (_xmlReader.Read())
            {
                if (_xmlReader.LocalName == "Header" && _xmlReader.IsStartElement())
                {

                    var map = new OXM.FragmentMap<Header>(Namespaces.Message + "Header", new HeaderMap());
                    Value = map.ReadXml(_xmlReader);
                    return true;
                }
                else if (_xmlReader.LocalName == "Series" && _xmlReader.IsStartElement())
                {
                    _record.Clear();
                }               
                else if (_xmlReader.LocalName == "Obs" && _xmlReader.IsStartElement())
                {                    
                    ClearObsValues();
                }
                else if (_xmlReader.LocalName == "Time" && _xmlReader.IsStartElement())
                {
                    string value = _xmlReader.ReadElementContentAsString();
                    _record[KeyFamily.TimeDimension.Concept.ID.ToString()] = _converter.Parse(KeyFamily.TimeDimension, value, null);
                }
                else if (_xmlReader.LocalName == "ObsValue" && _xmlReader.IsStartElement())
                {
                    string value = _xmlReader.GetAttribute("value");
                    _record[KeyFamily.PrimaryMeasure.Concept.ID.ToString()] = _converter.Parse(KeyFamily.PrimaryMeasure, value, null);
                }
                else if (_xmlReader.LocalName == "Value" && _xmlReader.IsStartElement())
                {
                    string concept = _xmlReader.GetAttribute("concept");
                    string value = _xmlReader.GetAttribute("value");
                    var component = KeyFamily.GetComponent(concept);
                    _record[concept] = _converter.Parse(component, value, null);
                }
                else if (_xmlReader.LocalName == "Obs" && !_xmlReader.IsStartElement()) // end of Obs tag
                {   
                    return true;
                }
            }

            return false;
        }
    }
}
