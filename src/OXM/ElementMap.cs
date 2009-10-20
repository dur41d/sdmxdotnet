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
    internal class ElementMap<T, TProperty> : ElementMapBase<T>
    {
        internal Property<T, TProperty> Property { get; set; }
        internal ClassMap<TProperty> ClassMap { get; set; }

        public ElementMap(XName name, bool required)
            : base(name, required, false)
        {
        }

        public override void ReadXml(XElement element)
        {  
            Property.Set(ClassMap.ReadXml(element));
            _occurances++;
        }

        public override void WriteXml(XElement element, T obj)
        {
            var value = Property.Get(obj);
            if ((object)value != null)
            {
                XElement child = new XElement(Name);
                ClassMap.WriteXml(child, value);
                element.Add(child);
            }
        }
    }
}
