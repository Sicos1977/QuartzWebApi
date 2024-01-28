﻿using System.Collections.Specialized;
using System.Net;
using ConsoleApp;
using Microsoft.Owin.Hosting;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Calendar;
using QuartzWebApi;

//var properties = new NameValueCollection
//{
//    ["quartz.scheduler.exporter.bindName"] = "Scheduler",
//    ["quartz.scheduler.exporter.channelName"] = "tcpQuartz",
//    ["quartz.scheduler.exporter.channelType"] = "tcp",
//    ["quartz.scheduler.exporter.port"] = "45000",
//    ["quartz.scheduler.exporter.type"] = "Quartz.Simpl.RemotingSchedulerExporter, Quartz",
//    ["quartz.scheduler.instanceName"] = Dns.GetHostEntry(Environment.MachineName).HostName.Replace(".", "_"),
//    ["quartz.jobStore.type"] = "Quartz.Simpl.RAMJobStore, Quartz",
//    ["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz"
//};

//var scheduler = new StdSchedulerFactory(properties).GetScheduler().Result;

//scheduler.Context.Add("key1", "value1");
//scheduler.Context.Add("key2", "value2");
//scheduler.Context.Add("key3", "value3");

//scheduler.Start();

//var job = new TestJob();
//var schedulerJob = JobBuilder.Create(job.GetType())
//    .WithIdentity(new JobKey("JobKeyName", "JobKeyGroup"))
//    .WithDescription("Test")
//    .RequestRecovery()
//    .Build();

//var schedulerTrigger = (ISimpleTrigger)TriggerBuilder.Create()
//    .WithIdentity("triggerKey")
//    .WithDescription("TestTrigger")
//    .StartAt(DateTime.Now)
//    .Build();

//var mc = new MonthlyCalendar();
//mc.SetDayExcluded(1, true);
//mc.SetDayExcluded(12, true);
//scheduler.AddCalendar("monthlyCalendar", mc, true, true);

//scheduler.ScheduleJob(schedulerJob, schedulerTrigger);

var baseAddress = "http://localhost:44344/";

using var app = WebApp.Start<Startup>(baseAddress);
Console.WriteLine($"Server running at {baseAddress}");
Console.ReadLine();