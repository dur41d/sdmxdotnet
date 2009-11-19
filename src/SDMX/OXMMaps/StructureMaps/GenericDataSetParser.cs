using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace SDMX.Parsers
{
    //internal class GenericDataSetParser
    //{
    //    private KeyFamily keyFamily;        

    //    internal GenericDataSetParser(KeyFamily keyfamily)
    //    {
    //        if (keyFamily == null)
    //        {
    //            throw new ArgumentNullException("keyfamily");
    //        }

    //        this.keyFamily = keyfamily;
    //    }

    //    internal DataSet Parse(XElement datasetElement)
    //    {
    //        if (datasetElement == null)
    //        {
    //            throw new ArgumentNullException("datasetElement");
    //        }
    //        if (datasetElement.Name.LocalName != "DataSet")
    //        {
    //            throw new SDMXException("The element is not a DataSet");
    //        }

    //        var dataSet = new DataSet(keyFamily);

    //        foreach (var element in datasetElement.Elements())
    //        {               
    //            if (element.Name.LocalName == "KeyFamilyRef")
    //            {
    //                ParseKeyFamilyRef(element, dataSet);
    //            }
    //            else if (element.Name.LocalName == "Group")
    //            {
    //                var parser = new GroupParser(dataSet);
    //                var group = parser.Parse(element);
    //                dataSet.Groups.Add(group);
    //            }
    //            else if (element.Name.LocalName == "Series")
    //            {
    //                var parser = new SeriesParser(dataSet);
    //                var series = parser.Parse(element);
    //                dataSet.Series.Add(series);
    //            }
    //            else if (element.Name.LocalName == "Attributes")
    //            {
    //                var parser = new AttributesParser();
    //                parser.Parse(element, dataSet.Attributes, dataSet.KeyFamily);
    //            }
    //            else if (element.Name.LocalName == "Annotations")
    //            {
    //                var parser = new AnnotationsParser();
    //                parser.Parse(element, dataSet.Annotations);
    //            }
    //        }

    //        return dataSet;
    //    }

    //    private void ParseKeyFamilyRef(XElement element, DataSet dataSet)
    //    {
    //        // if dataset keyfamily is set then verify the reference is correct
    //        string keyFamilyRef = element.Value;
    //        if (dataSet.KeyFamily != null)
    //        {
    //            if (dataSet.KeyFamily.ID != keyFamilyRef)
    //            {
    //                throw new SDMXException("The KeyFamilyRef dataset value '{0}' is differnet than the one in the KeyFamily ID '{0}'"
    //                    .F(keyFamilyRef, dataSet.KeyFamily.ID));
    //            }
    //        }
    //    }

    //    private class GroupParser
    //    {
    //        private DataSet dataSet;

    //        internal GroupParser(DataSet dataSet)
    //        {
    //            this.dataSet = dataSet;
    //        }

    //        internal Group Parse(XElement element)
    //        {
    //            throw new NotImplementedException();
    //        }
    //    }

    //    private class LanguageParser
    //    {
    //        public Language Parse(string language)
    //        {
    //            if (language == "en")
    //            {
    //                return Language.English;
    //            }
    //            else
    //            {
    //                throw new SDMXException("Language '{0}' is not supported.".F(language));
    //            }
    //        }
    //    }

    //    private class AnnotationsParser
    //    {
    //        internal void Parse(XElement annotationsElement, IList<Annotation> list)
    //        {
    //            foreach (var listElement in annotationsElement.Elements())
    //            {
    //                if (listElement.Name.LocalName == "Annotation")
    //                {                        
    //                    foreach (var element in listElement.Elements())
    //                    {
    //                        Annotation annotation = new Annotation();                            

    //                        if (element.Name.LocalName == "AnnotationTitle")
    //                        {
    //                            annotation.Title = element.Value;
    //                        }
    //                        else if (element.Name.LocalName == "AnnotationType")
    //                        {
    //                            annotation.Type = element.Value;
    //                        }
    //                        else if (element.Name.LocalName == "AnnotationURL")
    //                        {
    //                            annotation.Url = new Uri(element.Value);
    //                        }
    //                        else if (element.Name.LocalName == "AnnotationText")
    //                        {
    //                            var languageParser = new LanguageParser();
    //                            Language language = languageParser.Parse(element.Attribute("lang").Value);
    //                            annotation.Text[language] = element.Value;
    //                        }

    //                        list.Add(annotation);
    //                    }
    //                }
    //            }
    //        }
    //    }

    //    private class ValueElementParser
    //    {
    //        public void Parse(XElement valueElement, out string concept, out string value, out string startTime)
    //        {
    //            concept = valueElement.Attribute("concept").Value;
    //            value = valueElement.Attribute("value").Value;
    //            startTime = null;
    //            var startTimeAttribute = valueElement.Attribute("startTime");
    //            if (startTimeAttribute != null)
    //            {
    //                startTime = startTimeAttribute.Value;
    //            }
    //        }
    //    }
    //    private class AttributesParser
    //    {         
    //        internal void Parse(XElement attributesElement, AttributeValues attributes, KeyFamily keyFamily)
    //        {
    //            foreach (var element in attributesElement.Elements())
    //            {
    //                if (element.Name.LocalName == "Value")
    //                {
    //                    var parser = new ValueElementParser();
    //                    string concept, value, startTime;
    //                    parser.Parse(element, out concept, out value, out startTime);
    //                    var attribute = keyFamily.GetAttribute(concept);
    //                    object attributeValue = attribute.GetValue(value, startTime);
    //                    attributes[attribute] = attributeValue;
    //                }
    //            }
    //        }
    //    }

    //    private class TimePeriodParser
    //    {
    //        Regex periodRegex = new Regex(@"^(\d\d\d\d\-Q1|\d\d\d\d\-Q2|\d\d\d\d\-Q3|\d\d\d\d\-Q4|\d\d\d\d\-T1|\d\d\d\d\-T2|\d\d\d\d\-T3|\d\d\d\d\-B1|\d\d\d\d\-B2|\d\d\d\d\-W1|\d\d\d\d\-W2|\d\d\d\d\-W3|\d\d\d\d\-W4|\d\d\d\d\-W5|\d\d\d\d\-W6|\d\d\d\d\-W7|\d\d\d\d\-W8|\d\d\d\d\-W9|\d\d\d\d\-W10|\d\d\d\d\-W11|\d\d\d\d\-W12|\d\d\d\d\-W13|\d\d\d\d\-W14|\d\d\d\d\-W15|\d\d\d\d\-W16|\d\d\d\d\-W17|\d\d\d\d\-W18|\d\d\d\d\-W19|\d\d\d\d\-W20|\d\d\d\d\-W21|\d\d\d\d\-W22|\d\d\d\d\-W23|\d\d\d\d\-W24|\d\d\d\d\-W25|\d\d\d\d\-W26|\d\d\d\d\-W27|\d\d\d\d\-W28|\d\d\d\d\-W29|\d\d\d\d\-W30|\d\d\d\d\-W31|\d\d\d\d\-W32|\d\d\d\d\-W33|\d\d\d\d\-W34|\d\d\d\d\-W35|\d\d\d\d\-W36|\d\d\d\d\-W37|\d\d\d\d\-W38|\d\d\d\d\-W39|\d\d\d\d\-W40|\d\d\d\d\-W41|\d\d\d\d\-W42|\d\d\d\d\-W43|\d\d\d\d\-W44|\d\d\d\d\-W45|\d\d\d\d\-W46|\d\d\d\d\-W47|\d\d\d\d\-W48|\d\d\d\d\-W49|\d\d\d\d\-W50|\d\d\d\d\-W51|\d\d\d\d\-W52)$");
    //        Regex gYearMonthRegex = new Regex(@"^\d\d\d\d-\d\d$");
    //        Regex gYearRegex = new Regex(@"^\d\d\d\d$");

    //        public TimePeriod Parse(string value)
    //        {
    //            if (gYearRegex.IsMatch(value))
    //            {
    //                int year = int.Parse(value);
    //                return new TimePeriod(year);
    //            }
    //            else if (gYearMonthRegex.IsMatch(value))
    //            {
    //                string[] split = value.Split(new[] { '-' });
    //                int year = int.Parse(split[0]);
    //                int month = int.Parse(split[1]);
    //                return new TimePeriod(year, month);
    //            }
    //            else
    //            {
    //                throw new SDMXException("Cannot parse time period '{0}'".F(value));
    //            }
    //        }
    //    }

    //    private class ValueParser
    //    {
    //        public ObservationValue Parse(XElement element, KeyFamily keyFamily)
    //        {
    //            var primaryMeasure = keyFamily.PrimaryMeasure;

    //            if (primaryMeasure.TextFormat.TextType == TextType.Double)
    //            {
    //                double dValue = double.Parse(element.Value);
    //                return new ObservationValue(dValue);
    //            }
    //            else if (primaryMeasure.TextFormat.TextType == TextType.Timespan)
    //            {
    //                string valueString = element.Attribute("value").Value;
    //                string startTimeString = null;
    //                var startTimeAttribute = element.Attribute("startTime");
    //                if (startTimeAttribute != null)
    //                {
    //                    startTimeString = startTimeAttribute.Value;
    //                }

    //                DateTime value = DateTime.Parse(valueString);
    //                DateTime startTime = DateTime.Parse(startTimeString);

    //                TimeSpan span = startTime - value;

    //                return new ObservationValue(span);
    //            }
    //            else
    //            { 
    //                throw new SDMXException("Primary measure type are not supported: '{0}' type '{1}'".F(primaryMeasure.TextFormat.TextType));
    //            }
    //        }
    //    }

    //    private class ObservationParser
    //    {
    //        private Series series;            

    //        internal ObservationParser(Series series)
    //        {
    //            this.series = series;
    //        }

    //        internal Observation Parse(XElement obsElement)
    //        {
    //            var observation = new Observation();

    //            foreach (var element in obsElement.Elements())
    //            {
    //                if (element.Name.LocalName == "Time")
    //                {                       
    //                    var timePeriodParser = new TimePeriodParser();
    //                    observation.Time = timePeriodParser.Parse(element.Value);
    //                }
    //                else if (element.Name.LocalName == "ObsValue")
    //                {
    //                    ValueParser parser = new ValueParser();
    //                    observation.Value = parser.Parse(element, series.DataSet.KeyFamily);
    //                }
    //                else if (element.Name.LocalName == "Attributes")
    //                {
    //                    var parser = new AttributesParser();
    //                    parser.Parse(element, observation.Attributes, series.DataSet.KeyFamily);
    //                }
    //                else if (element.Name.LocalName == "Annotations")
    //                {
    //                    var parser = new AnnotationsParser();
    //                    parser.Parse(element, observation.Annotations);
    //                }
    //            }

    //            return observation;
    //        }
    //    }

    //    private class SeriesParser
    //    {
    //        private DataSet dataSet;

    //        public SeriesParser(DataSet dataSet)
    //        {
    //            this.dataSet = dataSet;
    //        }

    //        internal Series Parse(XElement sereisElement)
    //        {
    //            var series = dataSet.CreateEmptySeries();

    //            foreach (var element in sereisElement.Elements())
    //            {
    //                if (element.Name.LocalName == "SeriesKey")
    //                {
    //                    ParseSeriesKey(element, series);
    //                }
    //                else if (element.Name.LocalName == "Obs")
    //                {
    //                    var parser = new ObservationParser(series);
    //                    var obs = parser.Parse(element);
    //                    series.Add(obs);
    //                }
    //                else if (element.Name.LocalName == "Attributes")
    //                {
    //                    var parser = new AttributesParser();
    //                    parser.Parse(element, series.Attributes, series.DataSet.KeyFamily);
    //                }
    //                else if (element.Name.LocalName == "Annotations")
    //                {
    //                    var parser = new AnnotationsParser();
    //                    parser.Parse(element, series.Annotations);
    //                }
    //            }

    //            return series;
    //        }

    //        private void ParseSeriesKey(XElement seriesKeyElement, Series series)
    //        {
    //            foreach (var element in seriesKeyElement.Elements())
    //            {
    //                string concept, value, startTime;
    //                var parser = new ValueElementParser();
    //                parser.Parse(element, out concept, out value, out startTime);
    //                var dimension = dataSet.KeyFamily.GetDimension(concept);
    //                object dimensionValue = dimension.GetValue(value, startTime);
    //                series.Key[dimension] = dimensionValue;
    //            }
    //        }
    //    }
    //}
}
