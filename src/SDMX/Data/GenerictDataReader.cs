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

            ClearErrors();

            while (XmlReader.Read())
            {
                if (XmlReader.LocalName == "Group" && XmlReader.IsStartElement())
                {                    
                    string groupId = XmlReader.GetAttribute("type");
                    Group group = null;                                        
                    if (IsNullOrEmpty(groupId))
                    {
                        AddParseError("Group is missing 'type' attribute.");
                    }
                    else
                    {
                        group = KeyFamily.Groups.Find(groupId);

                        if (group == null)
                        {
                            AddValidationError(string.Format("Keyfamily does not contain group with id: {0}.", groupId));
                        }
                    }

                    NewGroupValues();
                    while (XmlReader.Read() && XmlReader.LocalName != "Series")
                    {
                        if (group != null && IsValueElement())
                        {
                            ReadValue((n, v) => SetGroup(group, n, v));
                        }
                    }

                    if (group != null)
                    {
                        ValidateGroup(group);
                    }
                }


                if (XmlReader.LocalName == "Series" && XmlReader.IsStartElement())
                {
                    ClearSeries();

                    while (XmlReader.Read() && XmlReader.LocalName != "Obs")
                    {
                        if (IsValueElement())
                        {
                            ReadValue((n, v) => SetSeries(n, v));
                        }
                    }

                    ValidateSeries();
                }

                if (XmlReader.LocalName == "Obs" && XmlReader.IsStartElement())
                {
                    ClearObs();

                    while (XmlReader.Read() && !(XmlReader.LocalName == "Obs" && !XmlReader.IsStartElement()))
                    {
                        if (XmlReader.LocalName == "Time" && XmlReader.IsStartElement())
                        {
                            string value = XmlReader.ReadString();                            
                            SetObs(KeyFamily.TimeDimension.Concept.Id, value);
                        }
                        else if (XmlReader.LocalName == "ObsValue" && XmlReader.IsStartElement())
                        {
                            string value = XmlReader.GetAttribute("value");
                            SetObs(KeyFamily.PrimaryMeasure.Concept.Id, value);
                        }
                        else if (IsValueElement())
                        {
                            ReadValue((n, v) => SetObs(n, v));
                        }
                    }

                    ValidateObs();
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

        void ReadValue(Action<string, string> set)
        {
            string concept = XmlReader.GetAttribute("concept");
            string value = XmlReader.GetAttribute("value");
            //string startTime = XmlReader.GetAttribute("startTime");
            bool error = false;
            if (IsNullOrEmpty(concept))
            {
                AddParseError("The Value element is missing 'concept' attribute.");
                error = true;
            }

            if (IsNullOrEmpty(value))
            {
                AddParseError("Value element is missing 'value' attribute.");
                error = true;
            }

            if (!error)
            {
                set(concept, value);
            }
        }
    }
}
