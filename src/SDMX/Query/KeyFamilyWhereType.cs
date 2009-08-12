using System;
using System.Collections.Generic;
using System.Text;
using SDMX_ML.Framework.Structure;

namespace SDMX_ML.Framework.Query
{
    /// <summary>
    /// The KeyFamilyWhere element representes a query for a key family or 
    /// key families. It contains all of the clauses in that query, 
    /// represented by its child elements. Values are the IDs of the 
    /// referenced object.
    /// </summary>
    public class KeyFamilyWhereType
    {
        private string _keyfamily;
        private DimensionType _dimension;
        private string _concept;
        private AttributeType _attribute;
        private string _agencyid;
        private string _version;
        private CodelistType _codelist;
        private CategoryType _category;
        private AndType _and;
        private OrType _or;

        public KeyFamilyWhereType()
        {
        }

        public CategoryType Category
        {
            get { return _category; }
            set { _category = value; }
        }

        public OrType Or
        {
            get { return _or; }
            set { _or = value; }
        }

        public AndType And
        {
            get { return _and; }
            set { _and = value; }
        }

        public DimensionType Dimension
        {
            get { return _dimension; }
            set { _dimension = value; }
        }

        public CodelistType Codelist
        {
            get { return _codelist; }
            set { _codelist = value; }
        }

        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }

        public string AgencyID
        {
            get { return _agencyid; }
            set { _agencyid = value; }
        }

        public AttributeType Attribute
        {
            get { return _attribute; }
            set { _attribute = value; }
        }

        public string Concept
        {
            get { return _concept; }
            set { _concept = value; }
        }

        public string Keyfamily
        {
            get { return _keyfamily; }
            set { _keyfamily = value; }
        }


    }
}
