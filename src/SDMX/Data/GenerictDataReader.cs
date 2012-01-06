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

            while (XmlReader.Read())
            {
                if (XmlReader.LocalName == "Group" && XmlReader.IsStartElement())
                {
                    string groupId = XmlReader.GetAttribute("type");

                    if (groupId == null)
                        SDMXException.ThrowParseError(XmlReader, "Group is missing type attribute.");

                    var group = KeyFamily.Groups.Find(groupId);

                    if (group == null)
                        SDMXException.ThrowParseError(XmlReader, "Keyfamily does not contain group with id: {0}.", groupId);

                    var dict = new Dictionary<string, object>();
                    while (XmlReader.Read() && XmlReader.LocalName != "Series")
                    {
                        if (IsValueElement())
                            ParseValue((n, v) => dict.Add(n, v));
                    }

                    SetGroup(group, dict);
                } 


                if (XmlReader.LocalName == "Series" && XmlReader.IsStartElement())
                {
                    ClearSeries();

                    while (XmlReader.Read() && XmlReader.LocalName != "Obs")
                    {
                        if (IsValueElement())
                        {
                            ParseValue((n, v) => SetSeries(n, v));
                        }
                    }
                }

                if (XmlReader.LocalName == "Obs" && XmlReader.IsStartElement())
                {                    
                    ClearObs();

                    while (XmlReader.Read() && !(XmlReader.LocalName == "Obs" && !XmlReader.IsStartElement()))
                    {
                        if (XmlReader.LocalName == "Time" && XmlReader.IsStartElement())
                        {
                            string value = XmlReader.ReadString();
                            // TODO: improve exception message to include line number by using Try parse
                            // move parsing to DataReader class
                            SetObs(KeyFamily.TimeDimension.Concept.Id.ToString(), KeyFamily.TimeDimension.Parse(value, null));
                        }
                        else if (XmlReader.LocalName == "ObsValue" && XmlReader.IsStartElement())
                        {
                            string value = XmlReader.GetAttribute("value");
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
            return XmlReader.LocalName == "Value" && XmlReader.IsStartElement();
        }

        void ParseValue(Action<string, object> set)
        {
            string concept = XmlReader.GetAttribute("concept");
            string value = XmlReader.GetAttribute("value");
            string startTime = XmlReader.GetAttribute("startTime");
            var component = KeyFamily.GetComponent(concept);
            set(concept, component.Parse(value, startTime));
        }
    }
}
