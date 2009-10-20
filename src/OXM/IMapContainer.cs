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
    interface IMapContainer<T>
    {}

    interface IElementMapContainer<T> : IMapContainer<T>
    {
        void AddElementMap(XName name, IMemberMap<T> map);
    }

    interface IAttributeMapContainer<T> : IMapContainer<T>
    {
        void AddAttributeMap(XName name, IMemberMap<T> map);
    }

    interface IElementContentContainer<T> : IMapContainer<T>
    {
        void SetElementContentMap(IMemberMap<T> map);
    }
}
