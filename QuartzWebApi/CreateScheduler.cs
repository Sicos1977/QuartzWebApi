using Quartz;

namespace QuartzWebApi
{
    public class CreateScheduler
    {
        public static IScheduler Scheduler { get; private set; }

        public CreateScheduler(IScheduler scheduler)
        {
            Scheduler = scheduler;
        }
    }
}