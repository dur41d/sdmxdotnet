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
        public XName Name { get; protected set; }
        public bool Required { get; protected set; }

        public Action Writing { protected get; set; }

        public ElementMapBase(XName name, bool required)
        {
            Name = name;
            Required = required;
        }

        public abstract void ReadXml(XmlReader reader, Action<ValidationMessage> validationAction);

        public abstract void WriteXml(XmlWriter writer, T obj);
    }
}
