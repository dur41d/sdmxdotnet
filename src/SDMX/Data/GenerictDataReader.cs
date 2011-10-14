using System.Collections.Generic;
using System.Xml;
using System;

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
                    string groupId = _xmlReader.GetAttribute("type");

                    if (groupId == null)
                        SDMXException.ThrowParseError(_xmlReader, "Group is missing type attribute.");

                    var group = KeyFamily.Groups.Find(groupId);

                    if (group == null)
                        SDMXException.ThrowParseError(_xmlReader, "Keyfamily does not contain group with id: {0}.", groupId);

                    var dict = new Dictionary<string, object>();
                    while (_xmlReader.Read() && _xmlReader.LocalName != "Series")
                    {
                        if (IsValueElement())
                            ParseValue((n, v) => dict.Add(n, v));
                    }

                    SetGroup(group, dict);
                } 


                if (_xmlReader.LocalName == "Series" && _xmlReader.IsStartElement())
                {
                    ClearSeries();

                    while (_xmlReader.Read() && _xmlReader.LocalName != "Obs")
                    {
                        if (IsValueElement())
                        {
                            ParseValue((n, v) => SetSeries(n, v));
                        }
                    }
                }

                if (_xmlReader.LocalName == "Obs" && _xmlReader.IsStartElement())
                {                    
                    ClearObs();

                    while (_xmlReader.Read() && !(_xmlReader.LocalName == "Obs" && !_xmlReader.IsStartElement()))
                    {
                        if (_xmlReader.LocalName == "Time" && _xmlReader.IsStartElement())
                        {
                            string value = _xmlReader.ReadString();
                            // TODO: improve exception message to include line number by using Try parse
                            // move parsing to DataReader class
                            SetObs(KeyFamily.TimeDimension.Concept.Id.ToString(), KeyFamily.TimeDimension.Parse(value, null));
                        }
                        else if (_xmlReader.LocalName == "ObsValue" && _xmlReader.IsStartElement())
                        {
                            string value = _xmlReader.GetAttribute("value");
                            SetObs(KeyFamily.PrimaryMeasure.Concept.Id.ToString(), KeyFamily.PrimaryMeasure.Parse(value, null));
                        }
                        else if (IsValueElement())
                        {
                            ParseValue((n, v) => SetObs(n, v));
                        }
                    }

                    SetRecord();
                    
                    return true;
                }
            }

            return false;
        }


        private bool IsValueElement()
        {
            return _xmlReader.LocalName == "Value" && _xmlReader.IsStartElement();
        }

        void ParseValue(Action<string, object> set)
        {
            string concept = _xmlReader.GetAttribute("concept");
            string value = _xmlReader.GetAttribute("value");
            string startTime = _xmlReader.GetAttribute("startTime");
            var component = KeyFamily.GetComponent(concept);
            set(concept, component.Parse(value, startTime));
        }
    }
}
