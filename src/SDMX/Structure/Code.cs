using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public class Code : IdentifiableArtefact, Item
    {
        public Code(ID id)
            : base(id)
        { }

        public CodeList CodeList { get; private set; }
        public Code Parent { get; set; }
       
        public override Uri Urn
        {
            get 
            {
                return new Uri(string.Format("{0}.codelist.Code={1}:{2}.{3}[{4}]".F(UrnPrefix, CodeList.AgencyID, CodeList.ID, ID, CodeList.Version)));
            }
        }     

        #region Item Members

        Item Item.Parent 
        {
            get
            {
                return Parent;
            }
            set
            {
                Parent = (Code)value;
            }
        }
     
        IItemScheme Item.ItemScheme
        {
            get
            {
                return CodeList;
            }
            set
            {
                CodeList = (CodeList)value;
            }
        }

        string Item.Key
        {
            get 
            { 
               return ID; 
            }
        }

        #endregion
    }
}
