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
        public IList<KeyFamily> KeyFamilies { get; private set; }

        public StructureMessage()
        {
            KeyFamilies = new List<KeyFamily>();
        }
    }
}
