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
    internal class SimpleElementCollectionMap<T, TProperty> : ElementCollectionMap<T, TProperty>
    {        
        public SimpleElementCollectionMap(XName name, bool required)
            : base(name, required)
        { }
    }
    
    internal class SimpleElementMap<T, TProperty> : ElementMap<T, TProperty>, IElementMap<T>
    {
        public SimpleElementMap(XName name, bool required)
            : base(name, required)
        { }
    }
}
