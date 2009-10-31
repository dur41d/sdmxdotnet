using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public class CodeList : ItemScheme<Code>
    {
        public bool IsExternalReference { get; set; }
        
        public CodeList(InternationalString name, ID id, ID agencyID)
            : base(id, agencyID)
        {
            Name.Add(name);
        }
        
        public override Uri Urn
        {
            get 
            {
                return new Uri(string.Format("{0}.codelist={1}:{2}[{3}]".F(UrnPrefix, AgencyID, ID, Version)));
            }
        }

        public Code this[ID codeID]
        {
            get
            {
                return items[codeID];
            }
        }
    }
}
