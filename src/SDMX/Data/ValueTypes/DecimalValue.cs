using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace SDMX
{
    public class DecimalValue : Value, IEquatable<DecimalValue>
    {
        decimal _value;

        public DecimalValue(decimal value)
        {
            _value = value;
        }

        public static explicit operator DecimalValue(decimal value)
        {
            return new DecimalValue(value);
        }

        public static explicit operator decimal(DecimalValue value)
        {
            return value._value;
        }

        public override string ToString()
        {
            return _value.ToString();
        }

        #region IEquatable<DecimalValue> Members

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public override bool Equals(object other)
        {
            if (!(other is DecimalValue)) return false;
            return Equals((DecimalValue)other);
        }

        public bool Equals(DecimalValue other)
        {
            return _value.Equals(other._value);
        }

        public static bool operator ==(Value x, DecimalValue y)
        {
            if (x == null) return y == null;
            return x.Equals(y);
        }

        public static bool operator !=(Value x, DecimalValue y)
        {
            if (x == null) return y != null;
            return !x.Equals(y);
        }

        #endregion
    }
}
