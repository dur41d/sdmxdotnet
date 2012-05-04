using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Common;
using SDMX.Parsers;
using OXM;

namespace SDMX
{
    public abstract class TextFormat
    {
        internal abstract ISimpleTypeConverter Converter { get; }

        internal abstract bool TryCast(object obj, out object result);

        public abstract Type GetValueType();

        public object Parse(string s, string startTime)
        {
            object obj = null;

            if (TryParse(s, startTime, out obj))
            {
                return obj;
            }
            else
            { 
                throw new SDMXException("Cannot parse string '{0}' startTime '{1}' for text format type '{2}'.", s, startTime, this.GetType());
            }
        }

        public string Serialize(object obj, out string startTime)
        {
            string s = null;
            startTime = null;

            if (TrySerialize(obj, out s, out startTime))
            {
                return s;
            }
            else
            { 
                throw new SDMXException("Cannot serialize object type '{0}' for text format type '{1}'.", typeof(object), this.GetType());
            }
        }

        public bool CanSerialize(object obj)
        {
            string s, startTime = null;
            return TrySerialize(obj, out s, out startTime);
        }

        public bool CanParse(string s, string startTime)
        {
            object obj = null;
            return TryParse(s, startTime, out obj);
        }

        public virtual bool TryParse(string s, string startTime, out object obj)
        {
            if (Converter.CanConvertToObj(s))
            {
                obj = Converter.ToObj(s);
                return true;
            }
            else
            {
                obj = null;
                return false;
            }
        }

        public virtual bool TrySerialize(object obj, out string s, out string startTime)
        {
            startTime = null;
            object castObject = null;
            if (!TryCast(obj, out castObject))
            {
                // if cannot cast but the object is string and can be parsed
                // return the string as the serialized string
                if (obj is string)
                {                     
                    if (CanParse((string)obj, startTime))
                    {
                        s = (string)obj;
                        return true;
                    }
                }

                s = null;
                return false;
            }

            if (Converter.CanConvertToXml(castObject))
            {
                s = Converter.ToXml(castObject);
                return true;
            }
            else
            {
                s = null;
                return false;
            }
        }
    }
}