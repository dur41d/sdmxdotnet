using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Common;
using OXM;
using System.Xml;

namespace SDMX.Parsers
{
    internal class IdConverter : SimpleTypeConverter<Id>
    {
        public override string ToXml(Id value)
        {
            return value.ToString();
        }

        public override Id ToObj(string value)
        {
            return (Id)value;
        }

        public override bool CanConvertToObj(string s)
        {            
            return Id.IsValid(s);
        }
    }   
}
