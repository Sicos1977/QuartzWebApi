using System;
using System.Threading;
using Quartz;

namespace QuartzWebApi.Data
{
    public class RunningJobs : IJobExecutionContext
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
        public TriggerKey RecoveringTriggerKey { get; }
        public int RefireCount { get; }
        public JobDataMap MergedJobDataMap { get; }
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
    }
}