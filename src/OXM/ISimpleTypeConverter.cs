using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Common;

namespace OXM
{
    public interface ISimpleTypeConverter
    {
        string ToXml(object obj);
        object ToObj(string s);
        bool CanConvertToXml(object obj);
        bool CanConvertToObj(string s);
    }
}