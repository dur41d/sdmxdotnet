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
    public class StringValueElementMap : ValueElementMap<String>
    {
        public StringValueElementMap()
            : base(s => s)
        {
        }
    }
}
