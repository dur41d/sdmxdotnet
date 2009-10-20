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
    internal class ValueMap<T, TProperty> : SimpleTypeMap<T, TProperty>
    {
        protected override void WriteValue(XElement element, string value)
        {
            element.Value = value;
        }

        protected override string ReadValue(XElement element)
        {
            return element.Value;
        }
    }
}
