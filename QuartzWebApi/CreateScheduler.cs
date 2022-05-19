using System;
using System.Collections.Specialized;
using System.Net;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Quartz;
using Quartz.Impl;

namespace QuartzWebApi
{
    public static class CreateScheduler
    {
        public static IScheduler Scheduler { get; private set; }

        //public CreateScheduler(IScheduler scheduler)
        //{
        //    Scheduler = scheduler;
        //}

        static CreateScheduler()
        {
            var properties = new NameValueCollection
            {
                ["quartz.scheduler.exporter.bindName"] = "Scheduler",
                ["quartz.scheduler.exporter.channelName"] = "tcpQuartz",
                ["quartz.scheduler.exporter.channelType"] = "tcp",
                ["quartz.scheduler.exporter.port"] = "45000",
                ["quartz.scheduler.exporter.type"] = "Quartz.Simpl.RemotingSchedulerExporter, Quartz",
                ["quartz.scheduler.instanceName"] = Dns.GetHostEntry(Environment.MachineName).HostName.Replace(".", "_"),
                ["quartz.jobStore.type"] = "Quartz.Simpl.RAMJobStore, Quartz",
                ["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz"
            };

            Scheduler = new StdSchedulerFactory(properties).GetScheduler().Result;
            Scheduler.Start();

            var job = new TestJob();
            var schedulerJob = JobBuilder.Create(job.GetType())
                .WithIdentity(new JobKey("JobKeyName", "JobKeyGroup"))
                .WithDescription("Test")
                .RequestRecovery()
                .Build();

            var schedulerTrigger = (ISimpleTrigger)TriggerBuilder.Create()
                .WithIdentity("triggerKey")
                .WithDescription("TestTrigger")
                .StartAt(DateTime.Now)
                .Build();

            Scheduler.ScheduleJob(schedulerJob, schedulerTrigger);
        }

        private static void SetPrincipal(IPrincipal principal)
        {
            Thread.CurrentPrincipal = principal;

            if (HttpContext.Current != null)
                HttpContext.Current.User = principal;
        }

        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new AuthorizeAttribute());
        }
    }

    public class TestJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            while (true)
            {
                context.JobDetail.JobDataMap.PutAsString("Timer", DateTime.Now);
                Thread.Sleep(10000);
            }
        }
    }
}