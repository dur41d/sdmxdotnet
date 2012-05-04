using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using SDMX.Parsers;
using Common;

namespace SDMX
{
    public class DimensionCollection : Collection<Dimension>
    { }


    public class KeyFamily : MaintainableArtefact
    {
        public KeyFamily(InternationalString name, Id id, Id agencyId)
            : base(id, agencyId)
        {
            Name.Add(name);
            Dimensions = new Collection<Dimension>();
            Attributes = new Collection<Attribute>();
            Groups = new Collection<Group>();
            XMeasures = new Collection<CrossSectionalMeasure>();
        }

        // TODO: rethink exposing Collection<T> and maybe using one collection internally
        // for all componenets and exposing them via the properties Dimensions, TimeDimension, Attributes.. etc.
        public TimeDimension TimeDimension { get; set; }
        public PrimaryMeasure PrimaryMeasure { get; set; }
        public Collection<Dimension> Dimensions { get; private set; }
        public Collection<Group> Groups { get; private set; }
        public Collection<Attribute> Attributes { get; private set; }
        public Collection<CrossSectionalMeasure> XMeasures { get; set; }

        public Group CreateNewGroup(Id groupId)
        {
            var group = new Group(groupId, this);
            Groups.Add(group);
            return group;
        }
    
        public override Uri Urn
        {
            get 
            {
                return new Uri(string.Format("{0}.keyfamily.KeyFamily={1}:{2}[{3}]".F(UrnPrefix, AgencyId, Id, Version)));
            }
        }

        public bool ValidateDataQuery(DataQuery query)
        {
            Contract.AssertNotNull(query, "query");

            foreach (var criterion in GetCriteria(query.Criterion))
            {
                if (criterion is DimensionCriterion)
                {
                    var c = criterion as DimensionCriterion;
                    
                    var dim = Dimensions.Find(c.Name);

                    if (dim == null) 
                        return false;

                    if (!dim.CanSerialize(c.Value))
                        return false;
                }
                else if (criterion is AttributeCriterion)
                {
                    var c = criterion as AttributeCriterion;

                    var att = Attributes.Find(c.Name);

                    if (att == null)
                        return false;

                    if (!att.CanSerialize(c.Value))
                        return false;

                    //if (att.AttachementLevel != c.AttachmentLevel)
                    //    return false;
                }
            }

            return true;
        }

        private IEnumerable<ICriterion> GetCriteria(ICriterion criterion)
        {
            if (criterion is ICriteriaContainer)
            {
                foreach (var item in ((ICriteriaContainer)criterion).Criteria)
                    foreach (var c in GetCriteria(item))
                        yield return c;
            }
            else
            {
                yield return criterion;
            }
        }

        public Component FindComponent(Id id)
        {
            Component com = Dimensions.Find(id);
            if (com == null)
            {
                com = Attributes.Find(id);
            }
            if (com == null && id == TimeDimension.Concept.Id)
            {
                return TimeDimension;
            }
            if (com == null && id == PrimaryMeasure.Concept.Id)
            {
                return PrimaryMeasure;
            }

            return com;
        }

        public Component GetComponent(Id id)
        {
            var com = FindComponent(id);

            if (com == null)
            {
                throw new SDMXException("Did not find component with id '{0}'.", id);
            }

            return com;
        }
    }
}
