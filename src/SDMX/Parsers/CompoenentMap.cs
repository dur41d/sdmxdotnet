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
    public class ConceptRef
    {
        public ID ID { get; set; }
        public string Version { get; set; }
        public ID Agency { get; set; }
        public ConceptSchemeRef SchemeRef { get; set; }
    }

    public class ConceptSchemeRef
    {
        public ID ID { get; set; }
        public string Version { get; set; }
        public ID Agency { get; set; }
    }

    public class CodelistRef
    {
        public ID ID { get; set; }
        public string Version { get; set; }
        public ID AgencyID { get; set; }
    }

    public class CrossSectionalAttachment
    {
        public bool CrossSectionalAttachDataSet { get; set; }
        public bool CrossSectionalAttachGroup { get; set; }
        public bool CrossSectionalAttachSection { get; set; }
        public bool CrossSectionalAttachObservation { get; set; }
    }
    
    public abstract class CompoenentMap<T> : AnnotableArtefactMap<T>
            where T : Component
    {
        protected abstract T Create(Concept conecpt);

        public CompoenentMap(DSD dsd)
        {
            var conceptAttributes = MapAttributeCollection<ConceptRef>()
                .Getter(o => GetConceptRef(o))
                .Setter(p =>
                    {
                        var concept = dsd.GetConcept(p);
                        Instance = Create(concept);
                    });

            conceptAttributes.MapAttribute<ID>("conceptRef", true)
               .Getter(o => o.ID)
               .Setter(p => Instance.ID = p)
               .Parser(s => new ID(s));

            conceptAttributes.MapAttribute<string>("conceptVersion", false)
                .Getter(o => o.Version)
                .Setter(p => Instance.Version = p)
                .Parser(s => s);

            conceptAttributes.MapAttribute<ID>("conceptAgency", false)
                .Getter(o => o.Agency)
                .Setter(p => Instance.Agency = p)
                .Parser(s => new ID(s));        
           

            MapAttribute<ID>("conceptSchemeRef", false)
               .Getter(d => d.Concept.ConceptScheme.ID)
               .Setter(p => conceptSchemeRef = p)
               .Parser(s => s);

            MapAttribute<ID>("conceptSchemeAgency", false)
              .Getter(d => d.Concept.ConceptScheme.AgencyID)
              .Setter(p => conceptSchemeAgency = p)
              .Parser(s => s);

            MapAttribute<ID>("codelist", false)
            .Getter(d => d.CodeList.ID)
            .Setter(p => codelist = p)
            .Parser(s => s);

            MapAttribute<string>("codelistVersion", false)
              .Getter(d => d.CodeList.Version)
              .Setter(p => codelistVersion = p)
              .Parser(s => s);

            MapAttribute<ID>("codelistAgency", false)
              .Getter(d => d.CodeList.AgencyID)
              .Setter(p => codelistAgency = p)
              .Parser(s => s);

            MapAttribute<bool>("crossSectionalAttachDataSet", false)
              .Getter(d => d.CrossSectionalAttachmentLevel == CrossSectionalAttachmentLevel.DataSet)
              .Setter(p => crossSectionalAttachDataSet = p)
              .Parser(s => bool.Parse(s));

            MapAttribute<bool>("crossSectionalAttachGroup", false)
             .Getter(d => d.CrossSectionalAttachmentLevel == CrossSectionalAttachmentLevel.Group)
             .Setter(p => crossSectionalAttachGroup = p)
             .Parser(s => bool.Parse(s));

            MapAttribute<bool>("crossSectionalAttachSection", false)
             .Getter(d => d.CrossSectionalAttachmentLevel == CrossSectionalAttachmentLevel.Section)
             .Setter(p => crossSectionalAttachSection = p)
             .Parser(s => bool.Parse(s));

            MapAttribute<bool>("crossSectionalAttachObservation", false)
             .Getter(d => d.CrossSectionalAttachmentLevel == CrossSectionalAttachmentLevel.Observation)
             .Setter(p => crossSectionalAttachObservation = p)
             .Parser(s => bool.Parse(s));

            MapElement<TextFormat>("TextFormat", false)
                .Parser(new TextFormatMap())
                .Getter(o => o.TextFormat)
                .Setter(p => Instance.TextFormat = p);
        }

        private ConceptRef GetConceptRef(T component)
        {
            var conceptRef = new ConceptRef();
            conceptRef.ID = component.Concept.ID;
            conceptRef.Version = component.Concept.Version;
            conceptRef.Agency = component.Concept.AgencyID;
            conceptRef.SchemeRef = new ConceptSchemeRef();
            conceptRef.SchemeRef.ID = component.Concept.ConceptScheme.ID;
            conceptRef.SchemeRef.Version = component.Concept.ConceptScheme.Version;
            conceptRef.SchemeRef.Agency = component.Concept.ConceptScheme.AgencyID;

            return conceptRef;
        }

        //protected void SetComponentProperties(T compoenent)
        //{
        //    compoenent.CodeList = _dsd.GetCodeList(codelist.Value, codelistAgency.Value, codelistVersion.Value);

        //    if (crossSectionalAttachDataSet.Value)
        //    {
        //        compoenent.CrossSectionalAttachmentLevel = CrossSectionalAttachmentLevel.DataSet;
        //    }
        //    else if (crossSectionalAttachGroup.Value)
        //    {
        //        compoenent.CrossSectionalAttachmentLevel = CrossSectionalAttachmentLevel.Group;
        //    }
        //    else if (crossSectionalAttachSection.Value)
        //    {
        //        compoenent.CrossSectionalAttachmentLevel = CrossSectionalAttachmentLevel.Section;
        //    }
        //    else if (crossSectionalAttachObservation.Value)
        //    {
        //        compoenent.CrossSectionalAttachmentLevel = CrossSectionalAttachmentLevel.Observation;
        //    }
        //    else
        //    {
        //        compoenent.CrossSectionalAttachmentLevel = CrossSectionalAttachmentLevel.None;
        //    }
        //}


    }
}
