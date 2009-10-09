using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public interface Item
    {
        Item Parent { get; set; }
        IItemScheme ItemScheme { get; set; }
        string Key { get; }
    }     
}
