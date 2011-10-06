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
        public abstract ITextFormat DefaultTextFormat { get; }
       
        public ITextFormat TextFormat
        {
            get { return TextFormatImpl; }
            set { TextFormatImpl = value; }
        }

        // only used to fake Covariance with TimeDimension        
        // http://peisker.net/dotnet/covariance.htm
        protected virtual ITextFormat TextFormatImpl { get; set; }
        

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

        //public virtual object Parse(string value)
        //{
        //    return Parse(value, null);    
        //}

        //public virtual object Parse(string value, string startTime)
        //{
        //    if (IsCoded)
        //    {
        //        return CodeList.Get((Id)value);
        //    }
        //    else
        //    {
        //        throw new Exception("non coded components are not supported yet.");
        //    }
        //}       

        //public virtual bool TryParse(string s, string startTime, out IValue value, out string reason)
        //{
        //    value = null;
        //    reason = null;
        //    if (IsCoded)
        //    {
        //        var code = CodeList.Get((Id)s);
        //        if (code == null)
        //        {
        //            reason = string.Format("Code not found for value '{0}'.", s);
        //            return false;
        //        }
        //        value = code;
        //        return true;
        //    }
        //    else
        //    {
        //        return TextFormat.TryParse(s, startTime, out value, out reason);
        //    }
        //}

        //public virtual void Serialize(IValue value, out string s, out string startTime)
        //{
        //    if (IsCoded)
        //    {
        //        s = null; //((Id)value).ToString();
        //        startTime = null;
        //    }
        //    else
        //    {   
        //       TextFormat.Serialize(value, out s, out startTime);               
        //    }
        //}

        public virtual bool IsValid(Value value)
        {
            if (IsCoded)
            {
                return value is CodeValue && CodeList.Contains((CodeValue)value);
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

        internal Type GetValueType()
        {
            if (IsCoded)
            {
                return typeof(CodeValue);
            }
            else
            {
                return TextFormat.GetValueType();
            }
        }
    }
}
