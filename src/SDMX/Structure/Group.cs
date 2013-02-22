using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace SDMX
{  
    public class Group : AnnotableArtefact, Item
    {
        private Dictionary<Id, Dimension> dimensions = new Dictionary<Id, Dimension>();
        
        public Id Id { get; set; }                
        public Id AttachmentConstraintRef { get; set; }
        public InternationalText Description { get; private set; }
        public KeyFamily KeyFamily { get; private set; }

        public IEnumerable<Dimension> Dimensions
        {
            get
            {
                return dimensions.Values.AsEnumerable();
            }
        }
        
        internal Group(Id id, KeyFamily keyFamily)
        {            
            Contract.AssertNotNull(id, "id");
            Contract.AssertNotNull(keyFamily, "keyFamily");

            Id = id;
            KeyFamily = keyFamily;
            Description = new InternationalText();
        }

        public void AddDimension(Id conceptId)
        {
            Contract.AssertNotNull(conceptId, "conceptId");

            var dimension = KeyFamily.Dimensions.Find(conceptId);
            dimensions.Add(conceptId, dimension);
        }

        public void RemoveDimension(Id conceptId)
        {
            dimensions.Remove(conceptId);
        }
    }
}
