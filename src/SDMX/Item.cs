using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public abstract class Item : VersionableArtefact
    {
        public Item Parent { get; protected set; }
        internal IItemScheme ItemScheme { get; set; }

        internal abstract string Key { get; }
    }
}
