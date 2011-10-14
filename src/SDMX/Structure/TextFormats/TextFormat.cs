using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Common;
using SDMX.Parsers;

namespace SDMX
{
    public abstract class TextFormat : IEquatable<TextFormat>
    {
        public abstract bool IsValid(object value);

        internal abstract IValueConverter Converter { get; }

        public virtual object Parse(string s, string startTime)
        {
            return Converter.Parse(s, startTime);
        }

        public virtual string Serialize(object obj, out string startTime)
        { 
            return Converter.Serialize(obj, out startTime);
        }

        public abstract bool Equals(TextFormat other);

        public abstract Type GetValueType();
    }
}