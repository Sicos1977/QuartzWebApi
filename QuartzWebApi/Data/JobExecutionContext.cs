using System;
using System.Threading;
using Quartz;

namespace QuartzWebApi.Data
{
    public class JobExecutionContext : IJobExecutionContext
    {
        public void Put(object key, object objectValue)
        {
            throw new NotImplementedException();
        }

        public object Get(object key)
        {
            throw new NotImplementedException();
        }

        public IScheduler Scheduler { get; }
        public ITrigger Trigger { get; }
        public ICalendar Calendar { get; }
        public bool Recovering { get; }
        public Quartz.TriggerKey RecoveringTriggerKey { get; }
        public int RefireCount { get; }
        public Quartz.JobDataMap MergedJobDataMap { get; }
        public IJobDetail JobDetail { get; }
        public IJob JobInstance { get; }
        public DateTimeOffset FireTimeUtc { get; }
        public DateTimeOffset? ScheduledFireTimeUtc { get; }
        public DateTimeOffset? PreviousFireTimeUtc { get; }
        public DateTimeOffset? NextFireTimeUtc { get; }
        public string FireInstanceId { get; }
        public object Result { get; set; }
        public TimeSpan JobRunTime { get; }
        public CancellationToken CancellationToken { get; }

        //[
        //{
        //    "trigger": {
        //        "nextFireTimeUtc": null,
        //        "previousFireTimeUtc": "2022-05-19T19:06:44.8965919+02:00",
        //        "repeatCount": 0,
        //        "repeatInterval": "00:00:00",
        //        "timesTriggered": 1,
        //        "name": "triggerKey",
        //        "group": "DEFAULT",
        //        "jobName": "JobKeyName",
        //        "jobGroup": "JobKeyGroup",
        //        "jobDataMap": {},
        //        "misfireInstruction": 0,
        //        "endTimeUtc": null,
        //        "startTimeUtc": "2022-05-19T19:06:44.8965919+02:00",
        //        "<Description>k__BackingField": "TestTrigger",
        //        "<CalendarName>k__BackingField": null,
        //        "<FireInstanceId>k__BackingField": "637885768049105905",
        //        "<Priority>k__BackingField": 5
        //    },
        //    "jobDetail": {
        //        "name": "JobKeyName",
        //        "group": "JobKeyGroup",
        //        "description": "Test",
        //        "jobDataMap": {
        //            "Timer": "05/19/2022 19:08:35"
        //        },
        //        "jobType": "QuartzWebApi.TestJob, QuartzWebApi, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
        //        "<RequestsRecovery>k__BackingField": true,
        //        "<Durable>k__BackingField": false
        //    },
        //    "jobDataMap": { },
        //    "numRefires": 0,
        //    "jobRunTime": null,
        //    "<Calendar>k__BackingField": null,
        //    "<Recovering>k__BackingField": false,
        //    "<FireTimeUtc>k__BackingField": "2022-05-19T17:06:44.9135854+00:00",
        //    "<ScheduledFireTimeUtc>k__BackingField": "2022-05-19T19:06:44.8965919+02:00",
        //    "<PreviousFireTimeUtc>k__BackingField": null,
        //    "<NextFireTimeUtc>k__BackingField": null,
        //    "<Result>k__BackingField": null
        //}
        //]
    }
}