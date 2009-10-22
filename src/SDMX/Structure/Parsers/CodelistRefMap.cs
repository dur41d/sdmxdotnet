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
    public class CodelistRef
    {
        public CodelistRef()
        { }

        public CodelistRef(CodeList codelist)
        {
            ID = codelist.ID;
            Version = codelist.Version;
            AgencyID = codelist.AgencyID;
        }
        
        public ID ID { get; set; }
        public string Version { get; set; }
        public ID AgencyID { get; set; }
    }


    public class CodelistRefMap : AttributeGroupTypeMap<CodelistRef>
    {
        CodelistRef codelistRef = new CodelistRef();

        public CodelistRefMap()
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

        protected override CodelistRef Return()
        {
            return codelistRef;
        }
    }
}
