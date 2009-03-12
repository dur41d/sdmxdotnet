using System;
using System.Collections.Generic;
using System.Text;
using SDMX_Structure = SDMX_ML.Framework.Structure;
using SDMX_Message = SDMX_ML.Framework.Message;
using SDMX_Common = SDMX_ML.Framework.Common;
using SDMX_ML.Framework.Interfaces;
using System.Xml.Linq;
using System.Xml;

namespace SDMX_ML.Framework.Messages
{
    public class Structure : IMessage
    {
        private List<SDMX_Structure.CodelistType> _codelists = new List<SDMX_Structure.CodelistType>();
        private List<SDMX_Structure.ConceptType> _concepts = new List<SDMX_Structure.ConceptType>();
        private SDMX_Structure.KeyFamiliesType _keyfamilies = new SDMX_Structure.KeyFamiliesType();
        private SDMX_Message.HeaderType _header = new SDMX_Message.HeaderType();
        private XDocument _xmldoc;
        private XElement _xml;
        private XNamespace _nsStructurMessage;
        private XNamespace _nsStructur;
        private XNamespace _nsDefault;


        public Structure()
        {
        }

        public Structure(string structure)
        {
            _xml = XElement.Parse(structure);

            setNameSpace();

            setStructur();

        }

        private void setNameSpace()
        {
            _nsStructurMessage = _xml.Name.Namespace;
            _nsStructur = _xml.GetNamespaceOfPrefix("structure");
            _nsDefault = _xml.GetDefaultNamespace();
        }

        private void setStructur()
        {
            XElement codelists = _xml.Element(_nsDefault + "CodeLists");
            XElement keyfamilies = _xml.Element(_nsDefault + "KeyFamilies");
            XElement concepts = _xml.Element(_nsDefault + "Concepts");

            if(codelists != null)
                setCodelists(codelists);

            if(keyfamilies != null)
                setKeyFamilies(keyfamilies);

            if(concepts != null)
                setConcepts(concepts);

        }

        private void setCodelists(XElement structur)
        {
            SDMX_Structure.CodelistType codelist = new SDMX_Structure.CodelistType();
            XElement cl = structur.Element(_nsStructur + "CodeList");

            if(cl != null)
            {
                codelist.Id = cl.Attribute("id").Value;
                codelist.Agencyid = cl.Attribute("agencyID").Value;

                XElement name = cl.Element(_nsStructur + "Name");
                codelist.Lang = name.Attribute(XNamespace.Xml + "lang").Value;;

                foreach(XElement cv in cl.Elements())
                {
                    if(cv.Name == _nsStructur + "Code")
                    {
                        SDMX_Structure.CodeType c = new SDMX_Structure.CodeType();

                        c.Codevalue =cv.Attribute("value").Value;
                        XElement desc = cv.Element(_nsStructur + "Description");
                        c.Description = desc.Value;
                        c.Lang = desc.Attribute(XNamespace.Xml + "lang").Value;

                        codelist.Codevalues.Add(c);
                    }

                }

                 _codelists.Add(codelist);
            }
        }

        private void setCodeValues(XElement codelist)
        {

        }

        private void setConcepts(XElement concept)
        {
            foreach(XElement c in concept.Elements())
            {
                SDMX_Structure.ConceptType con = new SDMX_Structure.ConceptType();
                con.AgencyID = c.Attribute("agencyID").Value;
                con.ID = c.Attribute("id").Value;
                con.Name = c.Element(_nsStructur + "Name").Value;
               
                _concepts.Add(con);
            }

        }

        private void setKeyFamilies(XElement element)
        {
            XElement kf = element.Element(_nsStructur + "KeyFamily");
            SDMX_Structure.KeyFamilyType keyfamily = new SDMX_Structure.KeyFamilyType();
            keyfamily.ID = kf.Attribute("id").Value;
            keyfamily.AgencyID = kf.Attribute("agencyID").Value;
            XElement kfname = kf.Element(_nsStructur + "Name");
            keyfamily.Name = kf.Value;
            
            keyfamily.Component = getComponents(kf);

            _keyfamilies.KeyFamily.Add(keyfamily);
        }

        private SDMX_Structure.ComponentsType getComponents(XElement element)
        {
            SDMX_Structure.ComponentsType comp = new SDMX_Structure.ComponentsType();
            XElement Components = element.Element(_nsStructur + "Components");

            foreach(XElement dim in Components.Elements())
            {
                if(dim.Name == _nsStructur + "Dimension")
                {
                    SDMX_Structure.DimensionType d = new SDMX_Structure.DimensionType();
                    SDMX_Common.IDType cr = new SDMX_Common.IDType(dim.Attribute("conceptRef").Value.ToString());
                    d.Conceptref = cr;
                    SDMX_Common.IDType cl = new SDMX_Common.IDType(dim.Attribute("codelist").Value.ToString());
                    d.Codelist = cl;

                    if(dim.Attribute("isFrequencyDimension") != null)
                        d.IsFrequencyDimension = Convert.ToBoolean(dim.Attribute("isFrequencyDimension").Value);

                    comp.Dimensions.Add(d);
                }
                if(dim.Name == _nsStructur + "Attribute")
                {
                    //XElement attribute = dim.Element(_nsStructur + "Attribute");
                    SDMX_Structure.AttributeType a = new SDMX_Structure.AttributeType();
                    a.ConceptRef = new SDMX_Common.IDType(dim.Attribute("conceptRef").Value);
                    a.AttachmentLevel = getAttachmentLevel(dim);
                    a.AssignmentStatus = getAssignmentStatus(dim);
                    a.CrossSectionalAttachDataset = Convert.ToBoolean(dim.Attribute("crossSectionalAttachDataSet").Value);
                    a.CrossSectionalAttachGroup = Convert.ToBoolean(dim.Attribute("crossSectionalAttachGroup").Value);
                    a.CrossSectionalAttachSection = Convert.ToBoolean(dim.Attribute("crossSectionalAttachSection").Value);
                    a.CrossSectionalAttachObservation = Convert.ToBoolean(dim.Attribute("crossSectionalAttachObservation").Value);
                    
                    if(dim.Attribute("isTimeFormat") != null)
                        a.IsTimeFormat = Convert.ToBoolean(dim.Attribute("isTimeFormat").Value);

                    if(dim.Attribute("codelist") != null)
                        a.Codelist = new SDMX_Common.IDType(dim.Attribute("codelist").Value);

                    comp.Attribute.Add(a);
                }
            }

            return comp;

        }

        private AttachmentLevel getAttachmentLevel(XElement element)
        {
            AttachmentLevel level;

            switch(element.Attribute("attachmentLevel").Value)
            {
                case "Series":
                    level = AttachmentLevel.Series;
                    break;
                case "DataSet":
                    level = AttachmentLevel.DataSet;
                    break;
                case "Group":
                    level = AttachmentLevel.Group;
                    break;
                case "Observation":
                    level = AttachmentLevel.Observation;
                    break;
                default:
                    level = AttachmentLevel.Any;
                    break;
            }

            return level;
        }

        private AssignmentStatus getAssignmentStatus(XElement element)
        {
            AssignmentStatus status;

            switch(element.Attribute("assignmentStatus").Value)
            {
                case "Conditional":
                    status = AssignmentStatus.Conditional;
                    break;
                default:
                    status = AssignmentStatus.Mandatory;
                    break;
            }

            return status;
        }


        public SDMX_Message.HeaderType Header
        {
            get { return _header; }
            set { _header = value; }
        }

        public SDMX_Structure.KeyFamiliesType Keyfamilies
        {
            get { return _keyfamilies; }
            set { _keyfamilies = value; }
        }

        public List<SDMX_Structure.CodelistType> Codelists
        {
            get { return _codelists; }
            set { _codelists = value; }
        }

        public List<SDMX_Structure.ConceptType> Concepts
        {
            get { return _concepts; }
            set { _concepts = value; }
        }

        public string ToXml()
        {
            try
            {
                XElement x = GetXml();
                return _xmldoc.ToString();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public XDocument GetXmlDocument()
        {
            XElement x = GetXml();

            return _xmldoc;
        }

        public XElement GetXml()
        {
            _xmldoc = new XDocument(
                new XDeclaration("1.0", "UTF-8", "yes"));
            
             XElement structure = new XElement(Namespaces.GetAllNamespaces("Structure"));         
           
            _header.Prepared = DateTime.Now.ToString("s") + "-01:00";
            _header.Extracted = DateTime.Now.ToString("s") + "-01:00";
            _header.DataSetAction = SDMX_Message.HeaderType.EnumDataSetAction.Information;
            
            structure.Add(_header.GetXml());

            if(_codelists.Count > 0)
                structure.Add(addCodelist());

            if(_concepts.Count > 0)
                structure.Add(addConcept());

            if(_keyfamilies.KeyFamily.Count > 0)
                structure.Add(addKeyFamilies());

            _xmldoc.Add(structure);
            
            _xmldoc.Save(@"c:\projektxml.xml");

            return structure;
        }

        #region Codelist

        private XElement addCodelist()
        {
            XElement codelists = new XElement(Namespaces.GetNS("message") + "CodeLists");

            foreach(SDMX_Structure.CodelistType c in _codelists)
            {
                if(c.Agencyid != null)
                {
                    XElement codelist = new XElement(Namespaces.GetNS("structure") + "CodeList",
                        new XAttribute("id", c.Id), new XAttribute("agencyID", c.Agencyid), 
                        new XAttribute("version", c.Version), new XAttribute("uri", c.Uri),
                        new XElement(Namespaces.GetNS("structure") + "Name", 
                        new XAttribute(XNamespace.Xml + "lang", "en")));

                    codelist = addCodevalues(c, codelist);

                    codelists.Add(codelist);
                }
            }

            return codelists;
        }

        private XElement addCodevalues(SDMX_Structure.CodelistType codelist, XElement element)
        {
            foreach(SDMX_Structure.CodeType c in codelist.Codevalues)
            {
                XElement code = new XElement(Namespaces.GetNS("structure") + "Code",
                    new XAttribute("value", c.Codevalue),
                    new XElement(Namespaces.GetNS("structure") + "Description", c.Description,
                    new XAttribute(XNamespace.Xml + "lang", "en")));

                element.Add(code);
            }

            return element;
        }

        #endregion

        #region Concept

         private XElement addConcept()
        {
            XElement concepts = new XElement(Namespaces.GetNS("message") + "Concepts");

            foreach(SDMX_Structure.ConceptType c in _concepts)
            {
                XElement concept = new XElement(Namespaces.GetNS("structure") + "Concept",
                    new XAttribute("id", c.ID), new XAttribute("agencyID", c.AgencyID), 
                    new XAttribute("version", c.Version), new XAttribute("uri", "c.Uri"),
                    new XElement(Namespaces.GetNS("structure") + "Name", c.Name, 
                    new XAttribute(XNamespace.Xml + "lang", "en")));

                concepts.Add(concept);
            }

            return concepts;
        }

        #endregion

        #region KeyFamily

        private XElement addKeyFamilies()
        {
            XElement keyfamilies = new XElement(Namespaces.GetNS("message") + "KeyFamilies");

            foreach(SDMX_Structure.KeyFamilyType kf in _keyfamilies.KeyFamily)
            {
                XElement keyfamily = new XElement(Namespaces.GetNS("structure") + "KeyFamily", 
                    new XAttribute("id", kf.ID), new XAttribute("agencyID", kf.AgencyID),
                    new XAttribute("version", ""), new XAttribute("uri", ""), 
                    new XElement(Namespaces.GetNS("structure") + "Name",
                    new XAttribute(XNamespace.Xml + "lang", "en"), kf.Name));

                keyfamily.Add(getComponents(kf.Component));

                keyfamilies.Add(keyfamily);
            }

            return keyfamilies;

        }

        private XElement getComponents(SDMX_Structure.ComponentsType comp)
        {
            XElement component = new XElement(Namespaces.GetNS("structure") + "Components");

            foreach(SDMX_Structure.DimensionType d in comp.Dimensions)
                component.Add(getDimension(d));
            
            if(comp.Primarymeasure != null)
                component.Add(getPrimaryMeasure(comp.Primarymeasure));

            foreach(SDMX_ML.Framework.Structure.AttributeType a in comp.Attribute)
                component.Add(getAttribute(a));
            

            return component;
        }

        private XElement getDimension(SDMX_Structure.DimensionType d)
        {
            XElement dimension = new XElement(Namespaces.GetNS("structure") + "Dimension",
                new XAttribute("conceptRef", d.Conceptref.Value), new XAttribute("codelist", d.Codelist.Value),
                new XAttribute("isFrequencyDimension", d.IsFrequencyDimension.ToString().ToLower()));

            return dimension;
          
        }

        private XElement getPrimaryMeasure(SDMX_Structure.PrimaryMeasureType pm)
        {
            XElement primary = new XElement(Namespaces.GetNS("structure") + "PrimaryMeasure",
                new XAttribute("conceptRef", pm.ConceptRef.Value), 
                new XElement(Namespaces.GetNS("structure") + "TextFormat", 
                new XAttribute("textType", pm.Textformat)));

            return primary;
        }

        private XElement getAttribute(SDMX_ML.Framework.Structure.AttributeType a)
        {
            XElement attribute = new XElement(Namespaces.GetNS("structure") + "Attribute",
                new XAttribute("conceptRef", a.ConceptRef.Value), 
                new XAttribute("attachmentLevel", a.AttachmentLevel), 
                new XAttribute("assignmentStatus", a.AssignmentStatus),
                new XAttribute("crossSectionalAttachDataSet", a.CrossSectionalAttachDataset.ToString().ToLower()),
                new XAttribute("crossSectionalAttachGroup", a.CrossSectionalAttachGroup.ToString().ToLower()),
                new XAttribute("crossSectionalAttachSection", a.CrossSectionalAttachSection.ToString().ToLower()),
                new XAttribute("crossSectionalAttachObservation", a.CrossSectionalAttachObservation.ToString().ToLower()));

            if(a.Codelist.Value != "")
                attribute.Add(new XAttribute("codelist", a.Codelist.Value));
            if(a.IsTimeFormat)
                attribute.Add(new XAttribute("isTimeFormat", "true"));

            return attribute;
          
        }

        #endregion
    }
}
