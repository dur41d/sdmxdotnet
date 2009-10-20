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
    internal abstract class ElementMapBase<T> : IElementMap<T>
    {
        protected XName Name { get; set; }

        protected int _occurances;
        private bool _isCollection;
        private bool _required;        

        public ElementMapBase(XName name, bool required, bool isCollection)
        {
            Name = name;
            _required = required;
            _isCollection = isCollection;
        }

        public virtual void AssertValid()
        {
            if (_required && _occurances == 0)
            {
                throw new OXMException("Element '{0}' is required but was not found'", Name);
            }
            else if (_isCollection == false && _occurances > 1)
            {
                throw new OXMException("Element '{0}' is supposed to occure only once but occured '{1}' times. Use MapElementCollections instead.", Name, _occurances);
            }
        }

        public abstract void ReadXml(XElement element);

        public abstract void WriteXml(XElement element, T obj);
    }
}
