using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace SDMX.Tests
{

    [TestFixture]
    public class IdTests
    {
        [Test]
        public void Not_null()
        {
            Assert.Throws<ArgumentNullException>(() => 
            {
                string s = null;
                Id id = s; 
            });
        }

        [Test]
        public void Not_empty_string()
        {
            Assert.Throws<SDMXException>(() => { Id id = "   "; });
        }
        
        [Test]
        public void Test_equlity_with_null()
        {
            Id id1 = "D_S";

            Assert.IsFalse(id1 == null);
            Assert.IsTrue(id1 != null);
        }

        [Test]
        public void Test_equlity()
        {
            Id id1 = "D_S";
            Id id2 = "D_S";

            Assert.IsTrue(id1 == id2);
        }

        [Test]
        public void Test_casting_from_object()
        {
            string s = "Id";
            object obj = s;
            string s2 = obj as string;
            Id id = s2;

            Assert.IsNotNull(id);
            Assert.AreEqual("Id", id.ToString());
        }

        [Test]
        public void Test_casting_from_object2()
        {
            var dt = DateTime.Now;
            object obj = dt;
            t2? dt2 = obj as t2?;


            Assert.IsNotNull(dt2);
            Assert.AreEqual(dt, (DateTime)dt2.Value);
        }


    }

    public struct t2 : IConvertible
    {
        private DateTime _dateTime;



        public t2(DateTime dateTime)
        {
            _dateTime = dateTime;
        }

        public static implicit operator t2(DateTime value)
        {
            return new t2(value);
        }

        public static implicit operator DateTime(t2 value)
        {
            return value._dateTime;
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.DateTime;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            return _dateTime;
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }
    }
}
