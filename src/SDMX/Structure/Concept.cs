using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public class Concept : MaintainableArtefact, Item
    {        
        public Concept(InternationalString name, ID id, ID agencyID)
            : base(id, agencyID)
        {
            Name.Add(name);
        }

        public ConceptScheme ConceptScheme { get; private set; }
        public Concept Parent { get; set; }
        public TextFormat TextFormat { get; set; }
        public bool IsExternalReference { get; set; }

        public override Uri Urn
        {
            get
            {
                return new Uri(string.Format("{0}.conceptScheme.Concept={1}:{2}.{3}[{4}]".F(UrnPrefix, ConceptScheme.AgencyID, ConceptScheme.ID, ID, ConceptScheme.Version)));
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
                Parent = (Concept)value;
            }
        }

        IItemScheme Item.ItemScheme
        {
            get
            {
                return ConceptScheme;
            }
            set
            {
                ConceptScheme = (ConceptScheme)value;
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
