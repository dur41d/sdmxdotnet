using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public class Code : Item
    {
        public Code(ID id)
            : base(id)
        { }
        
        public CodeList CodeList 
        {
            get 
            { 
                return (CodeList)ItemScheme; 
            }
        }
       
        public override Uri Urn
        {
            get 
            {
                return new Uri(string.Format("{0}.codelist.Code={1}:{2}.{3}[{4}]".F(UrnPrefix, CodeList.AgencyID, CodeList.ID, ID, CodeList.Version)));
            }
        }

        internal override string Key
        {
            get 
            { 
                return ID; 
            }
        }
    }
}
