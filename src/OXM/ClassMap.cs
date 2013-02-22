using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Common;

namespace OXM
{
    public abstract class ClassMap<T> : IElementMapContainer<T>, IAttributeMapContainer<T>, IElementContentContainer<T>
    {
        internal XNamespace Namespace { get; set; }
        protected RootElementMap<T> _rootMap;
        private MapList<T> _attributeMaps;
        private MapList<T> _elementMaps;
        private IMemberMap<T> _contentMap;
        private string[] _attributesOrder;
        private string[] _elementsOrder;
        private List<IMapBuilder<T>> builders = new List<IMapBuilder<T>>();

        private Action<ValidationMessage> _validationAction;
        private XmlReader _reader;

        public ClassMap()
        {
            string name = this.GetType().Name;

            _attributeMaps = new MapList<T>(name);
            _elementMaps = new MapList<T>(name);
        }

        protected abstract T Return();

        protected void SignalError(string message)
        {
            SignalValidatation(message, SeverityType.Error);
        }

        protected void SignalError(string format, params object[] args)
        {
            SignalValidatation(string.Format(format, args), SeverityType.Error);
        }        

        protected void SignalWarning(string message)
        {
            SignalValidatation(message, SeverityType.Warning);
        }

        protected void SignalWarning(string format, params object[] args)
        {
            SignalValidatation(string.Format(format, args), SeverityType.Warning);
        }

        protected void SignalValidatation(string message, SeverityType severity)
        {
            int lineNumber = 0;
            int linePosition = 0;
            var xml = _reader as IXmlLineInfo;
            var type = this.GetType();

            string signalMessage = null;
            if (xml != null)
            {
                lineNumber = xml.LineNumber;
                linePosition = xml.LinePosition;

                signalMessage = string.Format(@"Parse {0}: {1} 
Line: {2}, Position: {3}  
Type: {4}.", severity.ToString(), message, lineNumber, linePosition, type.Name);
            }
            else
            {
                signalMessage = string.Format(@"Parse {0}: {1}. Type: {2}.", 
                    severity.ToString(), message, type.Name);
            }

            if (_validationAction == null)
            {
                if (severity == SeverityType.Error)
                {
                    throw new ParseException(signalMessage) { LineNumber = lineNumber, LinePosition = linePosition };
                }
                else if (severity == SeverityType.Warning)
                {
                    System.Diagnostics.Debug.WriteLine(signalMessage, "Warning");
                }
            }
            else
            {
                _validationAction(new ValidationMessage(signalMessage, severity, lineNumber, linePosition));
            }
            
        }   

        bool _isBuilt = false;
        private void BuildAndVerifyMaps()
        {
            if (!_isBuilt)
            {
                builders.ForEach(b => b.BuildMaps(this));

                if (_contentMap != null && _elementMaps.Count() > 0)
                {
                    throw new MappingException("Class map for '{0}' has both elements and content. This is not possible.", typeof(T).ToString());
                }
                _isBuilt = true;
            }
        }

        internal void WriteXml(XmlWriter writer, T obj)
        {
            BuildAndVerifyMaps();

            foreach (var map in _attributeMaps.GetOrderedList(_attributesOrder))
            {
                map.WriteXml(writer, obj);
            }

            foreach (var map in _elementMaps.GetOrderedList(_elementsOrder))
            {
                map.WriteXml(writer, obj);
            }

            if (_contentMap != null)
            {
                _contentMap.WriteXml(writer, obj);
            }
        }

        internal T ReadXml(XmlReader reader, Action<ValidationMessage> validationAction)
        {
            _reader = reader;
            _validationAction = validationAction;

            BuildAndVerifyMaps();

            foreach (var attributeMap in _attributeMaps.GetOrderedList(_attributesOrder))
            {
                attributeMap.ReadXml(reader, validationAction);
            }

            if (_contentMap != null)
            {
                if (reader.IsEmptyElement)
                {                    
                    Helper.NotifyWarning(validationAction, reader, typeof(T), 
                        "Element '{0}' is empty but its content is mapped.", reader.GetXName());
                }

                _contentMap.ReadXml(reader, validationAction);
            }
            else
            {
                ReadElements(reader, _elementMaps, validationAction);
            }   

            return Return();
        }

        internal static void ReadElements(XmlReader reader, MapList<T> elementMaps, Action<ValidationMessage> validationAction)
        {
            var counts = new NameCounter<XName>();

            if (!reader.IsEmptyElement)
            {
                int depth = reader.Depth;

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.EndElement && reader.Depth == depth)
                    { 
                        break;
                    }
                    else if (reader.NodeType == XmlNodeType.Element)
                    {
                        XName name = reader.GetXName();
                        var elementMap = elementMaps.Get(name);
                        if (elementMap == null)
                        {
                            Helper.NotifyWarning(validationAction, reader, typeof(T), "Element '{0}' is not Mapped.", name);
                            continue;
                        }

                        elementMap.ReadXml(reader, validationAction);
                        counts.Increment(name);
                    }
                }
            }

            foreach (IElementMap<T> elementMap in elementMaps)
            {
                if (elementMap.Required && counts.Get(elementMap.Name) == 0)
                {
                    Helper.Notify(validationAction, reader, typeof(T), "Element '{0}' is required but was not found.", elementMap.Name);
                }
            }
        }

        protected PropertyMap<T, TProperty> Map<TProperty>(Func<T, TProperty> property)
        {
            var builder = new PropertyMap<T, TProperty>(property);
            builders.Add(builder);
            return builder;
        }

        protected CollectionMap<T, TProperty> MapCollection<TProperty>(Func<T, IEnumerable<TProperty>> collection)
        {
            var builder = new CollectionMap<T, TProperty>(collection);
            builders.Add(builder);
            return builder;
        }

        protected void AttributesOrder(params string[] order)
        {
            _attributesOrder = order;
        }

        protected void ElementsOrder(params string[] order)
        {
            _elementsOrder = order;
        }

        protected ContainerMap<T> MapContainer(XName name, bool required)
        {
            var builder = new ContainerMap<T>(name, required);
            builders.Add(builder);
            return builder;
        }

        #region IAttributeMapContainer<T> Members

        void IAttributeMapContainer<T>.AddAttributeMap(XName name, IMemberMap<T> map)
        {
            _attributeMaps.Add(name, map);
        }

        #endregion

        #region IElementMapContainer<T> Members

        void IElementMapContainer<T>.AddElementMap(XName name, IMemberMap<T> map)
        {
            _elementMaps.Add(name, map);
        }

        #endregion

        #region IElementContentContainer<T> Members

        void IElementContentContainer<T>.SetElementContentMap(IMemberMap<T> map)
        {
            if (_contentMap != null)
            {
                throw new MappingException("Element content is mapped more than once in {0}.", this.GetType().Name);
            }
            _contentMap = map;
        }

        #endregion

        #region IMapContainer<T> Members

        XNamespace IMapContainer<T>.Namespace
        {
            get { return Namespace; }
        }

        #endregion
    }
}
