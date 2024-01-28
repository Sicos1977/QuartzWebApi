using System;
using System.Collections.Specialized;
using System.Net;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin.Hosting;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Calendar;

namespace QuartzWebApi;

public static class CreateScheduler
{
    public static IScheduler Scheduler { get; }

    //public CreateScheduler(IScheduler scheduler)
    //{
    //    Scheduler = scheduler;
    //}

    static CreateScheduler()
    {
        string baseAddress = "http://localhost:9000/";

        using (WebApp.Start<Controllers.Scheduler>(url: baseAddress))
        {
            Console.WriteLine($"Server running at {baseAddress}");
            Console.ReadLine();
        }

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

        Scheduler.Context.Add("key1", "value1");
        Scheduler.Context.Add("key2", "value2");
        Scheduler.Context.Add("key3", "value3");

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

        var mc = new MonthlyCalendar();
        mc.SetDayExcluded(1, true);
        mc.SetDayExcluded(12, true);
        Scheduler.AddCalendar("monthlyCalendar", mc, true, true);

        Scheduler.ScheduleJob(schedulerJob, schedulerTrigger);
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