using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using System.Text.RegularExpressions;
using Common;

namespace SDMX.Parsers
{
    internal static class TimePeriodUtility
    {
        public static TimeSpan ParseTimeOffset(Match match)
        {
            var offset = new TimeSpan();
            if (match.Groups["ZoneSign"].Success)
            {
                int hours = int.Parse(match.Groups["ZoneHour"].Value);
                int minutes = int.Parse(match.Groups["ZoneMinute"].Value);

                if (match.Groups["ZoneSign"].Value == "-")
                {
                    hours *= -1;
                }
                offset = new TimeSpan(hours, minutes, 0);
            }

            return offset;
        }
    }
}
