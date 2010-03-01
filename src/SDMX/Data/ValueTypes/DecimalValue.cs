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
            return Equals(other as DecimalValue);
        }

        public bool Equals(DecimalValue other)
        {
            return this.Equals(other, () => _value.Equals(other._value));
        }

        public static bool operator ==(Value x, DecimalValue y)
        {
            return object.Equals(x, y);
        }

        public static bool operator !=(Value x, DecimalValue y)
        {            
            return !object.Equals(x, y);
        }

        #endregion
    }
}
