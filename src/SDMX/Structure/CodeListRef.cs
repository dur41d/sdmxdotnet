using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace SDMX
{
    public class CodeListRef
    {
        public ID ID { get; set; }
        public ID AgencyID { get; set; }
        public string Version { get; set; }
        public ID Alias { get; set; }

        public CodeListRef()
        { }

        public CodeListRef(CodeList codeList, ID alias)
        {
            ID = codeList.ID;
            AgencyID = codeList.AgencyID;
            Version = codeList.Version;
            Alias = alias;
        }

        private readonly string UrnPrefix = "urn:sdmx:org.sdmx.infomodel.";

        public Uri Urn
        {
            get
            {
                return new Uri(string.Format("{0}.codelist={1}:{2}[{3}]".F(UrnPrefix, AgencyID, ID, Version)));
            }
        }

        public override string ToString()
        {
            return Alias;
        }

        public override bool Equals(object other)
        {
            return Equals(other as CodeListRef);
        }

        public bool Equals(CodeListRef other)
        {
            return ID.Equals(other.ID) &&
                AgencyID.Equals(other.AgencyID) &&
                Version.Equals(other.Version) &&
                Alias.Equals(other.Alias);
        }

        public static bool operator ==(CodeListRef a, CodeListRef b)
        {
            return Equals(a, b);
        }

        public static bool operator !=(CodeListRef a, CodeListRef b)
        {
            return !Equals(a, b);
        }  
    }
}
