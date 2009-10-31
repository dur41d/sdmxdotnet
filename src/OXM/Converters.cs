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
            return value.ToString();
        }

        public T ToObj(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }
    }

    public class DateTimeConverter : ISimpleTypeConverter<DateTime>
    {
        public string ToXml(DateTime value)
        {
            return value.ToString("s");
        }

        public DateTime ToObj(string value)
        {
            return XmlConvert.ToDateTime(value, XmlDateTimeSerializationMode.RoundtripKind);
        }
    }
}
