using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Common;
using System.Runtime.Serialization;

namespace OXM
{  
    interface IMapBuilder<T>
    {
        void BuildMaps(IMapContainer<T> map);
    }
}
