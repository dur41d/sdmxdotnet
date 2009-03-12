using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using SDMX_Message = SDMX_ML.Framework.Message;
using SDMX_Generic = SDMX_ML.Framework.Generic;
using SDMX_ML.Framework.Interfaces;
using SDMX_Common = SDMX_ML.Framework.Common;


namespace SDMX_ML.Framework.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public class GenericData : IMessage
    {
        #region Fields

        private XDocument _xmldoc;
        private XElement _xml;
        private XNamespace _nsQueryMessage;
        private XNamespace _nsGeneric;
        private XNamespace _default;

        private SDMX_Generic.DataSetType _dataset = null;
        private SDMX_Message.HeaderType _header = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates objects from the xml file in the parameter
        /// </summary>
        /// <param name="xml">A SDMX_ML QueryMessage with the DataWhere element in a string</param>
        public GenericData(string xml)
        {
            _xml = XElement.Parse(xml);
            //_dataset = new SDMX_Generic.DataSet();

            setNameSpace();

            if(_header == null)
                _header = new SDMX_Message.HeaderType(_xml);

            setDataSet();
        }

        /// <summary>
        /// Creates a SDMX_ML GenericDataMessage that can write a xml file
        /// </summary>
        public GenericData()
        {
            _dataset = new SDMX_Generic.DataSetType();
            _header = new SDMX_Message.HeaderType();
        }

        #endregion

        #region Create Objects from XML

        private void setNameSpace()
        {
            _nsQueryMessage = _xml.Name.Namespace;
            _nsGeneric = _xml.GetNamespaceOfPrefix("generic");
            _default = _xml.GetDefaultNamespace();
        }

 
        private void setDataSet()
        {

            if(_xml.Element(_default + "DataSet") != null)
            {
                _dataset = new SDMX_Generic.DataSetType();

                XElement ds = _xml.Element(_default + "DataSet");

                if(ds.Element(_nsGeneric + "KeyFmilyRef") != null)
                    _dataset.Keyfamilyref = setKeyFamilyRef(ds.Element(_nsGeneric + "KeyFmilyRef"));

                if(ds.Element(_nsGeneric + "Attributes") != null)
                {
                    foreach(XElement e in ds.Elements(_nsGeneric + "Attributes"))
                        _dataset.Attributes.Add(setValueType(e));
                }

                if(ds.Element(_nsGeneric + "Group") != null)
                {
                    foreach(XElement group in ds.Elements(_nsGeneric + "Group"))
                        _dataset.Group.Add(setGroup(ds.Element(_nsGeneric + "Group")));
                }
                else if(ds.Element(_nsGeneric + "Series") != null)
                    _dataset.Series.Add(setSeries(ds.Element(_nsGeneric + "Series")));

                if(ds.Element(_nsGeneric + "Annotations") != null)
                {
                    foreach(XElement annotation in ds.Elements(_nsGeneric + "Annotations"))
                        _dataset.Annotations.Add(setAnnotation(annotation));
                }
            }
        }

        private SDMX_Common.IDType setKeyFamilyRef(XElement kfref)
        {
            SDMX_Common.IDType id = new SDMX_Common.IDType();

            if(kfref.Value != null)
                id.Value = kfref.Value;

            return id;
        }

        private SDMX_Generic.GroupType setGroup(XElement element)
        {
            SDMX_Generic.GroupType group = new SDMX_Generic.GroupType();

            if(element != null)
            {
                if(element.Attribute("type") != null)
                    group.Type = element.Attribute("type").Value;

                if(element.Element(_nsGeneric + "GroupKey") != null)
                {
                    foreach(XElement e in element.Elements(_nsGeneric + "GroupKey"))
                        group.Groupkey.Add(setValueType(e));
                }

                if(element.Element(_nsGeneric + "Attributes") != null)
                {
                    foreach(XElement e in element.Elements(_nsGeneric + "Attributes"))
                        group.Attributes.Add(setValueType(e));
                }

                if(element.Element(_nsGeneric + "Series") != null)
                {
                    foreach(XElement series in element.Elements(_nsGeneric + "Series"))
                        group.Series.Add(setSeries(series));
                }

                if(element.Element(_nsGeneric + "Annotations") != null)
                {
                    foreach(XElement annotation in element.Elements(_nsGeneric + "Annotations"))
                        group.Annotations.Add(setAnnotation(annotation));
                }

            }

            return group;
        }

        private SDMX_Generic.ValueType setValueType(XElement attribute)
        {
            SDMX_Generic.ValueType value = new SDMX_Generic.ValueType();

            if(attribute != null)
            {
                if(attribute.Value != null)
                    value.Value = attribute.Value;

                if(attribute.Attribute("concept") != null)
                {
                    if(attribute.Attribute("concept").Value != null)
                    {
                        SDMX_Common.IDType id = new SDMX_Common.IDType();
                        id.Value = attribute.Attribute("concept").Value;
                        value.Concept = id;
                    }
                }

                if(attribute.Attribute("value") != null)
                {
                    if(attribute.Attribute("value").Value != null)
                        value.Value = attribute.Attribute("value").Value;
                }

                if(attribute.Attribute("startTime") != null)
                {
                    if(attribute.Attribute("startTime").Value != null)
                        value.StartTime = attribute.Attribute("startTime").Value;
                }
            }

            return value;
            
        }

        private SDMX_Generic.SeriesType setSeries(XElement element)
        {
            SDMX_Generic.SeriesType series = new SDMX_Generic.SeriesType();

            if(element.Element(_nsGeneric + "SeriesKey") != null)
            {
                XElement serieskey = element.Element(_nsGeneric + "SeriesKey");

                foreach(XElement sk in serieskey.Elements(_nsGeneric + "Value"))
                    series.Serieskey.Add(setValueType(sk));
            }

            if(element.Element(_nsGeneric + "Attributes") != null)
            {
                XElement attributes = element.Element(_nsGeneric + "Attributes");

                foreach(XElement at in attributes.Elements(_nsGeneric + "Value"))
                    series.Attributes.Add(setValueType(at));
            }

            foreach(XElement obs in element.Elements(_nsGeneric + "Obs"))
                series.Obs.Add(setObs(obs));

            if(element.Element(_nsGeneric + "Annotations") != null)
            {
                foreach(XElement annotation in element.Elements(_nsGeneric + "Annotations"))
                    series.Annotations.Add(setAnnotation(annotation));
            }

            return series;
        }

        private SDMX_Generic.ObsType setObs(XElement element)
        {
            SDMX_Generic.ObsType obs = new SDMX_Generic.ObsType();

            if(element.Element(_nsGeneric + "Time") != null)
            {
                SDMX_Common.TimePeriodType time = new SDMX_Common.TimePeriodType();
                time.TimePeriod = element.Element(_nsGeneric + "Time").Value;
                obs.Time = time;
            }

            if(element.Element(_nsGeneric + "ObsValue") != null)
            {
                XElement obsvalue = element.Element(_nsGeneric + "ObsValue");

                SDMX_Generic.ObsValueType ovalue = new SDMX_Generic.ObsValueType();
            
                if(obsvalue.Attribute("value") != null)
                {
                    if(obsvalue.Attribute("value").Value != null)
                        ovalue.Value = Convert.ToDouble(obsvalue.Attribute("value").Value);
                }

                if(obsvalue.Attribute("startTime") != null)
                {
                    if(obsvalue.Attribute("startTime").Value != null)
                        ovalue.StartTime = obsvalue.Attribute("startTime").Value;
                }
                
                obs.ObsValue = ovalue;
            }

            if(element.Element(_nsGeneric + "Attributes") != null)
            {
                XElement attributes = element.Element(_nsGeneric + "Attributes");

                foreach(XElement at in attributes.Elements(_nsGeneric + "Value"))
                    obs.Attributes.Add(setValueType(attributes));
            }

            if(element.Element(_nsGeneric + "Annotations") != null)
            {
                XElement annotations = element.Element(_nsGeneric + "Annotations");

                foreach(XElement an in annotations.Elements(_nsGeneric + "Annotation"))
                    obs.Annotations.Add(setAnnotation(an));
            }

            return obs;
        }

        private SDMX_Common.AnnotationType setAnnotation(XElement element)
        {
            SDMX_Common.AnnotationType annotype = new SDMX_Common.AnnotationType();

            if(element.Element(_nsGeneric + "AnnotationTitle") != null)
            {
                if(element.Element(_nsGeneric + "AnnotationTitle").Value != null)
                    annotype.AnnotationTitle = element.Element(_nsGeneric + "AnnotationTitle").Value;
            }

            if(element.Element(_nsGeneric + "AnnotationType") != null)
            {
                if(element.Element(_nsGeneric + "AnnotationType").Value != null)
                    annotype.Annotationtype = element.Element(_nsGeneric + "AnnotationType").Value;
            }

            if(element.Element(_nsGeneric + "AnnotationURL") != null)
            {
                if(element.Element(_nsGeneric + "AnnotationURL").Value != null)
                    annotype.AnnotationUrl = element.Element(_nsGeneric + "AnnotationURL").Value;
            }

            if(element.Element(_nsGeneric + "AnnotationText") != null)
            {
                XElement antext = element.Element(_nsGeneric + "AnnotationText");

                SDMX_Common.TextType tx = new SDMX_Common.TextType();

                if(antext.Value != null)
                    tx.Text = antext.Value;
           
                if(antext.Attribute(XNamespace.Xml + "lang") != null)
                    tx.Lang = antext.Attribute(XNamespace.Xml + "lang").Value;

                annotype.AnnotationText = tx;
            }

            return annotype;
        }

        #endregion

        #region WriteXmlFile (From Objects to Xml) 

        /// <summary>
        /// Writing the SDMX_ML file
        /// </summary>
        /// <returns>A string containing the xml file in SDMX_ML format</returns>
        public string ToXml()
        {
            try
            {
                XElement x = GetXml();
                
                return _xmldoc.ToString();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public XDocument GetXmlDocument()
        {
            XElement x = GetXml();

            return _xmldoc;
        }

        public XElement GetXml()
        {
            _xmldoc = new XDocument(
                new XDeclaration("1.0", "UTF-8", "yes"));
            
                                   
            XElement genericdata = new XElement(Namespaces.GetNS("message") + "GenericData",
                new XAttribute(XNamespace.Xmlns + "generic", Namespaces.GetNS("generic")),
                new XAttribute(XNamespace.Xmlns + "common", Namespaces.GetNS("common")),
                new XAttribute(XNamespace.Xmlns + "compact", Namespaces.GetNS("compact")),
                new XAttribute(XNamespace.Xmlns + "cross", Namespaces.GetNS("cross")),
                new XAttribute(XNamespace.Xmlns + "query", Namespaces.GetNS("query")),
                new XAttribute(XNamespace.Xmlns + "structure", Namespaces.GetNS("structure")),
                new XAttribute(XNamespace.Xmlns + "utility", Namespaces.GetNS("utility")),
                new XAttribute(XNamespace.Xmlns + "xsi",Namespaces.GetXSI()),
                new XAttribute(Namespaces.GetXSI() + "schemaLocation", Namespaces.GetLocation())//Namespaces.GetNS("message") + "SDMXMessage.xsd")
                );
           
            if(_header != null)
            {
                _header.Prepared = DateTime.Now.ToString("s") + "-01:00";//DateTime.Now.ToString("yyyy-MM-dd" + "T" + "HH:mm:ss" + "-05:00");
                _header.Extracted = DateTime.Now.ToString("s") + "-01:00";
                _header.DataSetAction = SDMX_Message.HeaderType.EnumDataSetAction.Information;
            
                genericdata.Add(_header.GetXml());
            }

            if(_dataset.Group != null)
                genericdata.Add(getDataset(_dataset));

            _xmldoc.Add(genericdata);
            
            //_xmldoc.Save(@"c:\projektxml.xml");

            return genericdata;

        }

        private XElement getDataset(SDMX_Generic.DataSetType ds)
        {
            XElement dataset = new XElement(Namespaces.GetNS("message") + "DataSet");

            if(ds.Keyfamilyref != null)
                dataset.Add(getKeyfamilyRef(ds.Keyfamilyref));

            if(ds.Attributes.Count > 0)
            {
                XElement attribute = new XElement(Namespaces.GetNS("generic") + "Attributes");

                foreach(SDMX_Generic.ValueType att in ds.Attributes)
                    attribute.Add(getValueType(att, "Value"));

                dataset.Add(attribute);
            }

            //One of the Group or Series can exist more than one time
            //but not both at the same time
            if(ds.Group.Count > 0)
            {
                foreach(SDMX_Generic.GroupType group in ds.Group)
                    dataset.Add(getGroup(group));
            }
            else if(ds.Series.Count > 0)
            {
                foreach(SDMX_Generic.SeriesType seriestype in ds.Series)
                    dataset.Add(getSeries(seriestype));
            }

            if(ds.Annotations.Count > 0)
            {
                XElement annotations = new XElement(Namespaces.GetNS("generic") + "Annotations");
                
                foreach(SDMX_Common.AnnotationType antype in ds.Annotations)
                    annotations.Add(getAnnotation(antype));

                dataset.Add(annotations);
            }


            return dataset;

        }

        private XElement getKeyfamilyRef(SDMX_Common.IDType keyfref)
        {
            XElement element = new XElement(Namespaces.GetNS("generic") + "KeyFamilyRef");

            if(keyfref != null)
            {
                if(keyfref.Value != null)
                    element.Value = keyfref.Value;
            }

            return element;
        }

        private XElement getGroup(SDMX_Generic.GroupType grouptype)
        {
            XElement group = new XElement(Namespaces.GetNS("generic") + "Group");

            if(grouptype.Type != null)
                group.Add(new XAttribute("type", grouptype.Type));

            if(grouptype.Groupkey.Count > 0)
            {
                XElement groupkey = new XElement(Namespaces.GetNS("generic") + "GroupKey");

                foreach(SDMX_Generic.ValueType valuetype in grouptype.Groupkey)
                    groupkey.Add(getValueType(valuetype, "Value"));

                group.Add(groupkey);
            }
              
            if(grouptype.Attributes.Count > 0)
            {
                XElement attribute = new XElement(Namespaces.GetNS("generic") + "Attributes");

                foreach(SDMX_Generic.ValueType att in grouptype.Attributes)
                    attribute.Add(getValueType(att, "Value"));

                group.Add(attribute);
            }

            if(grouptype.Series.Count > 0)
            {
                foreach(SDMX_Generic.SeriesType serie in grouptype.Series)
                    group.Add(getSeries(serie));
            }

            if(grouptype.Annotations.Count > 0)
            {
                XElement annotations = new XElement(Namespaces.GetNS("generic") + "Annotations");

                foreach(SDMX_Common.AnnotationType antype in grouptype.Annotations)
                    annotations.Add(getAnnotation(antype));

                group.Add(annotations);
            }

            return group;

        }

        private XElement getValueType(SDMX_Generic.ValueType valuetype, string elementname)
        {
            XElement element = new XElement(Namespaces.GetNS("generic") + elementname);

            if(valuetype.Value != null)
                element.Add(new XAttribute("value", valuetype.Value));

            if(valuetype.Concept != null)
            {
                SDMX_Common.IDType id = valuetype.Concept;
                element.Add(new XAttribute("concept", id.Value));
            }

            if(valuetype.StartTime != null)
                element.Add(new XAttribute("startTime", valuetype.StartTime));

            return element;
        }

         private XElement getSeries(SDMX_Generic.SeriesType seriestype)
        {
            XElement element = new XElement(Namespaces.GetNS("generic") + "Series");
            
            if(seriestype.Serieskey.Count > 0)
            {
                XElement serieskey = new XElement(Namespaces.GetNS("generic") + "SeriesKey");

                foreach(SDMX_Generic.ValueType sk in seriestype.Serieskey)
                    serieskey.Add(getValueType(sk, "Value"));

                element.Add(serieskey);
            }

            if(seriestype.Attributes.Count > 0)
            {
                XElement attribute = new XElement(Namespaces.GetNS("generic") + "Attributes");

                foreach(SDMX_Generic.ValueType at in seriestype.Attributes)
                    attribute.Add(getValueType(at, "Value"));

                element.Add(attribute);
            }

            if(seriestype.Obs.Count > 0)
            {
                foreach(SDMX_Generic.ObsType obstype in seriestype.Obs)
                    element.Add(getObs(obstype));

            }

            return element;
        }

        private XElement getObs(SDMX_Generic.ObsType obstype)
        {
            XElement element = new XElement(Namespaces.GetNS("generic") + "Obs");

            if(obstype.Time != null)
            {
                SDMX_Common.TimePeriodType period = obstype.Time;

                if(period.TimePeriod != null)
                {
                    XElement time = new XElement(Namespaces.GetNS("generic") + "Time");
                    time.Value = period.TimePeriod;
                    element.Add(time);
                }
            }

            if(obstype.ObsValue != null)
            {
                SDMX_Generic.ObsValueType obsvalue = new SDMX_Generic.ObsValueType();
                obsvalue = obstype.ObsValue;

                XElement value = new XElement(Namespaces.GetNS("generic") + "ObsValue");
                value.Add(new XAttribute("value", obsvalue.Value));

                if(obsvalue.StartTime != null)
                    value.Add(new XAttribute("startTime", obsvalue.StartTime));

                element.Add(value);

                if(obstype.Attributes.Count > 0)
                {
                    XElement attributes = new XElement(Namespaces.GetNS("generic") + "Attributes");

                    foreach(SDMX_Generic.ValueType valuetype in obstype.Attributes)
                        attributes.Add(getValueType(valuetype, "Value"));

                    element.Add(attributes);
                }
            }

            if(obstype.Annotations.Count > 0)
            {
                XElement annotations = new XElement(Namespaces.GetNS("generic") + "Annotations");

                foreach(SDMX_Common.AnnotationType antype in obstype.Annotations)
                    annotations.Add(getAnnotation(antype));

                element.Add(annotations);
            }

            return element;
        }

        private XElement getAnnotation(SDMX_Common.AnnotationType antype)
        {
            XElement element = new XElement(Namespaces.GetNS("generic") + "Annotation");

            if(antype.AnnotationTitle != null)
            {
                XElement annotitle = new XElement(Namespaces.GetNS("generic") + "AnnotationTitle");
                annotitle.Value = antype.AnnotationTitle;

                element.Add(annotitle);
            }

            if(antype.Annotationtype != null)
            {
                XElement annotype = new XElement(Namespaces.GetNS("generic") + "AnnotationType");
                annotype.Value = antype.Annotationtype;

                element.Add(annotype);
            }

            if(antype.AnnotationUrl != null)
            {
                XElement annourl = new XElement(Namespaces.GetNS("generic") + "AnnotationURL");
                annourl.Value = antype.AnnotationUrl;

                element.Add(annourl);
            }

            if(antype.AnnotationText != null)
            {
                XElement annotext = new XElement(Namespaces.GetNS("generic") + "AnnotationText");
                SDMX_Common.TextType tx = antype.AnnotationText;

                if(tx.Text != null)
                    annotext.Value = tx.Text;

                if(tx.Lang != null)
                    annotext.Add(new XAttribute(XNamespace.Xml + "lang", tx.Lang));

                element.Add(annotext);
            }

            return element;
        }

        #endregion

        #region Properties

        public SDMX_Message.HeaderType Header
        {
            get { return _header; }
            set { _header = value; }
        }
        
        
        public SDMX_Generic.DataSetType Dataset
        {
            get { return _dataset; }
            set { _dataset = value; }
        }

        #endregion
    }
}
