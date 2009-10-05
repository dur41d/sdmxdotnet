using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Common;

namespace SDMX.Tests
{
    public interface IAttributeMap<T>
    {
        void ToXml(XElement element, T parent);
        void SetPropertyValue(T parent);
        void SetAttributeValue(XElement element);
    }

    public class Attribute<T>
    {
        public string Name {get; set;}
        public bool Required {get; set;}
        public T Default {get; set;}
        public T Value { get; set; }
        public bool HasDefalut { get; set; }

        public Attribute()
        { }

        public Attribute(string name, bool required)
        {
            Name = name;
            Required = required;
        }

        public Attribute(string name, bool required, T defaultValue)
        {
            Name = name;
            Required = required;
            Default = defaultValue;
            HasDefalut = true;
        }
    }

    public interface IElementMap
    {       
    }

    public class ElementMap<C, P> : IElementMap
    {
        public string Name { get; set; }
        public int MinOccures { get; set; }
        public int MaxOccures { get; set; }
        private ClassMap<P> classMap;

        public ElementMap(string name, int minOccures, int maxOccures)
        {
            Name = name;
            MinOccures = minOccures;
            MaxOccures = maxOccures;
        }

        public ElementMap<C, P> Using(ClassMap<P> classMap)
        {
            this.classMap = classMap;
            return this;
        }
    }

    public abstract class ClassMap<C>
    {
        public abstract string ElementName { get; }
        private List<IAttributeMap<C>> list = new List<IAttributeMap<C>>();
        private List<IElementMap> elementMaps = new List<IElementMap>();

        protected abstract C CreateObject();

        public XElement ToXml(C obj)
        {
            XElement element = new XElement(ElementName);
            foreach (var p in list)
            {
                p.ToXml(element, obj);
            }
            return element;
        }

        public C ToObj(XElement element)
        {
            
            
            foreach (var p in list)
            {
                p.SetAttributeValue(element);
            }

            C obj = CreateObject();

            foreach (var p in list)
            {
                p.SetPropertyValue(obj);
            }
            return obj;
        }

        public AttributeMap<C, P> MapAttribute<P>(string name, bool required)
        {
            return MapAttribute(name, required, default(P), false);
        }

        public AttributeMap<C, P> MapAttribute<P>(string name, bool required, P defaultValue)
        {
            return MapAttribute<P>(name, required, defaultValue, true);
        }

        private AttributeMap<C, P> MapAttribute<P>(string name, bool required, P defaultValue, bool hasDefault)
        {
            var attributeMap = new AttributeMap<C, P>(name, required, defaultValue, hasDefault);
            list.Add(attributeMap);
            return attributeMap;
        }

        public ElementMap<C, P> MapElement<P>(string name, int minOccures, int maxOccures)
        {
            var elementMap = new ElementMap<C, P>(name, minOccures, maxOccures);
            elementMaps.Add(elementMap);
            return elementMap;
        }
    }

  
    public class AttributeMap<T, V> : IAttributeMap<T>
    {
        private Attribute<V> attribute {get; set;}
        private Func<T, V> getter;
        private Func<string, V> parser;
        private Action<T, V> setter;
        public V Value
        {
            get
            {
                return attribute.Value;
            }
        }

        public AttributeMap(string name, bool required, V defaultValue, bool hasDefault)
        {
            this.attribute = new Attribute<V>() { Name = name, Required = required, Default = defaultValue, HasDefalut = hasDefault };
        }

        public AttributeMap<T, V> Getter(Func<T, V> getter)
        {
            this.getter = getter;
            return this;
        }

        public AttributeMap<T, V> Setter(Action<T, V> setter)
        {
            this.setter = setter;
            return this;
        }

        public AttributeMap<T, V> Parser(Func<string, V> parser)
        {
            this.parser = parser;
            return this;
        }

        public void ToXml(XElement element, T parent)
        {
            Contract.AssertNotNull(() => getter);

            var value = getter(parent);
            if (!attribute.Required && attribute.HasDefalut && attribute.Default.Equals(value))
                return;
            element.SetAttributeValue(attribute.Name, value);
        }

        public void SetPropertyValue(T parent)
        {
            if (setter != null)
                setter(parent, attribute.Value);
        }

        public void SetAttributeValue(XElement element)
        {
            Contract.AssertNotNull(() => parser);
            var a = element.Attribute(attribute.Name);
                        
            if (a == null)
            {
                if (attribute.Required)
                {
                    throw new SDMXException(string.Format("Attribute '{0}' is required but was not found in element '{1}'"
                        , attribute.Name, element));
                }
                else
                {
                    if (attribute.HasDefalut)
                    {
                        attribute.Value = attribute.Default;
                    }
                }
            }
            else
            {
                attribute.Value = parser(a.Value);
            }
        }
    }


}
