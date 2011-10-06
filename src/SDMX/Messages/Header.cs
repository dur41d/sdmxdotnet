using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{
    public class Header
    {
        public Id Id { get; set; }
        public bool Test { get; set; }
        public bool Truncated { get; set; }
        public InternationalText Name { get; private set; }
        public DateTimeOffset Prepared { get; set; }
        public IList<Party> Senders { get; private set; }
        public IList<Party> Receivers { get; private set; }
        public Id KeyFamilyId { get; set; }
        public Id KeyFamilyAgencyId { get; set; }
        public Id DataSetAgencyId { get; set; }
        public Id DataSetId { get; set; }
        public DataSetAction DataSetAction { get; set; }
        public DateTimeOffset? Extracted { get; set; }
        public DateTimeOffset? ReportingBegin { get; set; }
        public DateTimeOffset? ReportingEnd { get; set; }
        public InternationalText Source { get; private set; }

        public Header(Id id)
        {
            Id = id;
            Senders = new List<Party>();
            Receivers = new List<Party>();
            Source = new InternationalText();
            Name = new InternationalText();
        }

        public Header(Id id, Party sender)
            : this(id)
        {
            Senders.Add(sender);
        }

        public Header(Id id, Party sender, DateTime prepared)
            : this(id, sender)
        {
            Prepared = prepared;
        }
    }
}
