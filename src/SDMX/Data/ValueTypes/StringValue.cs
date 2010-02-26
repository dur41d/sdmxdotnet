using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace SDMX
{
    public class StringValue : Value, IEquatable<StringValue>
    {
        string _value;

        public StringValue(string value)
        {
            _value = value;
        }

        public static explicit operator StringValue(string value)
        {
            return new StringValue(value);
        }

        public static explicit operator string(StringValue value)
        {
            return value._value;
        }

        public override string ToString()
        {
            return _value;
        }

        #region IEquatable<StringValue> Members

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public override bool Equals(object other)
        {
            if (!(other is StringValue)) return false;
            return Equals((StringValue)other);
        }

        public bool Equals(StringValue other)
        {
            return _value.Equals(other._value);
        }

        public static bool operator ==(Value x, StringValue y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(Value x, StringValue y)
        {
            return !x.Equals(y);
        }

        #endregion
    }
}
