using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public abstract class Component : AnnotableArtefact
    {
        public Concept Concept { get; set; }
        public CodeList CodeList { get; set; }
        public ITextFormat TextFormat { get; set; }
        public CrossSectionalAttachmentLevel CrossSectionalAttachmentLevel { get; set; }

        public int Order { get; set; }

        public Component(Concept concept)
        {
            this.Concept = concept;
        }

        public Component(Concept concept, CodeList codeList)
        {
            this.Concept = concept;
            this.CodeList = codeList;
        }

        public bool IsCoded
        {
            get
            {
                return CodeList != null;
            }
        }

        //public virtual object Parse(string value)
        //{
        //    return Parse(value, null);    
        //}

        //public virtual object Parse(string value, string startTime)
        //{
        //    if (IsCoded)
        //    {
        //        return CodeList.Get((ID)value);
        //    }
        //    else
        //    {
        //        throw new Exception("non coded components are not supported yet.");
        //    }
        //}

        public virtual IValue Parse(string value)
        {
            return Parse(value, null);
        }

        public virtual IValue Parse(string value, string startTime)
        {
            if (IsCoded)
            {
                return (IValue)CodeList.Get((ID)value);
            }
            else
            {
                return TextFormat.Parse(value, startTime);
            }
        }

        public virtual void Serialize(IValue value, out string stringValue, out string startTime)
        {
            if (IsCoded)
            {
                stringValue = null; //((ID)value).ToString();
                startTime = null;
            }
            else
            {   
               TextFormat.Serialize(value, out stringValue, out startTime);               
            }
        }

        public virtual bool IsValid(IValue value)
        {
            if (IsCoded)
            {
                return false;
                // return value.Is<Code>() && CodeList.Contains((ID)value);
            }
            else
            {
                return TextFormat.IsValid(value);
            }
        }
    }
}
