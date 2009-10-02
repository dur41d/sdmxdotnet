using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SDMX.Tests
{
    public class DimensionMap : ClassMap<DimensionTest>
    {
        public DimensionMap(DSD dsd)
        {            
            //Map(d => d.Concept).ToAttributes("concept", "conceptAgency")
            //    .Setter(() =>
            //        {
            //            return dsd.GetConcept(ValueOf("concept"), ValueOf("conceptAgency"));
            //        })
            //    .Getter(d =>
            //        { 
            //            d.Concept.i
            //        }
            //    );

            //Map<bool>(d => d.IsMeasureDimension).ToAttribute("isMeasureDimension", false, false)
            //    .Getter((d, b) => d.IsMeasureDimension = b)
            //    .Setter(d => d.IsMeasureDimension);

            MapAttribute<bool>("isMeasureDimension", false, false)
                .Getter(d => d.IsMeasureDimension)                
                .Setter((d,b) => d.IsMeasureDimension = b)
                .Parser(s => bool.Parse(s));
        }
    }


    public interface IPropertyMap<T>
    {
        void ToXml(XElement element, T parent);
        void ToObj(XElement element, T parent);
    }

    public class Attribute<T>
    {
        public string Name {get; set;}
        public bool Required {get; set;}
        public T Default {get; set;}
        public T Value { get; set; }
    }

    public class PropertyMap<C, P>
    {
        List<Attribute<P>> attributes = new List<Attribute<P>>();
        Func<C, P> getter;
        Action<C, P> setter;
        
        public PropertyMap<C, P> ToAttributes(params string[] attributeNames)
        {
            return this;
        }

        public PropertyMap<C, P> Getter(Func<C, P> getter)
        {
            this.getter = getter;
            return this;
        }

        public PropertyMap<C, P> Setter(Action<C, P> setter)
        {
            this.setter = setter;
            return this;
        }

        public string ValueOf(string attribute)
        {            
            return null;
        }

        public PropertyMap<C, P> ToAttribute(string attribute, bool required, P defaultt)
        {
            var a = new Attribute<P>() { Name = attribute, Required = required, Default = defaultt };
            attributes.Add(a);

            return this;
        }

        public void ToXml(XElement element)
        {
            foreach (var a in attributes)
            { 
             //   element.SetAttributeValue(a.Name, 
            }
        }
    }



    public class ClassMap<C>
    {
        private List<IPropertyMap<C>> list = new List<IPropertyMap<C>>();

        //public PropertyMap<C, P> Map<P>(Func<C, IPropertyMap> func)
        //{
        //    var p = new PropertyMap<C, P>();
        //    list.Add(p);
        //    return p;
        //}

        public void ToXml(XElement element, C obj)
        {            
            foreach (var p in list)
            {
                p.ToXml(element, obj);
            }            
        }

        public void ToObj(XElement element, C obj)
        {           
            foreach (var p in list)
            {
                p.ToObj(element, obj);
            }            
        }

        public AttributeMap<C, P> MapAttribute<P>(string name, bool required, P defaultValue)
        {
            var attributeMap = new AttributeMap<C, P>(name, required, defaultValue);
            list.Add(attributeMap);
            return attributeMap;
        }
    }



    public class AttributeMap<T, V> : IPropertyMap<T>
    {
        private Attribute<V> attribute;
        private Func<T, V> getter;
        private Func<string, V> parser;
        private Action<T, V> setter;

        public AttributeMap(string name, bool required, V defaultValue)
        {
            this.attribute = new Attribute<V>() { Name = name, Required = required, Default = defaultValue };
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
            AssertNotNull(getter);
            element.SetAttributeValue(attribute.Name, getter(parent));
        }

        public void ToObj(XElement element, T parent)
        {
            AssertNotNull(setter);
            AssertNotNull(parser);
            var a = element.Attribute(attribute.Name);
            V value;
            if (a == null)
            {
                if (attribute.Required)
                {
                    throw new Exception();
                }
                else
                {
                    value = attribute.Default;
                }
            }
            else
            {
                value = parser(a.Value);
            }

            setter(parent, value);
        }

        private void AssertNotNull(object arg)
        {
            if (parser == null)
            {
                throw new ArgumentNullException();
            }
        }
    }

    public class DSD
    { 
        public Concept GetConcept(string concept, string conceptAgency)
        {
            return null;
        }
    }
}
