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

            while (XmlReader.Read() && !(XmlReader.LocalName == "DataSet" && !XmlReader.IsStartElement()))
            {
                if (XmlReader.LocalName == "Group" && XmlReader.IsStartElement())
                {                    
                    string groupId = XmlReader.GetAttribute("type");
                    Group group = null;                                        
                    if (IsNullOrEmpty(groupId))
                    {
                        AddValidationError(false, "Group is missing 'type' attribute.");
                    }
                    else
                    {
                        group = KeyFamily.Groups.Find(groupId);

                        if (group == null)
                        {
                            AddValidationError(false, "Keyfamily does not contain group with id: {0}.", groupId);
                        }
                    }

                    NewGroupValues();
                    while (XmlReader.Read() && XmlReader.LocalName != "Series")
                    {
                        if (group != null && IsValueElement())
                        {
                            ReadValue((n, v) => SetGroup(group, n, v), false);
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

                    // read until <Obs> or </Series> end element
                    while (XmlReader.Read() && !(XmlReader.LocalName == "Obs") && !(XmlReader.LocalName == "Series" && !XmlReader.IsStartElement()))
                    {
                        if (IsValueElement())
                        {
                            ReadValue((n, v) => SetSeries(n, v), true);
                        }
                    }

                    ValidateSeries();
                }

                if (XmlReader.LocalName == "Obs" && XmlReader.IsStartElement())
                {
                    ClearObs();

                    // read until </Obs> end element
                    while (XmlReader.Read() && !(XmlReader.LocalName == "Obs" && !XmlReader.IsStartElement()))
                    {
                        if (XmlReader.LocalName == "Time" && XmlReader.IsStartElement())
                        {
                            string value = XmlReader.ReadString();

                            if (KeyFamily.TimeDimension != null)
                            {
                                SetObs(KeyFamily.TimeDimension.Concept.Id, value);
                            }
                        }
                        else if (XmlReader.LocalName == "ObsValue" && XmlReader.IsStartElement())
                        {
                            string value = XmlReader.GetAttribute("value");

                            if (KeyFamily.PrimaryMeasure != null)
                            {
                                SetObs(KeyFamily.PrimaryMeasure.Concept.Id, value);
                            }
                        }
                        else if (IsValueElement())
                        {
                            ReadValue((n, v) => SetObs(n, v), false);
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

        void ReadValue(Action<string, string> set, bool isSeries)
        {
            string concept = XmlReader.GetAttribute("concept");
            string value = XmlReader.GetAttribute("value");
            //string startTime = XmlReader.GetAttribute("startTime");
            bool error = false;
            if (IsNullOrEmpty(concept))
            {
                AddValidationError("The Value element is missing 'concept' attribute.", isSeries);
                error = true;
            }

            if (IsNullOrEmpty(value))
            {
                AddValidationError("Value element is missing 'value' attribute.", isSeries);
                error = true;
            }

            if (!error)
            {
                set(concept, value);
            }
        }
    }
}
