using System;
using System.Collections.Generic;
using System.Text;
using SDMX_ML.Framework.Interfaces;
using SDMX_Generic = SDMX_ML.Framework.Generic;
using SDMX_Message = SDMX_ML.Framework.Message;
using System.Xml.Linq;

namespace SDMX_ML.Framework.Messages
{
    public class CompactData : IMessage
    {
        #region Fields

        private XDocument _xmldoc;
        private XElement _xml;
        private XNamespace _nsQueryMessage;
        private XNamespace _nsCompact;
        private XNamespace _default;

        private SDMX_Generic.DataSetType _dataset = null;
        private SDMX_Message.HeaderType _header = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates objects from the xml file in the parameter
        /// </summary>
        /// <param name="xml">A SDMX_ML QueryMessage with the DataWhere element in a string</param>
        public CompactData(string xml)
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
        public CompactData()
        {
            _dataset = new SDMX_Generic.DataSetType();
            _header = new SDMX_Message.HeaderType();
        }

        #endregion

        #region Create Objects from XML

        private void setNameSpace()
        {
            _nsQueryMessage = _xml.Name.Namespace;
            _nsCompact = _xml.GetNamespaceOfPrefix("compact");
            _default = _xml.GetDefaultNamespace();
        }

        private void setDataSet()
        {
            if (_xml.Element(_default + "DataSet") != null)
            {

            }
        }

        #endregion

        #region WriteXmlFile (From Objects to Xml)

        public string ToXml()
        {
            XElement element = GetXml();

            return element.ToString();
        }

        public XDocument GetXmlDocument()
        {
            XElement element = GetXml();

            return _xmldoc;
        }

        public XElement GetXml()
        {
            _xmldoc = new XDocument(
                new XDeclaration("1.0", "UTF-8", "yes"));

            XElement compactdata = new XElement(Namespaces.GetAllNamespaces("CompactData"));

            return compactdata;
        }

        #endregion

        #region Properties


        #endregion

    }
}
