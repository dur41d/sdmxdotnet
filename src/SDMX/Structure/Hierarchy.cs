using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace SDMX
{
    public class Hierarchy : VersionableArtefact
    {
        public HierarchicalCodeList HierarchicalCodeList { get; internal set; }

        public bool IsFinal { get; set; }
        
        public CodeRef Root {get; set; }
        
        // TODO: Add support to Levels. I don't really understand them yet
        // public IList<Level> Levels { get; private set; }

        public Hierarchy(ID id)
            : base(id)
        {            
        }

        public Hierarchy(ID id, CodeRef root)
            : this(id)
        {            
            Contract.AssertNotNull(() => root);
            
            Root = root;
        }

        public Hierarchy(InternationalString name, ID id, CodeRef root)
            : this(id, root)
        {
            Name.Add(name);
        }
    
        public override Uri Urn
        {
            get
            {
                return new Uri(string.Format("{0}.hierarchicalcodelist.hierarchy={1}:{2}.{3}[{4}]".F(UrnPrefix, HierarchicalCodeList.AgencyID, HierarchicalCodeList.ID, ID, HierarchicalCodeList.Version)));
            }
        }

        public IEnumerable<CodeRef> GetCodeRefs()
        {
            return GetCodeRefs(Root);
        }

        private IEnumerable<CodeRef> GetCodeRefs(CodeRef root)
        {
            yield return root;

            foreach (var child in root.Children)
            {
                foreach (var subchild in GetCodeRefs(child))
                {
                    yield return subchild;
                }
            }
        }
    }
}
