using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using SDMX.Parsers;
using System.Xml;
using System.IO;

namespace SDMX
{ 
    public class StructureMessage : MessageBase<StructureMessage>
    {
        public IList<CodeList> CodeLists { get; private set; }
        public IList<Concept> Concepts { get; private set; }
        public IList<KeyFamily> KeyFamilies { get; private set; }
        public IList<HierarchicalCodeList> HierarchicalCodeLists { get; private set; }

        public StructureMessage()
        {
            CodeLists = new List<CodeList>();
            Concepts = new List<Concept>();
            KeyFamilies = new List<KeyFamily>();
            HierarchicalCodeLists = new List<HierarchicalCodeList>();
        }


        private T Filter<T>(IEnumerable<T> list, params Func<T, bool>[] predicates)
        {
            IEnumerable<T> filtered = list;
            foreach (var perdicate in predicates)
            {
                filtered = filtered.Where(perdicate);
                int count = filtered.Count();

                if (count == 0)
                {
                    throw new SDMXException("not found.");
                }
                else if (count == 1)
                {
                    return filtered.Single();
                }                
            }

            throw new SDMXException("Multiple found for the cirteria.");
        }

        public CodeList GetCodeList(ID codeListID, ID agencyID, string version)
        {
            return Filter(CodeLists, c => c.ID == codeListID, c => c.AgencyID == agencyID, c => c.Version == version);
        }

      
        
        public Concept GetConcept(ID conceptID, ID agencyID, string version)
        {
            return Filter(Concepts, c => c.ID == conceptID, c => c.AgencyID == agencyID, c => c.Version == version);
        }

        protected override StructureMessage GetThis()
        {
            return this;
        }
    }
}
