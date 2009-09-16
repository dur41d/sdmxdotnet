using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX.Tests
{
    public class CodeList
    {
        public string Id { get; private set; }
        private Dictionary<string, Code> codes = new Dictionary<string, Code>();

        public CodeList(string id)
        {
            Id = id;
        }

        public void AddCode(Code code)
        {
            codes.Add(code.Value, code);
        }

        public IList<Code> Codes
        {
            get
            {
                return codes.Values.ToList();
            }
        }

        public Code this[string value]
        {
            get
            {
                Code result = null;
                if (!codes.TryGetValue(value, out result))
                {
                    throw new Exception("Code does not exsist with this value");
                }
                return result;
            }
        }
    }
}
