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
    //internal class SimpleElementMap<T, TProperty> : SimpleTypeMap<T, TProperty>
    //{
    //    private XName _name;
    //    private bool _required;

    //    public SimpleElementMap(XName name, bool required)
    //    {
    //        _name = name;
    //        _required = required;
    //    }

    //    protected override void WriteValue(XElement element, string value)
    //    {
    //        if (value == null)
    //        {
    //            // if the attribute is required through an exception otherwise do nothing
    //            if (_required)
    //            {
    //                throw new OXMException("Element '{0}' is required but its value is null.", _name);
    //            }
    //        }
    //        else
    //        {   
    //            XElement child = new XElement(_name);
    //            child.Value = value;
    //            element.Add(child);
    //        }
    //    }

    //    protected override string ReadValue(XElement element)
    //    {
    //        var children = element.Elements(_name);
    //        int count = children.Count();

    //        if (count == 0)
    //        {
    //            if (_required)
    //            {
    //                throw new OXMException("Element '{0}' is required but was not found.");
    //            }
    //            else
    //            {
    //                return null;
    //            }
    //        }
    //        if (count > 1)
    //        { 
    //            throw new OXMException("Element '{0}' was found more than once 
    //        }

    //    }
    //}
}
