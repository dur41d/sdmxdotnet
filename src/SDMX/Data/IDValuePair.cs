using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Collections;

namespace SDMX
{
    public struct IdValuePair
    {
        public Id Id { get; private set; }
        public Value Value { get; private set; }

        internal IdValuePair(Id id, Value value)
            : this()
        {
            Id = id;
            Value = value;
        }
    }
}