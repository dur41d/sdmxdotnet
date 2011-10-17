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
    public abstract class TextFormat : IEquatable<TextFormat>
    {
        public abstract bool IsValid(object value);

        internal abstract ISimpleTypeConverter Converter { get; }

        public virtual object Parse(string s, string startTime)
        {
            return Converter.ToObj(s);
        }

        public virtual string Serialize(object obj, out string startTime)
        {
            startTime = null;
            return Converter.ToXml(obj);
        }

        public abstract bool Equals(TextFormat other);

        public abstract Type GetValueType();
    }
}