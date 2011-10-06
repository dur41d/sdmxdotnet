using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace SDMX
{
    public class ConceptScheme : MaintainableArtefact, IEnumerable<Concept>
    {
        private List<Concept> concepts = new List<Concept>();        
      
        public ConceptScheme(InternationalString name, Id id, Id agencyId)
            : base(id, agencyId)
        {
            Name.Add(name);
        }

        public void Add(Concept concept)
        {
            Contract.AssertNotNull(concept, "concept");
            concept.ConceptScheme = this;
            concepts.Add(concept);
        }

        public void Remove(Concept concept)
        {
            Contract.AssertNotNull(concept, "concept");
            concepts.Remove(concept);
        }

        public Concept Get(Id conceptId)
        {            
            return concepts.Where(c => c.Id == conceptId).Single();
        }

        public override Uri Urn
        {
            get
            {
                return new Uri(string.Format("{0}.conceptScheme={1}:{2}[{3}]".F(UrnPrefix, AgencyId, Id, Version)));
            }
        }
       

        #region IEnumerable<Concept> Members

        public IEnumerator<Concept> GetEnumerator()
        {
            foreach (var concept in concepts)
            {
                yield return concept;
            }
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
