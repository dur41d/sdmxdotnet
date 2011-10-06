using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using SDMX.Parsers;
using Common;

namespace SDMX
{
    public class KeyFamily : MaintainableArtefact
    {
        public KeyFamily(InternationalString name, Id id, Id agencyId)
            : base(id, agencyId)
        {
            Name.Add(name);
            Dimensions = new Collection<Dimension>();
            Attributes = new Collection<Attribute>();
            Groups = new Collection<GroupDescriptor>();
            XMeasures = new Collection<CrossSectionalMeasure>();
        }

        public TimeDimension TimeDimension { get; internal set; }
        public PrimaryMeasure PrimaryMeasure { get; internal set; }
        public Collection<Dimension> Dimensions { get; private set; }
        public Collection<GroupDescriptor> Groups { get; private set; }
        public Collection<Attribute> Attributes { get; private set; }
        public Collection<CrossSectionalMeasure> XMeasures { get; set; }

        public GroupDescriptor CreateNewGroup(Id groupId)
        {
            var group = new GroupDescriptor(groupId, this);
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
                    // TODO: remove TryGet and make Get return null to simplify the api
                    var dim = Dimensions.TryGet(c.Name);

                    if (dim == null) 
                        return false;

                    if (!dim.IsValid((CodeValue)c.Value))
                        return false;
                }
                else if (criterion is AttributeCriterion)
                {
                    var c = criterion as AttributeCriterion;

                    var att = Attributes.TryGet(c.Name);

                    if (att == null)
                        return false;

                    if (!att.IsValid((CodeValue)c.Value))
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
       

        internal void ValidateAttribute(Id conceptId, Value value, AttachmentLevel level)
        {
            var attribute = Attributes.TryGet(conceptId);
            if (attribute == null)
            {
                throw new SDMXException("Invalid attribute '{0}'.", conceptId);
            }
            if (attribute.AttachementLevel != level)
            {
                throw new SDMXException("Attribute '{0}' has attachment level '{1}' but was attached to level '{2}'."
                    , conceptId, attribute.AttachementLevel, level);
            }
            if (!attribute.IsValid(value))
            {
                throw new SDMXException("Invalid value for attribute '{0}'. Value: {1}."
                    , conceptId, value);
            }
        }     

        internal Component GetComponent(Id id)
        {
            Component com = Dimensions.TryGet(id);
            if (com == null)
            {
                com = Attributes.TryGet(id);                
            }
            if (com == null && id == TimeDimension.Concept.Id)
            {
                return TimeDimension;
            }
            if (com == null && id == PrimaryMeasure.Concept.Id)
            {
                return PrimaryMeasure;
            }

            if (com == null)
            {
                throw new SDMXException("Did not find component with id '{0}'.", id);
            }
            return com;
        }
    }
}
