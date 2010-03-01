using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace SDMX
{
    public class IntegerValue : Value, IEquatable<IntegerValue>
    {
        int _value;

        public IntegerValue(int value)
        {
            _value = value;
        }

        public static explicit operator IntegerValue(int value)
        {
            return new IntegerValue(value);
        }

        public static explicit operator int(IntegerValue value)
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
            return Equals(other as IntegerValue);
        }

        public bool Equals(IntegerValue other)
        {
            return this.Equals(other, () => _value.Equals(other._value));
        }

        public static bool operator ==(Value x, IntegerValue y)
        {
            return object.Equals(x, y);
        }

        public static bool operator !=(Value x, IntegerValue y)
        {
            return !object.Equals(x, y);
        }

        #endregion
    }
}
