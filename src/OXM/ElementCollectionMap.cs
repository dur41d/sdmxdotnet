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
        internal Func<ClassMap<TProperty>> ClassMapConstructor { get; set;}

        public ElementCollectionMap(XName name, bool required)
            : base(name, required, true)
        {}
      
        public override void ReadXml(XmlReader reader)
        {
            var classMap = ClassMapConstructor();
            classMap.Namespace = Name.Namespace;
            Collection.Set(classMap.ReadXml(reader));
           _occurances++;
        }

        public override void WriteXml(XmlWriter writer, T obj)
        {
            var values = Collection.Get(obj);

            if ((object)values == null)
            {
                if (Required)
                {
                    throw new OXMException("Element Collection '{0}' is required but its value is null. Collection: ({1}).{2}"
                        , Name, Collection.GetTypeName(), Collection.GetName());
                }
            }          
            else
            {
                if (Required && values.Count() == 0)
                {
                    throw new OXMException("Element Collection '{0}' is required but the collection is empty. Collection: ({1}).{2}"
                        , Name, Collection.GetTypeName(), Collection.GetName());
                }

                foreach (TProperty value in values)
                {
                    if (Writing != null) Writing();
                    writer.WriteStartElement(Name.LocalName, Name.NamespaceName);
                    var classMap = ClassMapConstructor();
                    classMap.Namespace = Name.Namespace;
                    classMap.WriteXml(writer, value);
                    writer.WriteEndElement();
                }
            }
        }
    }
}
