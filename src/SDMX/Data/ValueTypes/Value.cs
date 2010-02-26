using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace SDMX
{
    public abstract class Value
    {
        public static bool operator ==(Value x, object y)
        {
            return object.Equals(x, y);
        }

        public static bool operator !=(Value x, object y)
        {
            return !object.Equals(x, y);
        }
       
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
}