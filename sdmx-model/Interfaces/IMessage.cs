using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace SDMX_ML.Framework.Interfaces
{
    interface IMessage
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Returns a string containing a Message in SDMX_ML format</returns>
        string ToXml();
        XElement GetXml();
        XDocument GetXmlDocument();
    }
}
