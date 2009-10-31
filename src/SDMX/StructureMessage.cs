using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public StructureMessage()
        {
            CodeLists = new List<CodeList>();
            Concepts = new List<Concept>();
            KeyFamilies = new List<KeyFamily>();
        }
    }
}
