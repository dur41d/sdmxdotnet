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
    internal class ElementCollectionMap<T, TProperty> : ElementMapBase<T>
    {
        internal Collection<T, TProperty> Collection { get; set; }
        internal ClassMap<TProperty> ClassMap { get; set; }

        public ElementCollectionMap(XName name, bool required)
            : base(name, required, true)
        {}
      
        public override void ReadXml(XElement element)
        {
            var list = new List<TProperty>();
            list.Add(ClassMap.ReadXml(element));
            var next = element.NextNode;
            _occurances++;

            while (next.NodeType == XmlNodeType.Element
                    && ((XElement)next).Name == Name)
            {
                list.Add(ClassMap.ReadXml((XElement)next));
                next = next.NextNode;
                _occurances++;
            }                
            
            Collection.Set(list);            
        }

        public override void WriteXml(XElement element, T obj)
        {
            var values = Collection.Get(obj);
            if ((object)values != null)
            {
                foreach (TProperty value in values)
                {
                    XElement child = new XElement(Name);
                    ClassMap.WriteXml(child, value);
                    element.Add(child);
                }
            }
        }
    }
}
