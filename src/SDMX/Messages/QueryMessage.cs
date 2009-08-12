using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using SDMX_Query = SDMX_ML.Framework.Query;
using SDMX_Structure = SDMX_ML.Framework.Structure;
using SDMX_Common = SDMX_ML.Framework.Common;
using SDMX_Message = SDMX_ML.Framework.Message;
using SDMX_Abstract = SDMX_ML.Framework.Abstract;
using SDMX_ML.Framework.Abstract;
using SDMX_ML.Framework.Interfaces;




namespace SDMX_ML.Framework.Messages
{
    /// <summary>
    /// The Query message allows standard querying of SDMX-compliant databases and web services. 
    /// It is intended to be used in non-registry exchanges, and is focused on data sets and metadata 
    /// sets. It allows queries to retrieve data, metadata, key families, metadata structure definitions, 
    /// codelists, concepts, and other structural metadata. Note that date and time formats are structured 
    /// according to the common:TimePeriodType, rather than being specified in the query. The response 
    /// documents for this query mesage are data formats (for data queries), metadata formats (for metadata 
    /// queries), and the SDMX Structure MEssage (for all other queries).
    /// </summary>
    public class QueryMessage : IMessage
    {
        #region Fields

        private XNamespace _nsQueryMessage;
        private XNamespace _nsQuery;
        private XNamespace _default;
        private XElement _xml;
        private List<SDMX_Query.DataWhereType> _datawhere = new List<SDMX_ML.Framework.Query.DataWhereType>();
        private List<SDMX_Query.CodelistWhereType> _codelistwhere = new List<SDMX_ML.Framework.Query.CodelistWhereType>();
        private List<SDMX_Query.ConceptWhereType> _conceptwhere = new List<SDMX_Query.ConceptWhereType>();
        private List<SDMX_Query.KeyFamilyWhereType> _keyfamilywhere = new List<SDMX_ML.Framework.Query.KeyFamilyWhereType>();
        private SDMX_Message.HeaderType _header = null;
        private XDocument _xmldoc;

        

        #endregion

        #region Constructor

        /// <summary>
        /// Creates objects from the QueryMessage parsed in the constructor
        /// </summary>
        /// <param name="query">Xml Element/Document in the format of a Query Message in SDMX_ML format</param>
        public QueryMessage(string query)
        {
            _xml = XElement.Parse(query);

            //Set the default namespaces
            setNameSpace();

            if(_header == null)
                _header = new SDMX_Message.HeaderType(_xml);

            //This will start creating objects from the xml elements
            setQuery();
        }

        /// <summary>
        /// Constructor for creating a Xml document from objects
        /// </summary>
        /// <remarks>Use the ToXml() method to write the document</remarks>
        public QueryMessage()
        {
            _header = new SDMX_Message.HeaderType();
        }
        
        #endregion

        #region Create objects from XML

        private void setNameSpace()
        {
            _nsQueryMessage = _xml.Name.Namespace;
            _nsQuery = _xml.GetNamespaceOfPrefix("query");
            _default = _xml.GetDefaultNamespace();
        }

        private void setQuery()
        {
            XElement query = _xml.Element(_nsQueryMessage + "Query");

            foreach(XElement element in query.Elements(_nsQuery + "DataWhere"))
                setDataWhere(element);

            foreach(XElement element in query.Elements(_nsQuery + "CodelistWhere"))
                setCodelistWhere(element);

            foreach(XElement element in query.Elements(_nsQuery + "KeyFamilyWhere"))
                setKeyFamilyWhere(element);

            foreach(XElement element in query.Elements(_nsQuery + "ConceptWhere"))
                setConceptWhere(element);
        }

        private void setDataWhere(XElement element)
        {
            SDMX_Query.DataWhereType dw = new SDMX_Query.DataWhereType();

            //Only one of these elements can be added to the DataWhere element
            if(element.Element(_nsQuery + "DataSet") != null)
                dw.Dataset = getElementValue(element.Element(_nsQuery + "DataSet"));
            else if(element.Element(_nsQuery + "KeyFamily") != null)
                dw.Keyfamily = getKeyFamily(element.Element(_nsQuery + "KeyFamily"));
            if(element.Element(_nsQuery + "Dimension") != null)
                dw.Dimensions = getDimension(element.Element(_nsQuery + "Dimension"));
            else if(element.Element(_nsQuery + "Attribute") != null)
                dw.Attributes = getAttribute(element.Element(_nsQuery + "Attribute"));
            else if(element.Element(_nsQuery + "Codelist") != null)
                dw.Codelists = getCodelist(element.Element(_nsQuery + "Codelist"));
            else if(element.Element(_nsQuery + "Time") != null)
                dw.Time = getTime(element.Element(_nsQuery + "Time"));
            else if(element.Element(_nsQuery + "Category") != null)
                dw.Category = getCategory(element.Element(_nsQuery + "Category"));
            else if(element.Element(_nsQuery + "Concept") != null)
                dw.Concept = getElementValue(element.Element(_nsQuery + "Concept"));
            else if(element.Element(_nsQuery + "DataProvider") != null)
                dw.DataProvider = getElementValue(element.Element(_nsQuery + "DataProvider"));
            else if(element.Element(_nsQuery + "Dataflow") != null)
                dw.DataFlow = getElementValue(element.Element(_nsQuery + "Dataflow"));
            else if(element.Element(_nsQuery + "Version") != null)
                dw.Version = getElementValue(element.Element(_nsQuery + "Version"));
            else if(element.Element(_nsQuery + "Or") != null)
            {
               SDMX_Query.OrType or = (SDMX_Query.OrType) getAndOr(element.Element(_nsQuery + "Or"), "Or");
                
                if(or != null)
                    dw.Or = or;
            }
            else if(element.Element(_nsQuery + "And") != null)
            {
                SDMX_Query.AndType and = (SDMX_Query.AndType) getAndOr(element.Element(_nsQuery + "And"), "And");
                
                if(and != null)
                    dw.And = and;
            }

            _datawhere.Add(dw);
        }

        private void setCodelistWhere(XElement element)
        {
            try
            {
                SDMX_Query.CodelistWhereType cw = new SDMX_Query.CodelistWhereType();

                if(element.Element(_nsQuery + "Codelist") != null)
                    cw.Codelist = getCodelist(element.Element(_nsQuery + "Codelist"));
                else if(element.Element(_nsQuery + "AgencyID") != null)
                    cw.Agencyid = getElementValue(element.Element(_nsQuery + "AgencyID"));
                else if(element.Element(_nsQuery + "Version") != null)
                    cw.Version = getElementValue(element.Element(_nsQuery + "Version"));
                else if(element.Element(_nsQuery + "Or") != null)
                {
                    SDMX_Query.OrType or = (SDMX_Query.OrType) getAndOr(element.Element(_nsQuery + "Or"), "Or");
                
                    if(or != null)
                        cw.Or = or;
                }
                else if(element.Element(_nsQuery + "And") != null)
                {
                    SDMX_Query.AndType and = (SDMX_Query.AndType) getAndOr(element.Element(_nsQuery + "And"), "And");
                
                    if(and != null)
                        cw.And = and;
                }

               _codelistwhere.Add(cw);
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void setKeyFamilyWhere(XElement element)
        {
            _keyfamilywhere = new List<SDMX_Query.KeyFamilyWhereType>();

            try
            {
                SDMX_Query.KeyFamilyWhereType kw = new SDMX_Query.KeyFamilyWhereType();

                if(element.Element(_nsQuery + "KeyFamily") != null)
                    kw.Keyfamily = getKeyFamily(element.Element(_nsQuery + "KeyFamily"));
                else if(element.Element(_nsQuery + "Dimension") != null)
                    kw.Dimension = getDimension(element.Element(_nsQuery + "Dimension"));
                else if(element.Element(_nsQuery + "Attribute") != null)
                    kw.Attribute = getAttribute(element.Element(_nsQuery + "Attribute"));
                else if(element.Element(_nsQuery + "Codelist") != null)
                    kw.Codelist = getCodelist(element.Element(_nsQuery + "Codelist"));
                else if(element.Element(_nsQuery + "Category") != null)
                    kw.Category = getCategory(element.Element(_nsQuery + "Category"));
                else if(element.Element(_nsQuery + "Concept") != null)
                    kw.Concept = getElementValue(element.Element(_nsQuery + "Concept"));
                else if(element.Element(_nsQuery + "AgencyID") != null)
                    kw.AgencyID = getElementValue(element.Element(_nsQuery + "AgencyID"));
                else if(element.Element(_nsQuery + "Version") != null)
                    kw.Version = getElementValue(element.Element(_nsQuery + "Version"));
                else if(element.Element(_nsQuery + "Or") != null)
                {
                    SDMX_Query.OrType or = (SDMX_Query.OrType) getAndOr(element.Element(_nsQuery + "Or"), "Or");
                
                    if(or != null)
                        kw.Or = or;
                }
                else if(element.Element(_nsQuery + "And") != null)
                {
                    SDMX_Query.AndType and = (SDMX_Query.AndType) getAndOr(element.Element(_nsQuery + "And"), "And");
                
                    if(and != null)
                        kw.And = and;
                }

                _keyfamilywhere.Add(kw);

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void setConceptWhere(XElement element)
        {
            try
            {
                SDMX_Query.ConceptWhereType conw = new SDMX_Query.ConceptWhereType();

                XElement cw = element.Element(_nsQuery + "ConceptWhere");

                if(element.Element(_nsQuery + "Concept") != null)
                    conw.Concept = getElementValue(element.Element(_nsQuery + "Concept"));
                else if(element.Element(_nsQuery + "AgencyID") != null)
                    conw.AgencyID = getElementValue(element.Element(_nsQuery + "AgencyID"));
                else if(element.Element(_nsQuery + "Version") != null)
                    conw.Version = getElementValue(element.Element(_nsQuery + "Version"));
                else if(element.Element(_nsQuery + "Or") != null)
                {
                    SDMX_Query.OrType or = (SDMX_Query.OrType) getAndOr(element.Element(_nsQuery + "Or"), "Or");
                
                    if(or != null)
                        conw.Or = or;
                }
                else if(element.Element(_nsQuery + "And") != null)
                {
                    SDMX_Query.AndType and = (SDMX_Query.AndType) getAndOr(element.Element(_nsQuery + "And"), "And");
                
                    if(and != null)
                        conw.And = and;
                }

                _conceptwhere.Add(conw);

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private string getKeyFamily(XElement element)
        {
            if(element != null)
                return element.Value;
            else
                return null;
        }

        private SDMX_Abstract.AndOR getAndOr(XElement element, string andor)
        {
            SDMX_Abstract.AndOR objandor = null;


            if(element != null)
            {
                if(andor == "Or")
                    objandor = new SDMX_Query.OrType();
                else
                    objandor = new SDMX_Query.AndType();
               
                foreach(XElement e in element.Elements(_nsQuery + "DataSet"))
                    objandor.Dataset.Add(getElementValue(e));

                foreach(XElement e in element.Elements(_nsQuery + "MetadataSet"))
                    objandor.Metadataset.Add(getElementValue(e));

               foreach(XElement e in element.Elements(_nsQuery + "KeyFamily"))
                    objandor.Keyfamily.Add(getKeyFamily(e));

                foreach(XElement e in element.Elements(_nsQuery + "MetadataStructure"))
                    objandor.Metadatastructure.Add(getElementValue(e));

                foreach(XElement e in element.Elements(_nsQuery + "Dimension"))
                    objandor.Dimensions.Add(getDimension(e));

                foreach(XElement e in element.Elements(_nsQuery + "StructureComponent"))
                    objandor.Structurecomponent.Add(getStructureComponent(e));

                foreach(XElement e in element.Elements(_nsQuery + "Attribute"))
                    objandor.Attributes.Add(getAttribute(e));

                foreach(XElement e in element.Elements(_nsQuery + "Codelist"))
                    objandor.Codelists.Add(getCodelist(e));

                foreach(XElement e in element.Elements(_nsQuery + "Time"))
                    objandor.Time.Add(getTime(e));
 
                foreach(XElement e in element.Elements(_nsQuery + "Category"))
                    objandor.Category.Add(getCategory(e));

                foreach(XElement e in element.Elements(_nsQuery + "Concept"))
                    objandor.Concept.Add(getElementValue(e));

                foreach(XElement e in element.Elements(_nsQuery + "AgencyID"))
                    objandor.Agencyid.Add(getElementValue(e));

                foreach(XElement e in element.Elements(_nsQuery + "DataProvider"))
                    objandor.DataProvider.Add(getElementValue(e));

                foreach(XElement e in element.Elements(_nsQuery + "Dataflow"))
                    objandor.DataFlow.Add(getElementValue(e));

                foreach(XElement e in element.Elements(_nsQuery + "Metadataflow"))
                    objandor.MetadataFlow.Add(getElementValue(e));

                foreach(XElement e in element.Elements(_nsQuery + "Version"))
                    objandor.Version.Add(getElementValue(e));

                //Recursive calls
                foreach(XElement e in element.Elements(_nsQuery + "Or"))
                    objandor.Or.Add(getAndOr(e, "Or"));

                //Recursive calls
                foreach(XElement e in element.Elements(_nsQuery + "And"))
                    objandor.And.Add(getAndOr(e, "And"));

            }

            return objandor;
        }

        private SDMX_Query.DimensionType getDimension(XElement element)
        {
            SDMX_Query.DimensionType dimension = null;

            if(element != null)
            {
                XAttribute attribute = element.Attribute("id");
                dimension = new SDMX_Query.DimensionType(attribute.Value, element.Value);
            }

            return dimension;
        }

        private SDMX_Query.TimeType getTime(XElement element)
        {
            SDMX_Query.TimeType time = new SDMX_Query.TimeType();

            if(element != null)
            {
                XElement starttime = element.Element(_nsQuery + "StartTime");

                SDMX_Common.TimePeriodType sttime = new SDMX_Common.TimePeriodType();

                if(starttime != null)
                {
                    sttime.TimePeriod = starttime.Value;
                    time.StartTime = sttime;
                }

                XElement endtime = element.Element(_nsQuery + "EndTime");

                SDMX_Common.TimePeriodType entime = new SDMX_Common.TimePeriodType();

                if(endtime != null)
                {
                    entime.TimePeriod = endtime.Value;
                    time.EndTime = entime;
                }
            }

            return time;

        }

        private SDMX_Query.AttributeType getAttribute(XElement element)
        {
            XAttribute attachmentlevel = element.Attribute("attachmentLevel");
            AttachmentLevel level = (AttachmentLevel)Enum.Parse(typeof(AttachmentLevel), 
                attachmentlevel.Value);
            XAttribute id = element.Attribute("id");

            SDMX_Query.AttributeType attribute = new SDMX_Query.AttributeType();

            return attribute;
        }

        private SDMX_Query.CodelistType getCodelist(XElement element)
        {
            XAttribute id = element.Attribute("id");
            //XAttribute type = element.Attribute("type");

            SDMX_Query.CodelistType codelist = new SDMX_Query.CodelistType();
            
            if(id != null)
                codelist.Id = id.Value;
            
            //if(type != null)
            //    codelist.Type = type.Value;
            
            return codelist;
        }

        private string getElementValue(XElement element)
        {
            return element.Value;
        }

        private SDMX_Query.CategoryType getCategory(XElement element)
        {
            SDMX_Query.CategoryType category = new SDMX_Query.CategoryType();

            if(element != null)
            {
                if(element.Attribute("ID") != null)
                {
                    SDMX_Common.IDType id = new SDMX_Common.IDType();
                    id.Value = element.Attribute("ID").Value;
                    category.ID = id;
                }

                if(element.Attribute("agencyID") != null)
                {
                    SDMX_Common.IDType id = new SDMX_Common.IDType();
                    id.Value = element.Attribute("agencyID").Value;
                    category.agencyID = id;
                }

                if(element.Attribute("CategoryScheme") != null)
                {
                    SDMX_Common.IDType id = new SDMX_Common.IDType();
                    id.Value = element.Attribute("CategoryScheme").Value;
                    category.CategoryScheme = id;
                }

                if(element.Attribute("version") != null)
                    category.Version = element.Attribute("version").Value;

                if(element.Value != null)
                    category.Value = element.Value;
            }


            return category;
        }

        private SDMX_Query.StructureComponentType getStructureComponent(XElement element)
        {
            SDMX_Query.StructureComponentType sc = new SDMX_Query.StructureComponentType();

            if(element != null)
            {
                if(element.Value != null)
                    sc.Value = element.Value;

                if(element.Attribute("id") != null)
                    sc.Id = element.Attribute("id").Value;
            }

            return sc;
        }

        

        
        #endregion

        #region WriteXmlFile (From Objects to Xml)

        /// <summary>
        /// This will write the SDMX_ML Query Message
        /// </summary>
        /// <returns>Return the SDMX_ML Query Message in a string format</returns>
        public string ToXml()
        {
            XElement x = GetXml();
            return _xmldoc.ToString();
        }

        public XDocument GetXmlDocument()
        {
            XElement x = GetXml();

            return _xmldoc;
        }

        /// <summary>
        /// This will write the SDMX_ML Query Message
        /// </summary>
        /// <returns>Return the SDMX_ML Query Message in a XElement format</returns>
        public XElement GetXml()
        {
            _xmldoc = new XDocument(
                new XDeclaration("1.0", "UTF-8", "yes"));
            
             XElement querymessage = new XElement(Namespaces.GetAllNamespaces("QueryMessage"));
            
            _header.Prepared = DateTime.Now.ToString("s") + "-01:00";
            _header.Extracted = DateTime.Now.ToString("s") + "-01:00";
            //_header.DataSetAction = SDMX_Message.Header.EnumDataSetAction.Information;
            
            querymessage.Add(_header.GetXml());

            XElement query = new XElement(Namespaces.GetNS("message") + "Query");

            //The Where elements can exist more than one time in a QueryMessage
            //so we loop the objects to create Where elements for each object
            if(_datawhere.Count > 0)
            {
                foreach(SDMX_Query.DataWhereType dw in _datawhere)
                    query.Add(writeDataWhere(dw));
            }
            else if(_codelistwhere.Count > 0)
            {
                foreach(SDMX_Query.CodelistWhereType cw in _codelistwhere)
                    query.Add(writeCodelistWhere(cw));    
            }
            else if(_keyfamilywhere.Count > 0)
            {
               foreach(SDMX_Query.KeyFamilyWhereType kw in _keyfamilywhere)
                    query.Add(writeKeyfamilyWhere(kw));
            }
            else if(_conceptwhere.Count > 0)
            {
                foreach(SDMX_Query.ConceptWhereType conw in _conceptwhere)
                    query.Add(writeConceptWhere(conw));
            }

            querymessage.Add(query);

            _xmldoc.Add(querymessage);
            
            return querymessage;
        }

        #region WriteWhereElements

        private XElement writeDataWhere(SDMX_Query.DataWhereType dw)
        {
            XElement query = new XElement(Namespaces.GetNS("query") + "DataWhere");
            
            if(dw.Dataset != null)
                query.Add(getElement(dw.Dataset, "DataSet"));
            else if(dw.Keyfamily != null)
                query.Add(getElement(dw.Keyfamily, "KeyFamily"));
            else if(dw.Dimensions != null)
                query.Add(getDimension(dw.Dimensions));
            else if(dw.Attributes != null)
                query.Add(getAttribute(dw.Attributes));
            else if(dw.Codelists != null)
                query.Add(getCodelist(dw.Codelists));
            else if(dw.Time != null)
                query.Add(getTime(dw.Time));
            else if (dw.Category != null)
                query.Add(getCategory(dw.Category));
            else if(dw.Concept != null)
                query.Add(getElement(dw.Concept, "Concept"));
            else if(dw.DataProvider != null)
                query.Add(getElement(dw.DataProvider, "DataProvider"));
            else if(dw.DataFlow != null)
                query.Add(getElement(dw.DataFlow, "Dataflow"));
            else if(dw.Version != null)
                query.Add(getElement(dw.Version, "Version"));
            else if(dw.Or != null)
                query.Add(addAndOr(dw.Or, "Or"));
            else if(dw.And != null)
                query.Add(addAndOr(dw.And, "And"));
            
            return query;
        }

        private XElement writeCodelistWhere(SDMX_Query.CodelistWhereType cw)
        {
            XElement query = new XElement(Namespaces.GetNS("query") + "CodelistWhere");

            if(cw.Codelist != null)
                query.Add(getCodelist(cw.Codelist));
            else if(cw.Agencyid != null)
                query.Add(getElement(cw.Agencyid, "AgencyID"));
            else if(cw.Version != null)
                query.Add(getElement(cw.Version, "Version"));
            else if(cw.Or != null)
                query.Add(addAndOr(cw.Or, "Or"));
            else if(cw.And != null)
                query.Add(addAndOr(cw.And, "And"));

            return query;
        }

        private XElement writeKeyfamilyWhere(SDMX_Query.KeyFamilyWhereType where)
        {
 
            XElement kfwhere = new XElement(Namespaces.GetNS("query") + "KeyFamilyWhere");

            if(where.Keyfamily != null)
                kfwhere.Add(getElement(where.Keyfamily, "KeyFamily"));
            else if(where.Dimension != null)
                kfwhere.Add(getDimension(where.Dimension));
            else if(where.Attribute != null)
                kfwhere.Add(getAttribute(where.Attribute));
            else if (where.Codelist != null)
                kfwhere.Add(getCodelist(where.Codelist));
            else if(where.Category != null)
                getCategory(where.Category);
            else if (where.AgencyID != null)
                kfwhere.Add(getElement(where.AgencyID, "AgencyID"));
            else if(where.Version != null)
                kfwhere.Add(getElement(where.Version, "Version"));
            else if(where.Or != null)
                kfwhere.Add(addAndOr(where.Or, "Or"));
            else if(where.And != null)
                kfwhere.Add(addAndOr(where.And, "And"));
            
            return kfwhere;
        }

        private XElement writeConceptWhere(SDMX_Query.ConceptWhereType where)
        {
            XElement query = new XElement(Namespaces.GetNS("query") + "ConceptWhere");

            if(where.Concept != null)
                query.Add(getElement(where.Concept, "Concept"));
            else if(where.AgencyID != null)
                query.Add(getElement(where.AgencyID, "AgencyID"));
            else if(where.Version != null)
                query.Add(getElement(where.Version, "Version"));
            else if(where.Or != null)
                query.Add(addAndOr(where.Or, "Or"));
            else if(where.And != null)
                query.Add(addAndOr(where.And, "And"));

            return query;
        }

        #endregion

        #region WriteElements

        private XElement addAndOr(SDMX_Abstract.AndOR andor, string crit)
        {

            XElement element = new XElement(Namespaces.GetNS("query") + crit);

            foreach(string ds in andor.Dataset)
                element.Add(getElement(ds, "Dataset"));

            foreach(string md in andor.Metadataset)
                element.Add(getElement(md, "MetadataSet"));

            foreach(string kf in andor.Keyfamily)
                element.Add(getElement(kf, "KeyFamily"));

            foreach(string ms in andor.Metadatastructure)
                element.Add(getElement(ms, "MetadataStructure"));

            foreach(SDMX_Query.DimensionType d in andor.Dimensions)
                element.Add(getDimension(d));

            foreach(SDMX_Query.StructureComponentType sc in andor.Structurecomponent)
                element.Add(getStructureComponent(sc));

            foreach(SDMX_Query.AttributeType ab in andor.Attributes)
                element.Add(getAttribute(ab));

            foreach(SDMX_Query.CodelistType cl in andor.Codelists)
                element.Add(getCodelist(cl));

            foreach(SDMX_Query.TimeType t in andor.Time)
                element.Add(getTime(t));

            foreach(SDMX_Query.CategoryType cat in andor.Category)
                element.Add(getCategory(cat));

            foreach(string con in andor.Concept)
                element.Add(getElement(con, "Concept"));

            foreach(string agencyid in andor.Agencyid)
                element.Add(getElement(agencyid, "AgencyID"));

            foreach(string p in andor.DataProvider)
                element.Add(getElement(p, "DataProvider"));

            foreach(string df in andor.DataFlow)
                element.Add(getElement(df, "Dataflow"));

            foreach(string mf in andor.MetadataFlow)
                element.Add(getElement(mf, "Metadataflow"));

            foreach(string version in andor.Version)
                element.Add(getElement(version, "Version"));

            foreach(SDMX_Abstract.AndOR or in andor.Or)
                element.Add(addAndOr(or, "Or"));

            foreach(SDMX_Abstract.AndOR and in andor.And)
                element.Add(addAndOr(and, "And"));

            return element;
        }

        /// <summary>
        /// Method that create an element on a string value
        /// </summary>
        /// <param name="val">The value that the element contain</param>
        /// <param name="name">The name of the element</param>
        /// <returns></returns>
        private XElement getElement(string val, string name)
        {
            XElement element = new XElement(Namespaces.GetNS("query") + name);

            if(val != null)
                element.Add(val);

            return element;
        }

        private XElement getStructureComponent(SDMX_Query.StructureComponentType sc)
        {
            XElement structurecomponent = new XElement(Namespaces.GetNS("query") + "StructureComponent");

            if(sc.Value != null)
                structurecomponent.Add(sc.Value);

            if(sc.Id != null)
                structurecomponent.Add(new XAttribute("id", sc.Id));

            return structurecomponent;
        }

        private XElement getDimension(SDMX_Query.DimensionType dim)
        {
            XElement dimension = new XElement(Namespaces.GetNS("query") + "Dimension");
            
            if(dim.Id != null)
                dimension.Add(new XAttribute("id", dim.Id));

            if(dim.Value != null)
                dimension.Value = dim.Value;

            return dimension;
        }

        private XElement getTime(SDMX_Query.TimeType t)
        {
            XElement time = new XElement(Namespaces.GetNS("query") + "Time");

            if(t.StartTime != null)
            {
                XElement start = new XElement(Namespaces.GetNS("query") + "StartTime", t.StartTime.TimePeriod);
                time.Add(start);
            }

            if(t.EndTime != null)
            {
                XElement end = new XElement(Namespaces.GetNS("query") + "EndTime", t.EndTime.TimePeriod);
                time.Add(end);
            }

            return time;
        }

         private XElement getCodelist(SDMX_Query.CodelistType cl)
        {
             XElement codelist = new XElement(Namespaces.GetNS("query") + "Codelist");

            if(cl.Id != null)
                codelist.Add(new XAttribute("id", cl.Id));

            return codelist;
        }

        private XElement getAttribute(SDMX_Query.AttributeType att)
        {
            XElement attribute = new XElement(Namespaces.GetNS("query") + "Attribute");

            if(att.Value != null)
                attribute.Add(att.Value);
            
            if(att.Id != null)
                attribute.Add(new XAttribute("id", att.Id));

            attribute.Add(new XAttribute("attachmentLevel", att.Attachmentlevel));

            return attribute;
        }

        private XElement getCategory(SDMX_Query.CategoryType cat)
        {
            XElement category = new XElement(Namespaces.GetNS("query") + "Category");

            if(cat.Value != null)
                category.Add(cat.Value);

            if(cat.ID != null)
                category.Add(new XAttribute("ID", cat.ID));

            if(cat.agencyID != null)
                category.Add(new XAttribute("agencyID", cat.agencyID));

            if(cat.CategoryScheme != null)
                category.Add(new XAttribute("CategoryScheme", cat.CategoryScheme));

            return category;
        }

        #endregion

        #endregion

        #region Properties

        /// <summary>
        /// You can add or get the DataWhere element that contain the query for datasets
        /// </summary>
        public List<SDMX_Query.DataWhereType> DataWhere
        {
            get{return _datawhere;}
            set { _datawhere = value; }
        }

        /// <summary>
        /// You can add or get the CodelistWhere element that contain the query for codelists
        /// </summary>
        public List<SDMX_Query.CodelistWhereType> CodelistWhere
        {
            get{return _codelistwhere;}
            set{_codelistwhere = value;}
        }

        /// <summary>
        /// You can add or get the ConceptWhere element that contain the query for concepts
        /// </summary>
        public List<SDMX_Query.ConceptWhereType> ConceptWhere
        {
            get{return _conceptwhere;}
            set{_conceptwhere = value;}
        }

        /// <summary>
        /// You can add or get the KeyFamilyWhere element that contain the query for keyfamilies
        /// </summary>
        public List<SDMX_Query.KeyFamilyWhereType> Keyfamilywhere
        {
            get { return _keyfamilywhere; }
            set { _keyfamilywhere = value; }
        }

        /// <summary>
        /// You can add or get the Header element that contain Information about sender and receiver
        /// </summary>
        public SDMX_Message.HeaderType Header
        {
            set{ _header = value;}
            get{return _header;}
        }

        #endregion
    }
}
