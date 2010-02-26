using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace SDMX
{
    public class Code : IdentifiableArtefact
    {
        public Code(ID id)
            : base(id)
        { }

        public CodeList CodeList { get; internal set; }
        public Code Parent { get; set; }
       
        public override Uri Urn
        {
            get 
            {
                return new Uri(string.Format("{0}.codelist.Code={1}:{2}.{3}[{4}]".F(UrnPrefix, CodeList.AgencyID, CodeList.ID, ID, CodeList.Version)));
            }
        }     

        //#region IEquatable<Code> Members

        //public bool Equals(Code other)
        //{
        //    return CodeList.Equals(other.CodeList)
        //        && ID.Equals(other.ID);
        //}

        //public override bool Equals(object obj)
        //{
        //    if (!(obj is Code)) return false;
        //    return Equals((Code)obj);
        //}

        //public override int GetHashCode()
        //{
        //    return 37
        //        ^ ID.GetHashCode()
        //        ^ CodeList.GetHashCode();
        //}

        //public override string ToString()
        //{
        //    //return string.Format("{0}.{1}", CodeList, ID);
        //    return ID.ToString();
        //}

        //#endregion
    }
}
