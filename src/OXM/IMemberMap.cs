using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Common;
using System.Xml;

namespace OXM
{
    internal interface IMemberMap<T>
    {
        void ReadXml(XmlReader reader);
        void WriteXml(XmlWriter writer, T obj);
    }
}
