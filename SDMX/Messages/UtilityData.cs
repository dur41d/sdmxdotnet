using System;
using System.Collections.Generic;
using System.Text;
using SDMX_Generic = SDMX_ML.Framework.Generic;
using SDMX_ML.Framework.Interfaces;
using System.Xml;
using System.Xml.Linq;


namespace SDMX_ML.Framework.Messages
{
    public class UtilityData : IMessage
    {
        private List<SDMX_Generic.DataSetType> _dataset = new List<SDMX_Generic.DataSetType>();

        private XDocument _xmldoc;

        public List<SDMX_Generic.DataSetType> Dataset
        {
            get { return _dataset; }
            set { _dataset = value; }
        }

        public string ToXml()
        {
            return "";
        }

        public XDocument GetXmlDocument()
        {
            XElement x = GetXml();
            
            return _xmldoc;
        }

        public XElement GetXml()
        {
            XElement element = new XElement("", "");

            return element;

        }
    }
}
