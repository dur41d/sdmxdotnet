using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public abstract class MaintainableArtefact : VersionableArtefact, IEquatable<MaintainableArtefact>
    {
        public MaintainableArtefact(ID id, ID agencyID)
            : base(id)
        {
            AgencyID = agencyID;
        }
        
        public ID AgencyID { get; private set; }
        public bool IsFinal { get; set; }
        public bool IsExternalReference { get; set; }

        #region IEquatable<MaintainableArtefact> Members

        public bool Equals(MaintainableArtefact other)
        {
            return AgencyID.Equals(other.AgencyID)
                && ID.Equals(other.ID)
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
                ^ ID.GetHashCode()
                ^ AgencyID.GetHashCode()
                ^ Version.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0}.{1}[{2}]", AgencyID, ID, Version);
        }

        #endregion
    }
}
