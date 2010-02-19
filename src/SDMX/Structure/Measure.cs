using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
//    public abstract class Measure : AnnotableArtefact
//    {
//        public Concept Concept { get; set; }
//        public CodeList CodeList { get; set; }
//        public ITextFormat TextFormat { get; set; }

//        public Measure(Concept concept)
//        {
//            this.Concept = concept;
//        }

//        public Measure(Concept concept, CodeList codeList)
//        {
//            this.Concept = concept;
//            this.CodeList = codeList;
//        }

//        public bool IsCoded
//        {
//            get
//            {
//                return CodeList != null;
//            }
//        }

//        //public virtual Value Parse(string value)
//        //{
//        //    return Parse(value, null);
//        //}

//        //public virtual Value Parse(string value, string startTime)
//        //{
//        //    if (IsCoded)
//        //    {
//        //        return (Value)CodeList.Get((ID)value);
//        //    }
//        //    else
//        //    {
//        //        if (TextFormat == null)
//        //        {
//        //            return (Value)value;
//        //        }
//        //        else
//        //        {
//        //            return TextFormat.Parse(value, startTime);
//        //        }
//        //    }
//        //}

//        //public virtual void Serialize(Value value, out string stringValue, out string startTime)
//        //{
//        //    if (IsCoded)
//        //    {                
//        //        stringValue = ((ID)value).ToString();
//        //        startTime = null;
//        //    }
//        //    else
//        //    {
//        //        if (TextFormat == null)
//        //        {
//        //            stringValue = (string)value;
//        //            startTime = null;
//        //        }
//        //        else
//        //        {
//        //            TextFormat.Serialize(value, out stringValue, out startTime);
//        //        }
//        //    }
//        //}

//        public virtual bool IsValid(IValue value)
//        {
//            if (IsCoded)
//            {
//                return false;
//                //return value.Is<Code>() && CodeList.Contains((ID)value);
//            }
//            else
//            {  
//                 return TextFormat.IsValid(value);
               
//            }
//        }

//        internal Type GetValueType()
//        {
//            throw new NotImplementedException();
//        }
//    }
}
