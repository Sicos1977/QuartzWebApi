using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;

namespace QuartzWebApi.Data
{
    public class AddCalender
    {
        public bool IsTimeIncluded(DateTimeOffset timeUtc)
        {
            throw new NotImplementedException();
        }

        public DateTimeOffset GetNextIncludedTimeUtc(DateTimeOffset timeUtc)
        {
            throw new NotImplementedException();
        }

        public ICalendar Clone()
        {
            throw new NotImplementedException();
        }

        public string Description { get; set; }
        public ICalendar CalendarBase { get; set; }
    }
}