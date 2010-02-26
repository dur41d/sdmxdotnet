using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace SDMX
{
    public class CodeValue : Value
    {
        ID _value;
        static Dictionary<ID, CodeValue> codes = new Dictionary<ID, CodeValue>();

        private CodeValue(ID value)
        {
            _value = value;
        }

        public static CodeValue Create(ID id)
        {
            CodeValue result = null;
            if (!codes.TryGetValue(id, out result))
            {
                result = new CodeValue(id);
                codes.Add(id, result);
            }
            return result;
        }

        public static explicit operator CodeValue(string s)
        {
            return CodeValue.Create(s);
        }

        public static explicit operator string(CodeValue value)
        {
            return value.ToString();
        }

        public static explicit operator CodeValue(ID id)
        {
            return CodeValue.Create(id);
        }

        public static explicit operator ID(CodeValue value)
        {
            return value._value;
        }

        public static explicit operator CodeValue(Code code)
        {
            return CodeValue.Create(code.ID);
        }

        public override string ToString()
        {
            return _value.ToString();
        }
    }
}
