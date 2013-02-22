using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace SDMX
{
    public class CodeListRef : IEquatable<CodeListRef>
    {
        public Id Id { get; set; }
        public Id AgencyId { get; set; }
        public string Version { get; set; }
        public Id Alias { get; set; }

        public CodeListRef()
        { }

        public CodeListRef(CodeList codeList, Id alias)
        {
            Id = codeList.Id;
            AgencyId = codeList.AgencyId;
            Version = codeList.Version;
            Alias = alias;
        }

        private readonly string UrnPrefix = "urn:sdmx:org.sdmx.infomodel.";

        public Uri Urn
        {
            get
            {
                return new Uri(string.Format("{0}.codelist={1}:{2}[{3}]".F(UrnPrefix, AgencyId, Id, Version)));
            }
        }

        public override string ToString()
        {
            return Alias.ToString();
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object other)
        {            
            return Equals(other as CodeListRef);
        }

        public bool Equals(CodeListRef other)
        {
            if (other == null) return false;

            return Id.Equals(other.Id) &&
                AgencyId.Equals(other.AgencyId) &&
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
