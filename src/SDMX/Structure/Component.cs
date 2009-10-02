using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public abstract class Component
    {
        public Concept Concept { get; private set; }
        public CodeList CodeList { get; private set; }
        public TextFormat TextFormat { get; private set; }

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

        public virtual object GetValue(string value)
        {
            return GetValue(value, null);    
        }

        public virtual object GetValue(string value, string startTime)
        {
            if (IsCoded)
            {
                return CodeList[value];
            }
            else
            {
                throw new Exception("non coded components are not supported yet.");
            }
        }
    }
}
