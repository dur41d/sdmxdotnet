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

        public virtual bool TryParse(string s, string startTime, out object obj)
        {
            if (IsCoded)
            {
                var code = CodeList.Get(s);
                if (code == null)
                {
                    obj = null;
                    return false;
                }
                obj = code.Id.ToString();
                return true;
            }
            else
            {
                return TextFormat.TryParse(s, startTime, out obj);
            }
        }

        public bool TrySerialize(object obj, out string s, out string startTime)
        {
            if (IsCoded)
            {
                var code = CodeList.Get(obj.ToString());
                if (code == null)
                {
                    s = startTime = null;
                    return false;
                }

                s = code.Id;
                startTime = null;
                return true;
            }
            else
            {
                return TextFormat.TrySerialize(obj, out s, out startTime);
            }
        }

        public virtual bool CanSerialize(object obj)
        {
            string s, startTime = null;
            return TrySerialize(obj, out s, out startTime);
        }

        public Type GetValueType()
        {
            if (IsCoded)
                return typeof(string);
            else
                return TextFormat.GetValueType();
        }
    }
}
