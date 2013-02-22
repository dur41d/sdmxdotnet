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
        public override bool TrySerialize(Id obj, out string s)
        {
            s = obj.ToString();
            return true;
        }

        public override bool TryParse(string s, out Id obj)
        {
            return Id.TryParse(s, out obj);
        }
    }   
}
