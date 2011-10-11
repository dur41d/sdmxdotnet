using OXM;

namespace SDMX.Parsers
{
    internal class ITimeCriterionMap : ClassMap<ITimeCriterion>
    {
        string _time, _startTime, _endTime;

        public ITimeCriterionMap()
        {
            Map(o => o is TimeCriterion ? ((TimeCriterion)o).Time : null).ToSimpleElement("Time", false)
                .Set(v => _time = v)
                .Converter(new StringConverter());

            Map(o => o is TimePeriodCriterion ? ((TimePeriodCriterion)o).StartTime : null).ToSimpleElement("StartTime", false)
                .Set(v => _startTime = v)
                .Converter(new StringConverter());

            Map(o => o is TimePeriodCriterion ? ((TimePeriodCriterion)o).EndTime : null).ToSimpleElement("EndTime", false)
               .Set(v => _endTime = v)
               .Converter(new StringConverter());
        }

        protected override ITimeCriterion Return()
        {
            if (_time != null)
                return new TimeCriterion() { Time = _time };
            else
                return new TimePeriodCriterion() { StartTime = _startTime, EndTime = _endTime };
        }
    }
}
