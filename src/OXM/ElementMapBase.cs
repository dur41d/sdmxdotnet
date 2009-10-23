using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Common;
using System.Xml;

namespace OXM
{
    internal abstract class ElementMapBase<T> : IElementMap<T>
    {
        protected XName Name { get; set; }
        protected bool Required { get; set; }

        public Action Writing { protected get; set; }

        protected int _occurances;
        private bool _isCollection;                

        public ElementMapBase(XName name, bool required, bool isCollection)
        {
            Name = name;
            Required = required;
            _isCollection = isCollection;
        }

        public virtual void AssertValid()
        {
            if (Required && _occurances == 0)
            {
                throw new OXMException("Element '{0}' is required but was not found'", Name);
            }
            else if (_isCollection == false && _occurances > 1)
            {
                throw new OXMException("Element '{0}' is supposed to occure only once but occured '{1}' times. Use MapElementCollections instead.", Name, _occurances);
            }
        }

        public abstract void ReadXml(XmlReader reader);

        public abstract void WriteXml(XmlWriter writer, T obj);
    }
}
