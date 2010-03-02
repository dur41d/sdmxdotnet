using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace SDMX
{  
    public class GroupDescriptor : AnnotableArtefact, Item
    {
        private Dictionary<ID, Dimension> dimensions = new Dictionary<ID, Dimension>();
        
        public ID ID { get; set; }                
        public ID AttachmentConstraintRef { get; set; }
        public InternationalText Description { get; private set; }
        public KeyFamily KeyFamily { get; private set; }

        public IEnumerable<Dimension> Dimensions
        {
            get
            {
                return dimensions.Values.AsEnumerable();
            }
        }
        
        internal GroupDescriptor(ID id, KeyFamily keyFamily)
        {            
            Contract.AssertNotNull(id, "id");
            Contract.AssertNotNull(keyFamily, "keyFamily");

            ID = id;
            KeyFamily = keyFamily;
            Description = new InternationalText();
        }

        public void AddDimension(ID conceptID)
        {
            Contract.AssertNotNull(conceptID, "conceptID");

            var dimension = KeyFamily.Dimensions.TryGet(conceptID);
            dimensions.Add(conceptID, dimension);
        }

        public void RemoveDimension(ID conceptID)
        {
            dimensions.Remove(conceptID);
        }
    }
}
