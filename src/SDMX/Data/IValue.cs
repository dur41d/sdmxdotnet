using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace SDMX
{
    public interface IValue
    {        
    }

    public class StringValue : IValue,  IEquatable<StringValue>
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

        public static bool operator ==(IValue x, StringValue y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(IValue x, StringValue y)
        {
            return !x.Equals(y);
        }

        #endregion
    }

    public class DecimalValue : IValue, IEquatable<DecimalValue>
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

        public static bool operator ==(IValue x, DecimalValue y)
        {
            if (x == null) return y == null;
            return x.Equals(y);
        }

        public static bool operator !=(IValue x, DecimalValue y)
        {
            if (x == null) return y != null;
            return !x.Equals(y);
        }

        #endregion
    }

    public class IntegerValue : IValue, IEquatable<IntegerValue>
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
            if (!(other is IntegerValue)) return false;
            return Equals((IntegerValue)other);
        }

        public bool Equals(IntegerValue other)
        {
            return _value.Equals(other._value);
        }

        public static bool operator ==(IValue x, IntegerValue y)
        {
            if (x == null) return y == null;
            return x.Equals(y);
        }

        public static bool operator !=(IValue x, IntegerValue y)
        {
            if (x == null) return y != null;
            return !x.Equals(y);
        }

        #endregion
    }
}
