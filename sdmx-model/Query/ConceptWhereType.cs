using System;
using System.Collections.Generic;
using System.Text;

namespace SDMX_ML.Framework.Query
{
    /// <summary>
    /// The ConceptWhere element representes a query for a concept or 
    /// concepts. It contains all of the clauses in that query, represented 
    /// by its child elements. Values are the IDs of the referenced object
    /// </summary>
    public class ConceptWhereType
    {
        private string _concept;

        public string Concept
        {
            get { return _concept; }
            set { _concept = value; }
        }
        private string _agencyid;

        public string AgencyID
        {
            get { return _agencyid; }
            set { _agencyid = value; }
        }
        private string _version;

        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }
        private AndType _and;

        public AndType And
        {
            get { return _and; }
            set { _and = value; }
        }
        private OrType _or;

        public OrType Or
        {
            get { return _or; }
            set { _or = value; }
        }
    }
}
