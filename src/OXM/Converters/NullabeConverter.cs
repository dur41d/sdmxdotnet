using System;
using System.Collections.Generic;
using System.Linq;

namespace OXM
{
    public abstract class NullabeConverter<T> : SimpleTypeConverter<Nullable<T>> where T : struct
    {
        protected abstract SimpleTypeConverter<T> Converter { get; }

        public override string ToXml(Nullable<T> value)
        {
            if (!value.HasValue)
                return null;

            return Converter.ToXml(value.Value);
        }

        public override Nullable<T> ToObj(string value)
        {
            if (value == null)
                return null;

            return Converter.ToObj(value);
        }

        public override bool CanConvertToObj(string s)
        {
            return Converter.CanConvertToObj(s);
        }
    }
}
