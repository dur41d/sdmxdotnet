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
    //public abstract class BaseMap<T>
    //{
    //    protected Func<T> Constructor { get; set; }
    //    private bool isConstructed = false;

    //    public BaseMap<T> Create(Func<T> constructor)
    //    {
    //        Constructor = constructor;
    //        return this;
    //    }

    //    public void CreateObject(ref T obj)
    //    {
    //        if (Constructor != null)
    //        {
    //            if (isConstructed)
    //            { 
    //                throw new OXMException("Create has already been called for this object '{0}'.", obj);
    //            }
    //            obj = Constructor();
    //            isConstructed = true;
    //        }
    //    }
    //}
}
