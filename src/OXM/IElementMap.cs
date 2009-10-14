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
    public interface IElementMap<T> : IMap<T>
    {
        void AssertValid();
        void SetValue(XElement element);
    }
}
