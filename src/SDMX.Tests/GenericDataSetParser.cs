using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Xml.Linq;

namespace SDMX.Tests.NewModel
{
    public class GenericDataSetParser
    {
        private KeyFamily keyFamily;

        public GenericDataSetParser(KeyFamily keyfamily)
        {
            this.keyFamily = keyfamily;
        }

        public DataSet Parse(XElement datasetElement)
        {
            if (datasetElement == null)
            {
                throw new ArgumentNullException("element");
            }
            if (datasetElement.Name.LocalName != "DataSet")
            {
                throw new SDMXException("The element is not a DataSet");
            }

            var dataSet = new DataSet(keyFamily);

            foreach (var element in datasetElement.Elements())
            {
                if (element.Name.LocalName == "KeyFamilyRef")
                {
                    ParseKeyFamilyRef(dataSet, element);
                }
                else if (element.Name.LocalName == "Group")
                {
                    var parser = new GroupParser(dataSet);
                    var group = parser.Parse(element);
                    dataSet.Groups.Add(group);
                }
                else if (element.Name.LocalName == "Series")
                {
                    var parser = new SeriesParser(dataSet);
                    var series = parser.Parse(element);
                    dataSet.Series.Add(series);
                }
                else if (element.Name.LocalName == "Attributes")
                {
                    if (dataSet.Attributes.Count > 0)
                    {
                        throw new SDMXException("Attributes element apprears more than once in DataSet element '{0}'".F(datasetElement));
                    }

                    var parser = new AttributesParser();
                    var attributes = parser.Parse(element, dataSet);
                    dataSet.Attributes = attributes;
                }
                else if (element.Name.LocalName == "Annotations")
                {
                    if (dataSet.Annotations.Count > 0)
                    {
                        throw new SDMXException("Annotations element apprears more than once in DataSet element '{0}'".F(datasetElement));
                    }

                    var parser = new AnnotationsParser();
                    var annotations = parser.Parse(element, dataSet);
                    dataSet.Annotations = annotations;
                }
                else
                {
                    throw new SDMXException("Invalid DataSet element '{0}'".F(element));
                }
            }


            return dataSet;
        }

        private void ParseKeyFamilyRef(DataSet dataSet, XElement element)
        {
            // if dataset keyfamily is set then verify the reference is correct
            string keyFamilyRef = element.Value;
            if (dataSet.KeyFamily != null)
            {
                if (dataSet.KeyFamily.ID != keyFamilyRef)
                {
                    throw new SDMXException("The KeyFamilyRef dataset value '{0}' is differnet than the one in the KeyFamily ID '{0}'"
                        .F(keyFamilyRef, dataSet.KeyFamily.ID));
                }
            }
        }

        private class GroupParser
        {
            private DataSet dataSet;

            internal GroupParser(DataSet dataSet)
            {
                this.dataSet = dataSet;
            }

            internal Group Parse(XElement element)
            {
                throw new NotImplementedException();
            }
        }

        private class AnnotationsParser
        {
            internal List<Annotation> Parse(XElement element, DataSet dataSet)
            {
                throw new NotImplementedException();
            }

            internal List<Annotation> Parse(XElement element, Group group)
            {
                throw new NotImplementedException();
            }

            internal List<Annotation> Parse(XElement element, Series series)
            {
                throw new NotImplementedException();
            }
        }

        private class AttributesParser
        {
            internal List<Attribute> Parse(XElement element, DataSet dataSet)
            {
                throw new NotImplementedException();
            }

            internal List<Attribute> Parse(XElement element, Group group)
            {
                throw new NotImplementedException();
            }

            internal List<Attribute> Parse(XElement element, Series series)
            {
                throw new NotImplementedException();
            }
        }

        private class SeriesParser
        {
            private DataSet dataSet;
            public SeriesParser(DataSet dataSet)
            {
                this.dataSet = dataSet;
            }

            internal Series Parse(XElement sereisElement)
            {
                var series = dataSet.CreateEmptySeries();

                foreach (var element in sereisElement.Elements())
                {
                    if (element.Name.LocalName == "SeriesKey")
                    {
                        ParseSeriesKey(element, series);
                    }
                    else if (element.Name.LocalName == "Obs")
                    {
                        ParseObs(element, series);
                    }
                    else if (element.Name.LocalName == "Attributes")
                    {
                        if (series.Attributes.Count > 0)
                        {
                            throw new SDMXException("Attributes element apprears more than once in Series element '{0}'".F(sereisElement));
                        }

                        var parser = new AttributesParser();
                        var attributes = parser.Parse(element, series);
                        series.Attributes = attributes;
                    }
                    else if (element.Name.LocalName == "Annotations")
                    {
                        if (series.Annotations.Count > 0)
                        {
                            throw new SDMXException("Annotations element apprears more than once in Series element '{0}'".F(sereisElement));
                        }

                        var parser = new AnnotationsParser();
                        var annotations = parser.Parse(element, series);
                        series.Annotations = annotations;
                    }
                    else
                    {
                        throw new SDMXException("Invalid Series element '{0}'".F(element));
                    }

                }

                return null;
            }

            private void ParseObs(XElement obsElement, Series series)
            {
                throw new NotImplementedException();
            }

            private void ParseSeriesKey(XElement seriesKeyElement, Series series)
            {
                foreach (var element in seriesKeyElement.Elements())
                {
                    string concept = element.Attribute("concept").Value;
                    string value = element.Attribute("value").Value;
                    string startTime = null;
                    var startTimeAttribute = element.Attribute("startTime");
                    if (startTimeAttribute != null)
                    {
                        startTime = startTimeAttribute.Value;
                    }

                    
                }
            }
        }
    }
}
