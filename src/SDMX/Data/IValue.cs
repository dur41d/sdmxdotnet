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

    public class StringValue : IValue
    {
        string _value;

        public StringValue(string value)
        {
            Contract.AssertNotNullOrEmpty(() => value);

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
    }

    public class DecimalValue : IValue
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
    }


    //public struct Value
    //{
    //    ValueType _valueType;
    //    object _referenceType;
        
    //    public Value(Code code)
    //    {
    //        _referenceType = null;
    //        _valueType = code.ID;
    //    }

    //    public Value(string value)
    //    {
    //        _referenceType = value;
    //        _valueType = default(ValueType);
    //    }

    //    public Value(double value)
    //    {
    //        _referenceType = null;
    //        _valueType = value;
    //    }     

    //    public static explicit operator Value(Code code)
    //    {
    //        return new Value(code);
    //    }

    //    public static explicit operator Value(string value)
    //    {
    //        return new Value(value);
    //    }

    //    public static explicit operator Value(double value)
    //    {
    //        return new Value(value);
    //    }

    //    public static explicit operator ID(Value value)
    //    {
    //        return (ID)value._valueType;
    //    }

    //    public static explicit operator string(Value value)
    //    {
    //        return (string)value._referenceType;
    //    }

    //    public static explicit operator double(Value value)
    //    {
    //        return (double)value._valueType;
    //    }

    //    public override string ToString()
    //    {            
    //        return null;
    //    }

    //    public bool Is<T>()
    //    {
    //        if (typeof(T) == typeof(string) && _referenceType is string)
    //        {
    //            return true;
    //        }
    //        else if (typeof(T) == typeof(double) && _valueType is double)
    //        {
    //            return true;
    //        }
    //        else if (typeof(T) == typeof(Code) && _valueType is ID)
    //        {
    //            return true;
    //        }
    //        else if (typeof(T) == typeof(ID) && _valueType is ID)
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }
    //}
    
    //public interface IValue
    //{
    //    string ToString();
    //    IValue Parse(string value);
    //}

    //public class StringValue : IValue
    //{
    //    string _value;

    //    private StringValue(string value)
    //    {
    //        Contract.AssertNotNullOrEmpty(() => value);            
    //        _value = value;
    //    }

    //    public static implicit operator StringValue(string value)
    //    {
    //        return new StringValue(value);
    //    }

    //    public static implicit operator string(StringValue value)
    //    {
    //        if (value == null)
    //        {
    //            return null;
    //        }
    //        return value.ToString();
    //    }

    //    #region IValue Members

    //    public override string ToString()
    //    {
    //        return _value;
    //    }

    //    public IValue Parse(string value)
    //    {
    //        return new StringValue(value);
    //    }

    //    #endregion

    //    public static bool operator ==(StringValue x, StringValue y)
    //    {
    //        return Equals(x, y);
    //    }

    //    public static bool operator !=(StringValue x, StringValue y)
    //    {
    //        return !Equals(x, y);
    //    }

    //    public override bool Equals(object other)
    //    {
    //        return Equals(other as StringValue);
    //    }

    //    public bool Equals(StringValue other)
    //    {
    //        return _value.Equals(other._value);
    //    }    
    //}

    //public class DoubleValue : IValue
    //{ 
    //     double _value;

    //     private DoubleValue(double value)
    //    {            
    //        _value = value;
    //    }

    //     public static implicit operator DoubleValue(double value)
    //    {
    //        return new DoubleValue(value);
    //    }

    //    public static implicit operator double(DoubleValue value)
    //    {
    //        Contract.AssertNotNull(() => value);    
    //        return value._value;
    //    }

    //    #region IValue Members

    //    public override string ToString()
    //    {
    //        return _value.ToString();
    //    }

    //    public IValue Parse(string value)
    //    {
    //        return new DoubleValue(double.Parse(value));
    //    }

    //    #endregion

    //    public static bool operator ==(DoubleValue x, DoubleValue y)
    //    {
    //        return Equals(x, y);
    //    }

    //    public static bool operator !=(DoubleValue x, DoubleValue y)
    //    {
    //        return !Equals(x, y);
    //    }

    //    public override bool Equals(object other)
    //    {
    //        return Equals(other as DoubleValue);
    //    }

    //    public bool Equals(DoubleValue other)
    //    {
    //        return _value.Equals(other._value);
    //    }    
    //}
}
