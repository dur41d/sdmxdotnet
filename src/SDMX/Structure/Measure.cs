using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public abstract class Measure : AnnotableArtefact
    {
        public Concept Concept { get; set; }
        public CodeList CodeList { get; set; }
        public TextFormat TextFormat { get; set; }

        public Measure(Concept concept)
        {
            this.Concept = concept;
        }

        public Measure(Concept concept, CodeList codeList)
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
