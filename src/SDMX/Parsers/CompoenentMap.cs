using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Common;
using OXM;

namespace SDMX.Parsers
{
    public abstract class CompoenentMap<T> : AnnotableArtefactMap<T>
            where T : Component
    {
        DSD _dsd;

        protected AttributeMap<T, ID> conceptRef;
        protected AttributeMap<T, string> conceptVersion;
        protected AttributeMap<T, ID> conceptAgency;
        protected AttributeMap<T, ID> conceptSchemeRef;
        protected AttributeMap<T, ID> conceptSchemeAgency;

        private AttributeMap<T, ID> codelist;
        private AttributeMap<T, string> codelistVersion;
        private AttributeMap<T, ID> codelistAgency;

        private AttributeMap<T, bool> crossSectionalAttachDataSet;
        private AttributeMap<T, bool> crossSectionalAttachGroup;
        private AttributeMap<T, bool> crossSectionalAttachSection;
        private AttributeMap<T, bool> crossSectionalAttachObservation;

        public CompoenentMap(DSD dsd)
        {
            _dsd = dsd;

            conceptRef = MapAttribute<ID>("conceptRef", true)
               .Getter(d => d.Concept.ID)
               .Parser(s => s);

            conceptVersion = MapAttribute<string>("conceptVersion", false)
               .Getter(d => d.Concept.Version)
               .Parser(s => s);

            conceptAgency = MapAttribute<ID>("conceptAgency", false)
               .Getter(d => d.Concept.AgencyID)
               .Parser(s => s);

            conceptSchemeRef = MapAttribute<ID>("conceptSchemeRef", false)
              .Getter(d => d.Concept.ConceptScheme.ID)
              .Parser(s => s);

            conceptSchemeAgency = MapAttribute<ID>("conceptSchemeAgency", false)
              .Getter(d => d.Concept.ConceptScheme.AgencyID)
              .Parser(s => s);

            codelist = MapAttribute<ID>("codelist", false)
            .Getter(d => d.CodeList.AgencyID)
            .Parser(s => s);

            codelistVersion = MapAttribute<string>("codelistVersion", false)
              .Getter(d => d.CodeList.Version)
              .Parser(s => s);

            codelistAgency = MapAttribute<ID>("codelistAgency", false)
              .Getter(d => d.CodeList.AgencyID)
              .Parser(s => s);

            crossSectionalAttachDataSet = MapAttribute<bool>("crossSectionalAttachDataSet", false)
              .Getter(d => d.CrossSectionalAttachmentLevel == CrossSectionalAttachmentLevel.DataSet)
              .Parser(s => bool.Parse(s));

            crossSectionalAttachGroup = MapAttribute<bool>("crossSectionalAttachGroup", false)
             .Getter(d => d.CrossSectionalAttachmentLevel == CrossSectionalAttachmentLevel.Group)
             .Parser(s => bool.Parse(s));

            crossSectionalAttachSection = MapAttribute<bool>("crossSectionalAttachSection", false)
             .Getter(d => d.CrossSectionalAttachmentLevel == CrossSectionalAttachmentLevel.Section)
             .Parser(s => bool.Parse(s));

            crossSectionalAttachObservation = MapAttribute<bool>("crossSectionalAttachObservation", false)
             .Getter(d => d.CrossSectionalAttachmentLevel == CrossSectionalAttachmentLevel.Observation)
             .Parser(s => bool.Parse(s));

            MapElement<TextFormat>("TextFormat", false)
                .Parser(new TextFormatMap())
                .Getter(o => o.TextFormat)
                .Setter((o, p) => o.TextFormat = p);
        }

        protected void SetComponentProperties(T compoenent)
        {
            compoenent.CodeList = _dsd.GetCodeList(codelist.Value, codelistAgency.Value, codelistVersion.Value);

            if (crossSectionalAttachDataSet.Value)
            {
                compoenent.CrossSectionalAttachmentLevel = CrossSectionalAttachmentLevel.DataSet;
            }
            else if (crossSectionalAttachGroup.Value)
            {
                compoenent.CrossSectionalAttachmentLevel = CrossSectionalAttachmentLevel.Group;
            }
            else if (crossSectionalAttachSection.Value)
            {
                compoenent.CrossSectionalAttachmentLevel = CrossSectionalAttachmentLevel.Section;
            }
            else if (crossSectionalAttachObservation.Value)
            {
                compoenent.CrossSectionalAttachmentLevel = CrossSectionalAttachmentLevel.Observation;
            }
            else
            {
                compoenent.CrossSectionalAttachmentLevel = CrossSectionalAttachmentLevel.None;
            }

        }
    }
}
