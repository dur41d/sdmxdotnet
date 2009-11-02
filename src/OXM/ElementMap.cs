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
    internal class ElementMap<T, TProperty> : ElementMapBase<T>
    {
        internal Property<T, TProperty> Property { get; set; }
        
        private ClassMap<TProperty> _classMap;
        internal ClassMap<TProperty> ClassMap
        {
            get
            {
                return _classMap;
            }
            set
            {
                _classMap = value;
                _classMap.Namespace = Name.Namespace;
            }
        }

        public ElementMap(XName name, bool required)
            : base(name, required)
        {
        }

        public override void ReadXml(XmlReader reader)
        {
            if (_occurances == 1)
            {
                throw new OXMException("Element '{0}' has already occured and it is not supposed to occure more than once.", Name);
            }
            
            TProperty property = ClassMap.ReadXml(reader);

            if ((object)property != null)
                Property.Set(property);

            _occurances++;
        }

        public override void WriteXml(XmlWriter writer, T obj)
        {
            var value = Property.Get(obj);
            if ((object)value == null)
            {
                if (Required)
                {
                    throw new OXMException("Element '{0}' is required but its property value is null. Property: ({1}).{2}"
                        , Name, Property.GetTypeName(), Property.GetName());
                }
            }
            else
            {
                if (Writing != null) Writing();
                writer.WriteStartElement(Name.LocalName, Name.NamespaceName);                
                ClassMap.WriteXml(writer, value);
                writer.WriteEndElement();
            }
        }
    }
}
