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
    internal interface IElementMap<T> : IMemberMap<T>
    {
        XName Name { get; }
        bool Required { get; }
        Action Writing { set; }
    }
}
