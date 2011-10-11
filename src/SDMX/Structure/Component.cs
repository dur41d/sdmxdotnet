using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace SDMX
{
    public abstract class Component : AnnotableArtefact
    {
        public Concept Concept { get; set; }
        public CodeList CodeList { get; set; }
        public abstract TextFormat DefaultTextFormat { get; }
       
        public TextFormat TextFormat
        {
            get { return TextFormatImpl; }
            set { TextFormatImpl = value; }
        }

        // only used to fake Covariance with TimeDimension        
        // http://peisker.net/dotnet/covariance.htm
        protected virtual TextFormat TextFormatImpl { get; set; }
        

        public int Order { get; set; }

        public Component(Concept concept)
            : this(concept, null)
        { }

        public Component(Concept concept, CodeList codeList)
        {
            this.Concept = concept;
            this.CodeList = codeList;
            this.TextFormat = DefaultTextFormat;
        }

        public bool IsCoded
        {
            get
            {
                return CodeList != null;
            }
        }

        public override string ToString()
        {
            return Concept.Id.ToString();
        }

        public virtual object Parse(string value)
        {
            return Parse(value, null);
        }

        public virtual object Parse(string s, string startTime)
        {
            if (IsCoded)
            {
                var code = CodeList.Get(s);
                if (code == null)
                {
                    throw new SDMXException("Cannot parse s='{0}' startTime='{1}' for Component id '{2}'.", s, startTime, Concept.Id);
                }
                return code.Id.ToString();
            }
            else
            {
                return TextFormat.Parse(s, startTime);
            }
        }

        public virtual bool IsValid(object value)
        {
            if (IsCoded)
            {
                string id = value as string;
                return id != null && CodeList.Contains(id);
            }
            else
            {
                return TextFormat.IsValid(value);
            }
        }

        internal void Validate(Value value)
        {
            if (!IsValid(value))
            {
                throw new SDMXException("Invalid value '{0}' for component '{1}'."
                            , value, Concept.Id);
            }
        }
    }
}
