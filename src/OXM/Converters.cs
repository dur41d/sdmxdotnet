using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace OXM
{
    public class StringConverter : ISimpleTypeConverter<string>
    {
        public string ToXml(string value)
        {
            return value;
        }

        public string ToObj(string value)
        {
            return value;
        }
    }

    public class BooleanConverter : ISimpleTypeConverter<bool>
    {
        public string ToXml(bool value)
        {
            return XmlConvert.ToString(value);
        }

        public bool ToObj(string value)
        {
            return XmlConvert.ToBoolean(value);
        }
    }

    public class NullableBooleanConverter : ISimpleTypeConverter<bool?>
    {
        BooleanConverter converter = new BooleanConverter();
        
        public string ToXml(bool? value)
        {
            if (!value.HasValue)
                return null;

            return converter.ToXml(value.Value);
        }

        public bool? ToObj(string value)
        {
            if (value == null)
                return null;

            return converter.ToObj(value);
        }
    }


    public class Int32Converter : ISimpleTypeConverter<int>
    {
        public string ToXml(int value)
        {
            return XmlConvert.ToString(value);
        }

        public int ToObj(string value)
        {
            return XmlConvert.ToInt32(value);
        }
    }

    public class UriConverter : ISimpleTypeConverter<Uri>
    {
        public string ToXml(Uri value)
        {
            return value == null ? null : value.ToString();
        }

        public Uri ToObj(string value)
        {
            return new Uri(value);
        }
    }

    public class EnumConverter<T> : ISimpleTypeConverter<T>
    {
        public string ToXml(T value)
        {
            if ((object)value == null)
                return null;

            return value.ToString();
        }

        public T ToObj(string value)        
        {
            Type enumType = typeof(T);
            if (enumType.IsGenericType && enumType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                var nonNullableType = Nullable.GetUnderlyingType(enumType);
                return (T)Enum.Parse(nonNullableType, value);
            }
            else
            {
                return (T)Enum.Parse(typeof(T), value);
            }
        }
    }

    public class DateTimeConverter : ISimpleTypeConverter<DateTimeOffset>
    {
        public string ToXml(DateTimeOffset value)
        {
            return value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK");
        }

        public DateTimeOffset ToObj(string value)
        {
            return XmlConvert.ToDateTimeOffset(value);
        }
    }

    public class NullableDateTimeConverter : ISimpleTypeConverter<DateTimeOffset?>
    {
        public string ToXml(DateTimeOffset? value)
        {
            if (!value.HasValue)
                return null;

            return value.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK");
        }

        public DateTimeOffset? ToObj(string value)
        {
            if (value == null)
                return null;

            return XmlConvert.ToDateTime(value, XmlDateTimeSerializationMode.RoundtripKind);
        }
    }


 
}
