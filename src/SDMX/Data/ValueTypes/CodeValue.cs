//ausing System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Common;

//namespace SDMX
//{
//    public class CodeValue : Value
//    {
//        Id _value;
//        static Dictionary<Id, CodeValue> codes = new Dictionary<Id, CodeValue>();

//        private CodeValue(Id value)
//        {
//            _value = value;
//        }

//        public static CodeValue Create(Id id)
//        {
//            CodeValue result = null;
//            if (!codes.TryGetValue(id, out result))
//            {
//                result = new CodeValue(id);
//                codes.Add(id, result);
//            }
//            return result;
//        }

//        public static explicit operator CodeValue(string s)
//        {
//            return CodeValue.Create(s);
//        }

//        public static explicit operator string(CodeValue value)
//        {
//            return value.ToString();
//        }

//        public static explicit operator CodeValue(Id id)
//        {
//            return CodeValue.Create(id);
//        }

//        public static explicit operator Id(CodeValue value)
//        {
//            return value._value;
//        }

//        public static explicit operator CodeValue(Code code)
//        {
//            return CodeValue.Create(code.Id);
//        }

//        public override string ToString()
//        {
//            return _value.ToString();
//        }
//    }
//}
