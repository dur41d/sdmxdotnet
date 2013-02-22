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
                codelistRef.Id = codelist.Id;
                codelistRef.Version = codelist.Version;
                codelistRef.AgencyId = codelist.AgencyId;

                return codelistRef;
            }          
        }      

        public Id Id { get; set; }
        public string Version { get; set; }
        public Id AgencyId { get; set; }
    }


    public class TempCodelistRefMap : AttributeGroupTypeMap<TempCodelistRef>
    {
        TempCodelistRef codelistRef = new TempCodelistRef();

        public TempCodelistRefMap()
        {
            MapAttribute(o => o.Id, "codelist", false)
                .Set(v => codelistRef.Id = v)
                .Converter(new IdConverter());

            MapAttribute(o => o.Version, "codelistVersion", false)
               .Set(v => codelistRef.Version = v)
               .Converter(new StringConverter());

            MapAttribute(o => o.AgencyId, "codelistAgency", false)
               .Set(v => codelistRef.AgencyId = v)
               .Converter(new IdConverter());
        }

        protected override TempCodelistRef Return()
        {
            if (codelistRef.Id == null)
                return null;

            return codelistRef;
        }
    }
}
