using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public class Header
    {
        public ID ID { get; set; }
        public bool Test { get; set; }
        public bool Truncated { get; set; }
        public InternationalText Name { get; private set; }
        public DateTime Prepared { get; set; }
        public IList<Party> Senders { get; private set; }
        public IList<Party> Receivers { get; private set; }
        public ID KeyFamilyID { get; set; }
        public ID KeyFamilyAgencyID { get; set; }
        public ID DataSetAgencyID { get; set; }
        public ID DataSetID { get; set; }
        public DataSetAction DataSetAction { get; set; }
        public DateTime? Extracted { get; set; }
        public DateTime? ReportingBegin { get; set; }
        public DateTime? ReportingEnd { get; set; }
        public InternationalText Source { get; private set; }

        public Header(ID id)
        {
            ID = id;
            Senders = new List<Party>();
            Receivers = new List<Party>();
            Source = new InternationalText();
            Name = new InternationalText();
        }

        public Header(ID id, Party sender)
            : this(id)
        {
            Senders.Add(sender);
        }
    }
}
