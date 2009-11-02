using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using Common;

namespace SDMX.Parsers
{
    internal class HeaderMap : ClassMap<Header>
    {
        Header header;

        public HeaderMap()
        {
            Map(o => o.ID).ToSimpleElement("ID", true)
                .Set(v => header = new Header(v))
                .Converter(new IDConverter());

            Map(o => o.Test).ToSimpleElement("Test", false)
                .Set(v => header.Test = v)
                .Converter(new BooleanConverter());

            Map(o => o.Truncated).ToSimpleElement("Truncated", false)
                .Set(v => header.Truncated = v)
                .Converter(new BooleanConverter());

            MapCollection(o => o.Name).ToElement("Name", false)
                .Set(v => header.Name.Add(v))
                .ClassMap(() => new InternationalStringMap());

            Map(o => o.Prepared).ToSimpleElement("Prepared", true)
                .Set(v => header.Prepared = v)
                .Converter(new DateTimeConverter());

            MapCollection(o => o.Senders).ToElement("Sender", true)
                .Set(v => header.Senders.Add(v))
                .ClassMap(() => new PartyMap());

            MapCollection(o => o.Receivers).ToElement("Receiver", false)
                .Set(v => header.Receivers.Add(v))
                .ClassMap(() => new PartyMap());

            Map(o => o.KeyFamilyID).ToSimpleElement("KeyFamilyRef", false)
                .Set(v => header.KeyFamilyID = v)
                .Converter(new IDConverter());

            Map(o => o.KeyFamilyAgencyID).ToSimpleElement("KeyFamilyAgency", false)
                .Set(v => header.KeyFamilyAgencyID = v)
                .Converter(new IDConverter());

            Map(o => o.DataSetAgencyID).ToSimpleElement("DataSetAgency", false)
                .Set(v => header.DataSetAgencyID = v)
                .Converter(new IDConverter());

            Map(o => o.DataSetID).ToSimpleElement("DataSetID", false)
                .Set(v => header.DataSetID = v)
                .Converter(new IDConverter());

            Map<DataSetAction?>(o => o.DataSetAction == DataSetAction.None ? (DataSetAction?)null : o.DataSetAction)
                .ToSimpleElement("DataSetAction", false)
                .Set(v => header.DataSetAction = v.Value)
                .Converter(new EnumConverter<DataSetAction?>());

            Map(o => o.Extracted).ToSimpleElement("Extracted", false)
                .Set(v => header.Extracted = v)
                .Converter(new NullableDateTimeConverter());

            Map(o => o.ReportingBegin).ToSimpleElement("ReportingBegin", false)
               .Set(v => header.ReportingBegin = v)
               .Converter(new NullableDateTimeConverter());

            Map(o => o.ReportingEnd).ToSimpleElement("ReportingEnd", false)
               .Set(v => header.ReportingEnd = v)
               .Converter(new NullableDateTimeConverter());

            MapCollection(o => o.Source).ToElement("Source", false)
                .Set(v => header.Source.Add(v))
                .ClassMap(() => new InternationalStringMap());
        }

        protected override Header Return()
        {
            return header;
        }
    }
}
