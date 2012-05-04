//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace SDMX
//{
//    public class ValuePair : IEquatable<ValuePair>
//    {
//        public string Value { get; private set; }
//        public string StartTime { get; private set; }

//        public bool HasStartTime
//        {
//            get
//            {
//                return !string.IsNullOrEmpty(StartTime);
//            }
//        }
        
//        public ValuePair(string value, string startTime)
//        {
//            Value = value;
//            StartTime = startTime;
//        }

//        public override int GetHashCode()
//        {
//            return 37 ^ Value.GetHashCode() ^ StartTime.GetHashCode();
//        }

//        public override bool Equals(object obj)
//        {
//            return Equals(obj as ValuePair);
//        }

//        public bool Equals(ValuePair other)
//        {
//            if (other == null)
//                return false;

//            return Value == other.Value && StartTime == other.StartTime;
//        }
//    }
//}
