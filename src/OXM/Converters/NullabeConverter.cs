using System;
using System.Collections.Generic;
using System.Linq;

namespace OXM
{
    public abstract class NullabeConverter<T> : SimpleTypeConverter<Nullable<T>> where T : struct
    {
        protected abstract SimpleTypeConverter<T> Converter { get; }

        public override bool TrySerialize(Nullable<T> value, out string s)
        {
            if (!value.HasValue)
            {
                s = null;
                return true;
            }

            return Converter.TrySerialize(value.Value, out s);
        }

        public override bool TryParse(string s, out Nullable<T> obj)
        {
            if (s == null)
            {
                obj = null;
                return true;
            }

            T result = default(T);
            if (Converter.TryParse(s, out result))
            {
                obj = result;
                return true;
            }
            else
            {
                obj = null;
                return false;
            }
        }
    }
}
