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

        public ElementMapBase(XName name, bool required)
        {
            Name = name;
            Required = required;
        }

        public void AssertValid()
        {
            if (Required && _occurances == 0)
            {
                throw new OXMException("Element '{0}' is required but was not found'", Name);
            }
        }

        public abstract void ReadXml(XmlReader reader);

        public abstract void WriteXml(XmlWriter writer, T obj);
    }
}
