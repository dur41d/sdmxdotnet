using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace SDMX
{
    public abstract class Message
    { 
        
    }
    
    public class StructureMessage : Message
    {
        public Header Header { get; set; }
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

        //private CodeList GCL(ID codeListID, ID agencyID, string version)
        //{
        //    Contract.AssertNotNull(() => codeListID);
            
        //    var codeLists = CodeLists.Where(c => c.ID == codeListID && c.AgencyID == agencyID);

        //    int count = codeLists.Count();
        //    if (count == 0)
        //    {
        //        return null;
        //    }
        //    if (count == 1)
        //    {
        //        return codeLists.Single();
        //    }
        //    else
        //    {
        //        if (version == null)
        //        {
        //            throw new SDMXException("Multipe codelists found and version is null.");    
        //        }

        //        codeLists = codeLists.Where(c => c.Version == version);

        //        count = codeLists.Count();
        //        if (count == 0)
        //        {
        //            return null;  
        //        }
        //        else if (count == 1)
        //        {
        //            return codeLists.Single();
        //        }
        //        else
        //        {
        //            throw new SDMXException("Multiple code lists found with the same version.");
        //        }
        //    }
        //}

        public Concept GetConcept(ID conceptID, ID agencyID, string version)
        {
            return Filter(Concepts, c => c.ID == conceptID, c => c.AgencyID == agencyID, c => c.Version == version);
        }


    }
}
