using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Collections;

namespace SDMX
{
    public struct IDValuePair
    {
        public ID ID { get; private set; }
        public Value Value { get; private set; }

        internal IDValuePair(ID id, Value value)
            : this()
        {
            ID = id;
            Value = value;
        }
    }
}