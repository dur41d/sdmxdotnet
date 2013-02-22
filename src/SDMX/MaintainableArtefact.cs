using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public abstract class MaintainableArtefact : VersionableArtefact, IEquatable<MaintainableArtefact>
    {
        public MaintainableArtefact(Id id, Id agencyId)
            : base(id)
        {
            AgencyId = agencyId;
        }
        
        public Id AgencyId { get; private set; }
        public bool IsFinal { get; set; }
        public bool IsExternalReference { get; set; }

        #region IEquatable<MaintainableArtefact> Members

        public bool Equals(MaintainableArtefact other)
        {
            return AgencyId.Equals(other.AgencyId)
                && Id.Equals(other.Id)
                && Version.Equals(other.Version);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is MaintainableArtefact)) return false;
            return base.Equals((MaintainableArtefact)obj);
        }

        public override int GetHashCode()
        {
            return 37
                ^ Id.GetHashCode()
                ^ AgencyId.GetHashCode()
                ^ Version.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0}.{1}[{2}]", AgencyId, Id, Version);
        }

        #endregion
    }
}
