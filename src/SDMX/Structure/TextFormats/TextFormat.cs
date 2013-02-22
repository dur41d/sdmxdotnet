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

        public virtual bool TryParse(string s, string startTime, out object obj)
        {
            return Converter.TryParse(s, out obj);
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
                    object result = null;
                    if (Converter.TryParse((string)obj, out result))
                    {
                        s = (string)obj;
                        return true;
                    }
                }

                s = null;
                return false;
            }
            
            return Converter.TrySerialize(castObject, out s);
        }
    }
}