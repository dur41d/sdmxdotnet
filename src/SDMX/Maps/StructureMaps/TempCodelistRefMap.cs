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
    public class TempCodelistRef
    {
        public TempCodelistRef()
        { }

        public static TempCodelistRef Create(CodeList codelist)
        {
            if (codelist == null)
            {
                return null;
            }
            else
            {
                var codelistRef = new TempCodelistRef();
                codelistRef.ID = codelist.ID;
                codelistRef.Version = codelist.Version;
                codelistRef.AgencyID = codelist.AgencyID;

                return codelistRef;
            }          
        }      

        public ID ID { get; set; }
        public string Version { get; set; }
        public ID AgencyID { get; set; }
    }


    public class TempCodelistRefMap : AttributeGroupTypeMap<TempCodelistRef>
    {
        TempCodelistRef codelistRef = new TempCodelistRef();

        public TempCodelistRefMap()
        {
            MapAttribute(o => o.ID, "codelist", false)
                .Set(v => codelistRef.ID = v)
                .Converter(new IDConverter());

            MapAttribute(o => o.Version, "codelistVersion", false)
               .Set(v => codelistRef.Version = v)
               .Converter(new StringConverter());

            MapAttribute(o => o.AgencyID, "codelistAgency", false)
               .Set(v => codelistRef.AgencyID = v)
               .Converter(new IDConverter());
        }

        protected override TempCodelistRef Return()
        {
            if (codelistRef.ID == null)
                return null;

            return codelistRef;
        }
    }
}
