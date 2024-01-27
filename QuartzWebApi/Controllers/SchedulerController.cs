﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Spi;
using QuartzWebApi.Data;
using QuartzWebApi.Data.Calendars;
using JobKey = QuartzWebApi.Data.JobKey;
using SchedulerMetaData = QuartzWebApi.Data.SchedulerMetaData;
using TriggerKey = QuartzWebApi.Data.TriggerKey;

namespace QuartzWebApi.Controllers;

//[Authorize]
public class SchedulerController : ApiController
{
    #region Fields
    /// <summary>
    ///     When set then logging is written to this ILogger instance
    /// </summary>
    private readonly ILogger _logger;
    #endregion

    #region IsJobGroupPaused
    /// <summary>
    ///     Returns <c>true</c> if the given JobGroup is paused
    /// </summary>
    /// <param name="groupName">The group name</param>
    /// <returns></returns>
    [HttpGet]
    [Route("scheduler/isjobgrouppaused/{groupName}")]
    public Task<bool> IsJobGroupPaused(string groupName)
    {
        _logger?.LogInformation($"Received request to check if the job group '{groupName}' is paused");
        var result = CreateScheduler.Scheduler.IsJobGroupPaused(groupName);
        _logger?.LogInformation($"Returning '{result}'");
        return result;
    }
    #endregion

    #region IsTriggerGroupPaused
    /// <summary>
    ///     Returns <c>true</c> if the given TriggerGroup is paused
    /// </summary>
    /// <param name="groupName">The group name</param>
    /// <returns></returns>
    [HttpGet]
    [Route("scheduler/istriggergrouppaused/{groupName}")]
    public Task<bool> IsTriggerGroupPaused(string groupName)
    {
        _logger?.LogInformation($"Received request to check if the trigger group '{groupName}' is paused");
        var result = CreateScheduler.Scheduler.IsTriggerGroupPaused(groupName);
        _logger?.LogInformation($"Returning '{result}'");
        return result;
    }
    #endregion

    #region SchedulerName
    /// <summary>
    ///     Returns the name of the scheduler
    /// </summary>
    [HttpGet]
    [Route("scheduler/schedulername")]
    public string SchedulerName()
    {
        _logger?.LogInformation("Received request to return the scheduler name");
        var result = CreateScheduler.Scheduler.SchedulerName;
        _logger?.LogInformation($"Returning '{result}'");
        return result;
    }
    #endregion

    #region SchedulerInstanceId
    /// <summary>
    ///     Returns the instance id of the scheduler
    /// </summary>
    [HttpGet]
    [Route("scheduler/schedulerinstanceid")]
    public string SchedulerInstanceId()
    {
        _logger?.LogInformation("Received request to return the scheduler instance id");
        var result = CreateScheduler.Scheduler.SchedulerInstanceId;
        _logger?.LogInformation($"Returning '{result}'");
        return result;
    }
    #endregion

    #region Context
    /// <summary>
    ///     Returns the <see cref="SchedulerContext" /> of the <see cref="IScheduler" />.
    /// </summary>
    [HttpGet]
    [Route("scheduler/schedulercontext")]
    public SchedulerContext Context()
    {
        _logger?.LogInformation("Received request to return the scheduler context");
        var result = CreateScheduler.Scheduler.Context;
        _logger?.LogDebug($"Returning '{result}'");
        return result;
    }
    #endregion

    #region InStandbyMode
    /// <summary>
    ///     Reports whether the <see cref="IScheduler" /> is in stand-by mode.
    /// </summary>
    /// <seealso cref="Standby" />
    /// <seealso cref="Start" />
    [HttpGet]
    [Route("scheduler/instandbymode")]
    public bool InStandbyMode()
    {
        _logger?.LogInformation("Received request to check if the scheduler is in standby mode");
        var result = CreateScheduler.Scheduler.InStandbyMode;
        _logger?.LogInformation($"Returning '{result}'");
        return result;
    }
    #endregion

    #region IsShutdown
    /// <summary>
    ///     Reports whether the <see cref="IScheduler" /> has been Shutdown.
    /// </summary>
    [HttpGet]
    [Route("scheduler/isshutdown")]
    public bool IsShutdown()
    {
        _logger?.LogInformation("Received request to check if the scheduler is shutdown");
        var result = CreateScheduler.Scheduler.IsShutdown;
        _logger?.LogInformation($"Returning '{result}'");
        return result;
    }
    #endregion

    #region GetMetaData
    /// <summary>
    ///     Get a <see cref="Quartz.SchedulerMetaData" /> object describing the settings
    ///     and capabilities of the scheduler instance.
    /// </summary>
    /// <remarks>
    ///     Note that the data returned is an 'instantaneous' snapshot, and that as
    ///     soon as it's returned, the meta-data values may be different.
    /// </remarks>
    [HttpGet]
    [Route("scheduler/getmetadata")]
    public string GetMetaData()
    {
        _logger?.LogInformation("Received request to return the meta-data");
        var result = new SchedulerMetaData(CreateScheduler.Scheduler.GetMetaData().Result).ToJsonString();
        _logger?.LogInformation($"Returning '{result}'");
        return result;
    }
    #endregion

    #region GetCurrentlyExecutingJobs
    /// <summary>
    ///     Return a list of <see cref="IJobExecutionContext" /> objects that
    ///     represent all currently executing Jobs in this Scheduler instance.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This method is not cluster aware.  That is, it will only return Jobs
    ///         currently executing in this Scheduler instance, not across the entire
    ///         cluster.
    ///     </para>
    ///     <para>
    ///         Note that the list returned is an 'instantaneous' snapshot, and that as
    ///         soon as it's returned, the true list of executing jobs may be different.
    ///         Also, please read the doc associated with <see cref="IJobExecutionContext" />-
    ///         especially if you're using remoting.
    ///     </para>
    /// </remarks>
    /// <seealso cref="IJobExecutionContext" />
    [HttpGet]
    [Route("scheduler/getcurrentlyexecutingjobs")]
    public string GetCurrentlyExecutingJobs()
    {
        _logger?.LogInformation("Received request to return the currently executing jobs");

        var jobExecutionContext = CreateScheduler.Scheduler.GetCurrentlyExecutingJobs().GetAwaiter().GetResult();
        var result = new JobExecutionContexts(jobExecutionContext).ToJsonString();

        _logger?.LogInformation("Returning currently executing jobs");
        _logger?.LogDebug($"JSON '{result}'");
        return result;
    }
    #endregion

    #region GetJobGroupNames
    /// <summary>
    ///     Get the names of all known <see cref="IJobDetail" /> groups.
    /// </summary>
    [HttpGet]
    [Route("scheduler/getjobgroupnames")]
    public Task<IReadOnlyCollection<string>> GetJobGroupNames()
    {
        _logger?.LogInformation("Received request to return the job group names");

        var result = CreateScheduler.Scheduler.GetJobGroupNames();

        _logger?.LogInformation("Returning job group names");
        _logger?.LogDebug($"JSON '{result}'");
        return result;
    }
    #endregion

    #region GetTriggerGroupNames
    /// <summary>
    ///     Get the names of all known <see cref="ITrigger" /> groups.
    /// </summary>
    [HttpGet]
    [Route("scheduler/gettriggergroupnames")]
    public Task<IReadOnlyCollection<string>> GetTriggerGroupNames()
    {
        _logger?.LogInformation("Received request to return the trigger group names");

        var result = CreateScheduler.Scheduler.GetTriggerGroupNames();

        _logger?.LogInformation("Returning trigger group names");
        _logger?.LogDebug($"JSON '{result}'");
        return result;
    }
    #endregion

    #region GetPausedTriggerGroups
    /// <summary>
    ///     Get the names of all <see cref="ITrigger" /> groups that are paused.
    /// </summary>
    [HttpGet]
    [Route("scheduler/getpausedtriggergroups")]
    public Task<IReadOnlyCollection<string>> GetPausedTriggerGroups()
    {
        _logger?.LogInformation("Received request to return the paused trigger groups");

        var result = CreateScheduler.Scheduler.GetPausedTriggerGroups();

        _logger?.LogInformation("Returning paused trigger groups");
        _logger?.LogDebug($"JSON '{result}'");
        return result;
    }
    #endregion

    #region Start
    /// <summary>
    ///     Starts the <see cref="IScheduler" />'s threads that fire <see cref="ITrigger" />s.
    ///     When a scheduler is first created it is in "stand-by" mode, and will not
    ///     fire triggers.  The scheduler can also be put into stand-by mode by
    ///     calling the <see cref="Standby" /> method.
    /// </summary>
    /// <remarks>
    ///     The misfire/recovery process will be started, if it is the initial call
    ///     to this method on this scheduler instance.
    /// </remarks>
    /// <seealso cref="StartDelayed" />
    /// <seealso cref="Standby" />
    /// <seealso cref="Shutdown(bool)" />
    [HttpPost]
    [Route("scheduler/start")]
    public Task Start()
    {
        _logger?.LogInformation("Received request to start the scheduler");

        var result = CreateScheduler.Scheduler.Start();

        _logger?.LogInformation("The scheduler is started");
        return result;
    }
    #endregion

    #region StartDelayed
    /// <summary>
    ///     Calls <see cref="Start" /> after the indicated delay.
    ///     (This call does not block). This can be useful within applications that
    ///     have initializers that create the scheduler immediately, before the
    ///     resources needed by the executing jobs have been fully initialized.
    /// </summary>
    /// <seealso cref="Start" />
    /// <seealso cref="Standby" />
    /// <seealso cref="Shutdown(bool)" />
    [HttpPost]
    [Route("scheduler/startdelayed/{delay}")]
    public Task StartDelayed(int delay)
    {
        _logger?.LogInformation($"Received request to start the scheduler with a delay of '{delay}' minutes");

        var result = CreateScheduler.Scheduler.StartDelayed(new TimeSpan(0, 0, delay));

        _logger?.LogInformation("The scheduler is started");
        return result;
    }
    #endregion

    #region IsStarted
    /// <summary>
    ///     Whether the scheduler has been started.
    /// </summary>
    /// <remarks>
    ///     Note: This only reflects whether <see cref="Start" /> has ever
    ///     been called on this Scheduler, so it will return <see langword="true" /> even
    ///     if the <see cref="IScheduler" /> is currently in standby mode or has been
    ///     since shutdown.
    /// </remarks>
    /// <seealso cref="Start" />
    /// <seealso cref="IsShutdown" />
    /// <seealso cref="InStandbyMode" />
    [HttpGet]
    [Route("scheduler/isstarted")]
    public bool IsStarted()
    {
        _logger?.LogInformation("Received request to check if the scheduler is started");

        var result = CreateScheduler.Scheduler.IsStarted;

        _logger?.LogInformation($"Returning '{result}'");
        return result;
    }
    #endregion

    #region Standby
    /// <summary>
    ///     Temporarily halts the <see cref="IScheduler" />'s firing of <see cref="ITrigger" />s.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         When <see cref="Start" /> is called (to bring the scheduler out of
    ///         stand-by mode), trigger misfire instructions will NOT be applied
    ///         during the execution of the <see cref="Start" /> method - any misfires
    ///         will be detected immediately afterward (by the <see cref="IJobStore" />'s
    ///         normal process).
    ///     </para>
    ///     <para>
    ///         The scheduler is not destroyed, and can be re-started at any time.
    ///     </para>
    /// </remarks>
    /// <seealso cref="Start" />
    /// <seealso cref="PauseAll" />
    [HttpPost]
    [Route("scheduler/standby")]
    public Task Standby()
    {
        _logger?.LogInformation("Received request to put the scheduler in standby mode");

        var result = CreateScheduler.Scheduler.Standby();

        _logger?.LogInformation("The scheduler is in standby mode");
        return result;
    }
    #endregion

    #region Shutdown
    /// <summary>
    ///     Halts the <see cref="IScheduler" />'s firing of <see cref="ITrigger" />s,
    ///     and cleans up all resources associated with the Scheduler. Equivalent to <see cref="Shutdown(bool)" />.
    /// </summary>
    /// <remarks>
    ///     The scheduler cannot be re-started.
    /// </remarks>
    /// <seealso cref="Shutdown(bool)" />
    [HttpPost]
    [Route("scheduler/shutdown")]
    public Task Shutdown()
    {
        _logger?.LogInformation("Received request to shutdown the scheduler");

        var result = CreateScheduler.Scheduler.Shutdown();

        _logger?.LogInformation("The scheduler is shutdown");
        return result;
    }

    /// <summary>
    ///     Halts the <see cref="IScheduler" />'s firing of <see cref="ITrigger" />s,
    ///     and cleans up all resources associated with the Scheduler.
    /// </summary>
    /// <remarks>
    ///     The scheduler cannot be re-started.
    /// </remarks>
    /// <param name="waitForJobsToComplete">
    ///     if <see langword="true" /> the scheduler will not allow this method
    ///     to return until all currently executing jobs have completed.
    /// </param>
    /// <seealso cref="Shutdown()" />
    [HttpPost]
    [Route("scheduler/shutdown/{waitForJobsToComplete}")]
    public Task Shutdown(bool waitForJobsToComplete)
    {
        _logger?.LogInformation(waitForJobsToComplete
            ? "Received request to shutdown the scheduler but wait the the jobs to complete first"
            : "Received request to shutdown the scheduler and not to wait for the jobs to complete first");

        var result = CreateScheduler.Scheduler.Shutdown(waitForJobsToComplete);

        _logger?.LogInformation("The scheduler is shutdown");
        return result;
    }
    #endregion

    #region ScheduleJob
    /// <summary>
    ///     Add the given <see cref="IJobDetail" /> to the
    ///     Scheduler, and associate the given <see cref="ITrigger" /> with
    ///     it.
    /// </summary>
    /// <remarks>
    ///     If the given Trigger does not reference any <see cref="IJob" />, then it
    ///     will be set to reference the Job passed with it into this method.
    /// </remarks>
    [HttpPost]
    [Route("scheduler/schedulejobwithjobdetailandtrigger")]
    public Task<DateTimeOffset> ScheduleJob([FromBody] string json)
    {
        _logger?.LogInformation("Received request to schedule a job with details and a trigger");
        _logger?.LogDebug($"Received JSON '{json}'");

        var jobDetailWithTrigger = JobDetailWithTrigger.FromJsonString(json);
        var result = CreateScheduler.Scheduler.ScheduleJob(
            jobDetailWithTrigger.JobDetail.ToJobDetail(),
            jobDetailWithTrigger.Trigger.ToTrigger());

        _logger?.LogDebug($"Job scheduled, returning '{result}'");
        return result;
    }
    #endregion

    #region ScheduleJobIdentifiedWithTrigger
    /// <summary>
    ///     Schedule the given <see cref="ITrigger" /> with the <see cref="IJob" /> identified by the <see cref="ITrigger" />'s
    ///     settings.
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("scheduler/schedulejobidentifiedwithtrigger")]
    public Task<DateTimeOffset> ScheduleJobIdentifiedWithTrigger([FromBody] string json)
    {
        _logger?.LogInformation("Received request to schedule a job identified by a trigger");
        _logger?.LogDebug($"Received JSON '{json}'");

        var trigger = Trigger.FromJsonString(json).ToTrigger();
        var result = CreateScheduler.Scheduler.ScheduleJob(trigger);

        _logger?.LogDebug($"Job scheduled, returning '{result}'");
        return result;
    }
    #endregion

    ///// <summary>
    ///// Schedule all the given jobs with the related set of triggers.
    ///// </summary>
    ///// <remarks>
    ///// <para>If any of the given jobs or triggers already exist (or more
    ///// specifically, if the keys are not unique) and the replace
    ///// parameter is not set to true then an exception will be thrown.</para>
    ///// </remarks>
    //[HttpPost]
    //[Route("scheduler/schedulejobs")]
    //public Task ScheduleJobs([FromBody] string json)
    //{
    //    // IReadOnlyDictionary<IJobDetail, IReadOnlyCollection<ITrigger>> triggersAndJobs, bool replace
    //    var triggerAndJobs
    //    return CreateScheduler.Scheduler.ScheduleJobs(trigger);
    //}

    #region ScheduleJobWithTriggers
    /// <summary>
    ///     Schedule the given job with the related set of triggers.
    /// </summary>
    /// <remarks>
    ///     If any of the given job or triggers already exist (or more
    ///     specifically, if the keys are not unique) and the replace
    ///     parameter is not set to true then an exception will be thrown.
    /// </remarks>
    [HttpPost]
    [Route("scheduler/schedulejobwithtriggers")]
    public Task ScheduleJobWithTriggers([FromBody] string json)
    {
        _logger?.LogInformation("Received request to schedule a job with triggers");
        _logger?.LogDebug($"Received JSON '{json}'");

        var jobDetailWithTriggers = JobDetailWithTriggers.FromJsonString(json);
        var result = CreateScheduler.Scheduler.ScheduleJob(
            jobDetailWithTriggers.JobDetail.ToJobDetail(),
            jobDetailWithTriggers.ToReadOnlyTriggerCollection(),
            jobDetailWithTriggers.Replace);

        _logger?.LogDebug("Job scheduled");
        return result;
    }
    #endregion

    #region UnscheduleJob
    /// <summary>
    ///     Remove the indicated <see cref="ITrigger" /> from the scheduler.
    ///     <para>
    ///         If the related job does not have any other triggers, and the job is
    ///         not durable, then the job will also be deleted.
    ///     </para>
    /// </summary>
    [HttpPost]
    [Route("scheduler/unschedulejob")]
    public Task<bool> UnscheduleJob([FromBody] string json)
    {
        _logger?.LogInformation("Received request to unschedule a job that matches the trigger");
        _logger?.LogDebug($"Received JSON '{json}'");

        var result = CreateScheduler.Scheduler.UnscheduleJob(TriggerKey.FromJsonString(json).ToTriggerKey());

        _logger?.LogInformation($"Job that matches the trigger unscheduled, returning '{result}'");
        return result;
    }
    #endregion

    #region UnscheduleJobs
    /// <summary>
    ///     Remove all the indicated <see cref="ITrigger" />s from the scheduler.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         If the related job does not have any other triggers, and the job is
    ///         not durable, then the job will also be deleted.
    ///     </para>
    ///     Note that while this bulk operation is likely more efficient than
    ///     invoking <see cref="UnscheduleJob" /> several times, it may have the
    ///     adverse affect of holding data locks for a single long duration of time
    ///     (rather than lots of small durations of time).
    /// </remarks>
    [HttpPost]
    [Route("scheduler/unschedulejobs")]
    public Task<bool> UnscheduleJobs([FromBody] string json)
    {
        _logger?.LogInformation("Received request to unschedule all the jobs that match the given triggers");
        _logger?.LogDebug($"Received JSON '{json}'");

        var result = CreateScheduler.Scheduler.UnscheduleJobs(TriggerKeys.FromJsonString(json).ToTriggerKeys());

        _logger?.LogInformation($"Jobs unscheduled that are matching the given triggers, returning '{result}'");
        return result;
    }
    #endregion

    #region RescheduleJob
    /// <summary>
    ///     Remove (delete) the <see cref="ITrigger" /> with the given key, and store the
    ///     new given one - which must be associated with the same job (the new trigger must
    ///     have the job name &amp; group specified)  - however, the new trigger need not have
    ///     the same name as the old trigger.
    /// </summary>
    /// <param name="json">The json</param>
    /// <returns>
    ///     <see langword="null" /> if a <see cref="ITrigger" /> with the given name and group
    ///     was not found and removed from the store (and the  new trigger is therefore not stored),
    ///     otherwise the first fire time of the newly scheduled trigger.
    /// </returns>
    [HttpPost]
    [Route("scheduler/reschedulejob")]
    public Task<DateTimeOffset?> RescheduleJob([FromBody] string json)
    {
        _logger?.LogInformation("Received request to reschedule the job that match the given trigger key");
        _logger?.LogDebug($"Received JSON '{json}'");

        var rescheduleJob = Data.RescheduleJob.FromJsonString(json);
        var result = CreateScheduler.Scheduler.RescheduleJob(rescheduleJob.CurrentTriggerKey.ToTriggerKey(),
            rescheduleJob.Trigger.ToTrigger());

        _logger?.LogInformation($"Job rescheduled that matches the given trigger key, returning '{result}'");
        return result;
    }
    #endregion

    #region AddJob
    /// <summary>
    ///     Add the given <see cref="IJob" /> to the Scheduler - with no associated
    ///     <see cref="ITrigger" />. The <see cref="IJob" /> will be 'dormant' until
    ///     it is scheduled with a <see cref="ITrigger" />, or TriggerJob /> is called for it.
    /// </summary>
    /// <remarks>
    ///     With the <see cref="Data.JobDetail.StoreNonDurableWhileAwaitingScheduling" /> parameter
    ///     set to <code>true</code>, a non-durable job can be stored.  Once it is
    ///     scheduled, it will resume normal non-durable behavior (i.e. be deleted
    ///     once there are no remaining associated triggers).
    /// </remarks>
    [HttpPost]
    [Route("scheduler/addjob")]
    public Task AddJob([FromBody] string json)
    {
        _logger?.LogInformation("Received request to add job to the scheduler");
        _logger?.LogDebug($"Received JSON '{json}'");

        var addJob = JobDetail.FromJsonString(json);
        var result = CreateScheduler.Scheduler.AddJob(addJob.ToJobDetail(), addJob.Replace,
            addJob.StoreNonDurableWhileAwaitingScheduling);

        _logger?.LogInformation("Job added to the scheduler");
        return result;
    }
    #endregion

    #region DeleteJob
    /// <summary>
    ///     Delete the identified <see cref="IJob" /> from the Scheduler - and any
    ///     associated <see cref="ITrigger" />s.
    /// </summary>
    /// <returns> true if the Job was found and deleted.</returns>
    [HttpPost]
    [Route("scheduler/deletejob")]
    public Task<bool> DeleteJob([FromBody] string json)
    {
        _logger?.LogInformation("Received request to delete a job that matches the given job key");
        _logger?.LogDebug($"Received JSON '{json}'");

        var jobKey = JobKey.FromJsonString(json);
        var result = CreateScheduler.Scheduler.DeleteJob(jobKey.ToJobKey());

        _logger?.LogInformation($"Removed job from the scheduler, result '{result}'");
        return result;
    }
    #endregion

    #region DeleteJobs
    /// <summary>
    ///     Delete the identified jobs from the Scheduler - and any
    ///     associated <see cref="ITrigger" />s.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Note that while this bulk operation is likely more efficient than
    ///         invoking <see cref="DeleteJob" /> several
    ///         times, it may have the adverse affect of holding data locks for a
    ///         single long duration of time (rather than lots of small durations
    ///         of time).
    ///     </para>
    /// </remarks>
    /// <returns>
    ///     true if all the Jobs were found and deleted, false if
    ///     one or more were not deleted.
    /// </returns>
    [HttpPost]
    [Route("scheduler/deletejobs")]
    public Task<bool> DeleteJobs([FromBody] string json)
    {
        _logger?.LogInformation("Received request to delete jobs that are matching the given job keys");
        _logger?.LogDebug($"Received JSON '{json}'");

        var jobKeys = JobKeys.FromJsonString(json).ToJobKeys();
        var result = CreateScheduler.Scheduler.DeleteJobs(jobKeys);

        _logger?.LogInformation($"Removed jobs from the scheduler, result '{result}'");
        return result;
    }
    #endregion

    #region TriggerJobWithJobKey
    /// <summary>
    ///     Trigger the identified <see cref="IJobDetail" /> (Execute it now).
    /// </summary>
    [HttpPost]
    [Route("scheduler/triggerjobwithjobkey")]
    public Task TriggerJobWithJobKey([FromBody] string json)
    {
        _logger?.LogInformation("Received request to trigger a job that matches the given job key");
        _logger?.LogDebug($"Received JSON '{json}'");

        var jobKey = JobKey.FromJsonString(json);
        var result = CreateScheduler.Scheduler.TriggerJob(jobKey.ToJobKey());

        _logger?.LogInformation("Job triggered");
        return result;
    }
    #endregion

    #region TriggerJobWithDataMap
    /// <summary>
    ///     Trigger the identified <see cref="IJobDetail" /> (Execute it now).
    /// </summary>
    /// <param name="json">
    ///     The <see cref="JobKey" /> with associated <see cref="Quartz.JobDataMap" /> of the <see cref="IJob" /> to be
    ///     executed.
    /// </param>
    [HttpPost]
    [Route("scheduler/triggerjobwithdatamap")]
    public Task TriggerJobWithDataMap([FromBody] string json)
    {
        _logger?.LogInformation("Received request to trigger a job with data map that matches the given job key");
        _logger?.LogDebug($"Received JSON '{json}'");

        var jobKeyWithDataMap = JobKeyWithDataMap.FromJsonString(json);
        var result =
            CreateScheduler.Scheduler.TriggerJob(jobKeyWithDataMap.JobKey.ToJobKey(), jobKeyWithDataMap.JobDataMap);

        _logger?.LogInformation("Job triggered");
        return result;
    }
    #endregion

    #region PauseJob
    /// <summary>
    ///     Pause the <see cref="IJobDetail" /> with the given
    ///     key - by pausing all of its current <see cref="ITrigger" />s.
    /// </summary>
    [HttpPost]
    [Route("scheduler/pausejob")]
    public Task PauseJob([FromBody] string json)
    {
        _logger?.LogInformation("Received request to pause a job that matches the given job key");
        _logger?.LogDebug($"Received JSON '{json}'");

        var jobKey = JobKey.FromJsonString(json);
        var result = CreateScheduler.Scheduler.PauseJob(jobKey.ToJobKey());

        _logger?.LogInformation("Job paused");
        return result;
    }
    #endregion

    #region PauseJobs
    /// <summary>
    ///     Pause all the <see cref="IJobDetail" />s in the
    ///     matching groups - by pausing all of their <see cref="ITrigger" />s.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         The Scheduler will "remember" that the groups are paused, and impose the
    ///         pause on any new jobs that are added to any of those groups until it is resumed.
    ///     </para>
    ///     <para>
    ///         NOTE: There is a limitation that only exactly matched groups
    ///         can be remembered as paused. For example, if there are pre-existing
    ///         job in groups "aaa" and "bbb" and a matcher is given to pause
    ///         groups that start with "a" then the group "aaa" will be remembered
    ///         as paused and any subsequently added jobs in group "aaa" will be paused,
    ///         however if a job is added to group "axx" it will not be paused,
    ///         as "axx" wasn't known at the time the "group starts with a" matcher
    ///         was applied.  HOWEVER, if there are pre-existing groups "aaa" and
    ///         "bbb" and a matcher is given to pause the group "axx" (with a
    ///         group equals matcher) then no jobs will be paused, but it will be
    ///         remembered that group "axx" is paused and later when a job is added
    ///         in that group, it will become paused.
    ///     </para>
    /// </remarks>
    /// <seealso cref="ResumeJobs" />
    [HttpPost]
    [Route("scheduler/pausejobs")]
    public Task PauseJobs([FromBody] string json)
    {
        _logger?.LogInformation("Received request to pause all jobs that are matching the given group matcher");
        _logger?.LogDebug($"Received JSON '{json}'");

        var groupMatcher = GroupMatcher<Quartz.JobKey>.FromJsonString(json);
        var result = CreateScheduler.Scheduler.PauseJobs(groupMatcher.ToGroupMatcher());

        _logger?.LogInformation("Jobs paused");
        return result;
    }
    #endregion

    #region PauseTrigger
    /// <summary>
    ///     Pause the <see cref="ITrigger" /> with the given key.
    /// </summary>
    [HttpPost]
    [Route("scheduler/pausetrigger")]
    public Task PauseTrigger([FromBody] string json)
    {
        _logger?.LogInformation("Received request to pause a trigger that matches the given trigger key");
        _logger?.LogDebug($"Received JSON '{json}'");

        var triggerKey = TriggerKey.FromJsonString(json);
        var result = CreateScheduler.Scheduler.PauseTrigger(triggerKey.ToTriggerKey());

        _logger?.LogInformation("Trigger paused");
        return result;
    }
    #endregion

    #region PauseTriggers
    /// <summary>
    ///     Pause all the <see cref="ITrigger" />s in the groups matching.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         The Scheduler will "remember" all the groups paused, and impose the
    ///         pause on any new triggers that are added to any of those groups until it is resumed.
    ///     </para>
    ///     <para>
    ///         NOTE: There is a limitation that only exactly matched groups
    ///         can be remembered as paused.  For example, if there are pre-existing
    ///         triggers in groups "aaa" and "bbb" and a matcher is given to pause
    ///         groups that start with "a" then the group "aaa" will be remembered as
    ///         paused and any subsequently added triggers in that group be paused,
    ///         however if a trigger is added to group "axx" it will not be paused,
    ///         as "axx" wasn't known at the time the "group starts with a" matcher
    ///         was applied.  HOWEVER, if there are pre-existing groups "aaa" and
    ///         "bbb" and a matcher is given to pause the group "axx" (with a
    ///         group equals matcher) then no triggers will be paused, but it will be
    ///         remembered that group "axx" is paused and later when a trigger is added
    ///         in that group, it will become paused.
    ///     </para>
    /// </remarks>
    /// <seealso cref="ResumeTriggers" />
    [HttpPost]
    [Route("scheduler/pausetriggers")]
    public Task PauseTriggers([FromBody] string json)
    {
        _logger?.LogInformation("Received request to pause all triggers that are matching the given group matcher");
        _logger?.LogDebug($"Received JSON '{json}'");

        var groupMatcher = GroupMatcher<Quartz.TriggerKey>.FromJsonString(json);
        var result = CreateScheduler.Scheduler.PauseTriggers(groupMatcher.ToGroupMatcher());

        _logger?.LogInformation("Triggers paused");
        return result;
    }
    #endregion

    #region ResumeJob
    /// <summary>
    ///     Resume (un-pause) the <see cref="IJobDetail" /> with
    ///     the given key.
    /// </summary>
    /// <remarks>
    ///     If any of the <see cref="IJob" />'s<see cref="ITrigger" /> s missed one
    ///     or more fire-times, then the <see cref="ITrigger" />'s misfire
    ///     instruction will be applied.
    /// </remarks>
    [HttpPost]
    [Route("scheduler/resumejob")]
    public Task ResumeJob([FromBody] string json)
    {
        _logger?.LogInformation("Received request to resume a job that matches the given job key");
        _logger?.LogDebug($"Received JSON '{json}'");

        var jobKey = JobKey.FromJsonString(json);
        var result = CreateScheduler.Scheduler.ResumeJob(jobKey.ToJobKey());

        _logger?.LogInformation("Job resumed");
        return result;
    }
    #endregion

    #region ResumeJobs
    /// <summary>
    ///     Resume (un-pause) all the <see cref="IJobDetail" />s
    ///     in matching groups.
    /// </summary>
    /// <remarks>
    ///     If any of the <see cref="IJob" /> s had <see cref="ITrigger" /> s that
    ///     missed one or more fire-times, then the <see cref="ITrigger" />'s
    ///     misfire instruction will be applied.
    /// </remarks>
    /// <seealso cref="PauseJobs" />
    [HttpPost]
    [Route("scheduler/resumejobs")]
    public Task ResumeJobs([FromBody] string json)
    {
        _logger?.LogInformation("Received request to resume all jobs that are matching the given group matcher");
        _logger?.LogDebug($"Received JSON '{json}'");

        var groupMatcher = GroupMatcher<Quartz.JobKey>.FromJsonString(json);
        var result = CreateScheduler.Scheduler.ResumeJobs(groupMatcher.ToGroupMatcher());

        _logger?.LogInformation("Jobs resumed");
        return result;
    }
    #endregion

    #region ResumeTrigger
    /// <summary>
    ///     Resume (un-pause) the <see cref="ITrigger" /> with the given
    ///     key.
    /// </summary>
    /// <remarks>
    ///     If the <see cref="ITrigger" /> missed one or more fire-times, then the
    ///     <see cref="ITrigger" />'s misfire instruction will be applied.
    /// </remarks>
    [HttpPost]
    [Route("scheduler/resumetrigger")]
    public Task ResumeTrigger([FromBody] string json)
    {
        _logger?.LogInformation("Received request to resume the trigger that matches the given trigger key");
        _logger?.LogDebug($"Received JSON '{json}'");

        var triggerKey = TriggerKey.FromJsonString(json);
        var result = CreateScheduler.Scheduler.ResumeTrigger(triggerKey.ToTriggerKey());

        _logger?.LogInformation("Trigger resumed");
        return result;
    }
    #endregion

    #region ResumeTriggers
    /// <summary>
    ///     Resume (un-pause) all the <see cref="ITrigger" />s in matching groups.
    /// </summary>
    /// <remarks>
    ///     If any <see cref="ITrigger" /> missed one or more fire-times, then the
    ///     <see cref="ITrigger" />'s misfire instruction will be applied.
    /// </remarks>
    /// <seealso cref="PauseTriggers" />
    [HttpPost]
    [Route("scheduler/resumetriggers")]
    public Task ResumeTriggers([FromBody] string json)
    {
        _logger?.LogInformation("Received request to resume all triggers that are matching the given group matcher");
        _logger?.LogDebug($"Received JSON '{json}'");

        var groupMatcher = GroupMatcher<Quartz.TriggerKey>.FromJsonString(json);
        var result = CreateScheduler.Scheduler.ResumeTriggers(groupMatcher.ToGroupMatcher());

        _logger?.LogInformation("Triggers resumed");
        return result;
    }
    #endregion

    #region PauseAll
    /// <summary>
    ///     Pause all triggers - similar to calling <see cref="PauseTriggers" />
    ///     on every group, however, after using this method <see cref="ResumeAll" />
    ///     must be called to clear the scheduler's state of 'remembering' that all
    ///     new triggers will be paused as they are added.
    /// </summary>
    /// <remarks>
    ///     When <see cref="ResumeAll" /> is called (to un-pause), trigger misfire
    ///     instructions WILL be applied.
    /// </remarks>
    /// <seealso cref="ResumeAll" />
    /// <seealso cref="PauseTriggers" />
    /// <seealso cref="Standby" />
    [HttpPost]
    [Route("scheduler/pauseall")]
    public void PauseAll()
    {
        _logger?.LogInformation("Received request to pause all trigger");
        CreateScheduler.Scheduler.PauseAll();
        _logger?.LogInformation("All triggers paused");
    }
    #endregion

    #region ResumeAll
    /// <summary>
    ///     Resume (un-pause) all triggers - similar to calling
    ///     <see cref="ResumeTriggers" /> on every group.
    /// </summary>
    /// <remarks>
    ///     If any <see cref="ITrigger" /> missed one or more fire-times, then the
    ///     <see cref="ITrigger" />'s misfire instruction will be applied.
    /// </remarks>
    /// <seealso cref="PauseAll" />
    [HttpPost]
    [Route("scheduler/resumeall")]
    public void ResumeAll()
    {
        _logger?.LogInformation("Received request to resume all trigger");
        CreateScheduler.Scheduler.ResumeAll();
        _logger?.LogInformation("All triggers resumed");
    }
    #endregion

    #region GetJobKeys
    /// <summary>
    ///     Get the keys of all the <see cref="IJobDetail" />s in the matching groups.
    /// </summary>
    [HttpGet]
    [Route("scheduler/getjobkeys")]
    public string GetJobKeys([FromBody] string json)
    {
        _logger?.LogInformation("Received request to get all the job keys that are matching the given group matcher");
        _logger?.LogDebug($"Received JSON '{json}'");

        var groupMatcher = GroupMatcher<Quartz.JobKey>.FromJsonString(json);
        var jobKeys = CreateScheduler.Scheduler.GetJobKeys(groupMatcher.ToGroupMatcher()).GetAwaiter().GetResult();
        var result = new JobKeys(jobKeys).ToJsonString();

        _logger?.LogInformation("Returning all job keys that are matching the given group matcher");
        _logger?.LogDebug($"JSON '{result}'");
        return result;
    }
    #endregion

    #region GetTriggersOfJob
    /// <summary>
    ///     Get all <see cref="ITrigger" /> s that are associated with the
    ///     identified <see cref="IJobDetail" />.
    /// </summary>
    /// <remarks>
    ///     The returned Trigger objects will be snapshots of the actual stored
    ///     triggers. If you wish to modify a trigger, you must re-store the
    ///     trigger afterward (e.g. see <see cref="RescheduleJob(string)" />).
    /// </remarks>
    [HttpGet]
    [Route("scheduler/gettriggersofjob")]
    public string GetTriggersOfJob([FromBody] string json)
    {
        _logger?.LogInformation("Received request to get all the triggers for the given job key");
        _logger?.LogDebug($"Received JSON '{json}'");

        var jobKey = JobKey.FromJsonString(json);
        var triggers = CreateScheduler.Scheduler.GetTriggersOfJob(jobKey.ToJobKey()).GetAwaiter().GetResult();
        var result = new Triggers(triggers).ToJsonString();

        _logger?.LogInformation("Returning triggers for the given job key");
        _logger?.LogDebug($"JSON '{result}'");
        return result;
    }
    #endregion

    #region GetTriggerKeys
    /// <summary>
    ///     Get the names of all the <see cref="ITrigger" />s in the given
    ///     groups.
    /// </summary>
    [HttpGet]
    [Route("scheduler/gettriggerkeys")]
    public string GetTriggerKeys([FromBody] string json)
    {
        _logger?.LogInformation(
            "Received request to get all the trigger keys that are matching the given group matcher");
        _logger?.LogDebug($"Received JSON '{json}'");

        var groupMatcher = GroupMatcher<Quartz.TriggerKey>.FromJsonString(json);
        var triggerKeys = CreateScheduler.Scheduler.GetTriggerKeys(groupMatcher.ToGroupMatcher()).GetAwaiter()
            .GetResult();
        var result = new TriggerKeys(triggerKeys).ToJsonString();

        _logger?.LogInformation("Returning all trigger keys that are matching the given group matcher");
        _logger?.LogDebug($"JSON '{result}'");
        return result;
    }
    #endregion

    #region GetJobDetail
    /// <summary>
    ///     Get the <see cref="IJobDetail" /> for the <see cref="IJob" /> instance with the given key.
    /// </summary>
    /// <remarks>
    ///     The returned JobDetail object will be a snapshot of the actual stored
    ///     JobDetail.  If you wish to modify the JobDetail, you must re-store the
    ///     JobDetail afterward (e.g. see <see cref="AddJob(string)" />).
    /// </remarks>
    [HttpGet]
    [Route("scheduler/getjobdetail")]
    public string GetJobDetail([FromBody] string json)
    {
        _logger?.LogInformation("Received request to get the job detail for the given job key");
        _logger?.LogDebug($"Received JSON '{json}'");

        var jobKey = JobKey.FromJsonString(json);
        var jobDetail = CreateScheduler.Scheduler.GetJobDetail(jobKey.ToJobKey()).GetAwaiter().GetResult();
        var result = new JobDetail(jobDetail).ToJsonString();

        _logger?.LogInformation("Returning job detail for the give job key");
        _logger?.LogDebug($"JSON '{result}'");
        return result;
    }
    #endregion

    #region GetTrigger
    /// <summary>
    ///     Get the <see cref="ITrigger" /> instance with the given key.
    /// </summary>
    /// <remarks>
    ///     The returned Trigger object will be a snapshot of the actual stored
    ///     trigger. If you wish to modify the trigger, you must re-store the
    ///     trigger afterward (e.g. see <see cref="RescheduleJob(string)" />).
    /// </remarks>
    [HttpGet]
    [Route("scheduler/gettrigger")]
    public string GetTrigger([FromBody] string json)
    {
        _logger?.LogInformation("Received request to get the trigger for the given trigger key");
        _logger?.LogDebug($"Received JSON '{json}'");

        var triggerKey = TriggerKey.FromJsonString(json);
        var trigger = CreateScheduler.Scheduler.GetTrigger(triggerKey.ToTriggerKey()).GetAwaiter().GetResult();
        var result = new Trigger(trigger).ToJsonString();

        _logger?.LogInformation("Returning trigger for the given trigger key");
        _logger?.LogDebug($"JSON '{result}'");
        return result;
    }
    #endregion

    #region GetTriggerState
    /// <summary>
    ///     Get the current state of the identified <see cref="ITrigger" />.
    /// </summary>
    /// <seealso cref="TriggerState.Normal" />
    /// <seealso cref="TriggerState.Paused" />
    /// <seealso cref="TriggerState.Complete" />
    /// <seealso cref="TriggerState.Blocked" />
    /// <seealso cref="TriggerState.Error" />
    /// <seealso cref="TriggerState.None" />
    [HttpGet]
    [Route("scheduler/gettriggerstate")]
    public string GetTriggerState([FromBody] string json)
    {
        _logger?.LogInformation("Received request to get the trigger state for the given trigger key");
        _logger?.LogDebug($"Received JSON '{json}'");

        var triggerKey = TriggerKey.FromJsonString(json);
        var triggerState = CreateScheduler.Scheduler.GetTriggerState(triggerKey.ToTriggerKey()).GetAwaiter().GetResult();
        var result = triggerState.ToString();

        _logger?.LogInformation($"Returning '{result}'");
        return result;
    }
    #endregion

    /// <summary>
    ///     Add (register) the given <see cref="ICalendar" /> to the Scheduler.
    /// </summary>
    /// <param name="json">The <see cref="ICalendar" /> information</param>
    [HttpPost]
    [Route("scheduler/addcalendar")]
    public Task AddCalendar([FromBody] string json)
    {
        _logger?.LogInformation("Received request to add a calendar to the scheduler");
        _logger?.LogDebug($"Received JSON '{json}'");

        var calendar = BaseCalendar.FromJsonString(json);

        switch (calendar.Type)
        {
            case CalendarType.Cron:
                calendar = CronCalendar.FromJsonString(json);
                break;
            
            case CalendarType.Daily:
                calendar = DailyCalendar.FromJsonString(json);
                break;
            
            case CalendarType.Weekly:
                calendar = WeeklyCalendar.FromJsonString(json);
                break;
            
            case CalendarType.Monthly:
                calendar = MonthlyCalendar.FromJsonString(json);
                break;
            
            case CalendarType.Annual:
                calendar = AnnualCalendar.FromJsonString(json);
                break;
            
            case CalendarType.Holiday:
                calendar = HolidayCalendar.FromJsonString(json);
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }

        return CreateScheduler.Scheduler.AddCalendar(calendar.Name, calendar.ToCalendar(), calendar.Replace,
            calendar.UpdateTriggers);
    }

    #region DeleteCalendar
    /// <summary>
    ///     Delete the identified <see cref="ICalendar" /> from the Scheduler.
    /// </summary>
    /// <remarks>
    ///     If removal of the <code>Calendar</code> would result in
    ///     <see cref="ITrigger" />s pointing to non-existent calendars, then a
    ///     <see cref="SchedulerException" /> will be thrown.
    /// </remarks>
    /// <param name="calName">Name of the calendar.</param>
    /// <returns>true if the Calendar was found and deleted.</returns>
    [HttpPost]
    [Route("scheduler/deletecalendar/{calName}")]
    public bool DeleteCalendar(string calName)
    {
        _logger?.LogInformation($"Received request to delete the calendar '{calName}' from the scheduler");

        var result = CreateScheduler.Scheduler.DeleteCalendar(calName).GetAwaiter().GetResult();

        _logger?.LogInformation($"Returning '{result}'");
        return result;
    }
    #endregion

    /// <summary>
    ///     Get the <see cref="ICalendar" /> instance with the given name.
    /// </summary>
    [HttpGet]
    [Route("scheduler/getcalendar/{calName}")]
    public string GetCalendar(string calName)
    {
        _logger?.LogInformation($"Received request to get the calendar with the name '{calName}' from the scheduler");

        var calendar = CreateScheduler.Scheduler.GetCalendar(calName).GetAwaiter().GetResult();
        if (calendar == null)
        {
            _logger?.LogInformation($"Calendar with the name '{calName}' not found");
            return null;
        }

        string result;

        switch (calendar)
        {
            case Quartz.Impl.Calendar.CronCalendar cronCalendar:
                result = new CronCalendar(cronCalendar).ToJsonString();
                break;
            
            case Quartz.Impl.Calendar.DailyCalendar dailyCalendar:
                result = new DailyCalendar(dailyCalendar).ToJsonString();
                break;
            
            case Quartz.Impl.Calendar.WeeklyCalendar weeklyCalendar:
                result = new WeeklyCalendar(weeklyCalendar).ToJsonString();
                break;
            
            case Quartz.Impl.Calendar.MonthlyCalendar monthlyCalendar:
                result = new MonthlyCalendar(monthlyCalendar).ToJsonString();
                break;
            
            case Quartz.Impl.Calendar.AnnualCalendar annualCalendar:
                result = new AnnualCalendar(annualCalendar).ToJsonString();
                break;
            
            case Quartz.Impl.Calendar.HolidayCalendar holidayCalendar:
                result = new HolidayCalendar(holidayCalendar).ToJsonString();
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }

        _logger?.LogInformation($"Returning '{result}'");

        return result;
    }

    /// <summary>
    ///     Get the names of all registered <see cref="ICalendar" />.
    /// </summary>
    [HttpGet]
    [Route("scheduler/getcalendarnames")]
    public Task<IReadOnlyCollection<string>> GetCalendarNames()
    {
        return CreateScheduler.Scheduler.GetCalendarNames();
    }

    #region InterruptJobKey
    /// <summary>
    ///     Request the cancellation, within this Scheduler instance, of all
    ///     currently executing instances of the identified <see cref="IJob" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         If more than one instance of the identified job is currently executing,
    ///         the cancellation token will be set on each instance.  However, there is a limitation that in the case that
    ///         <see cref="InterruptJobKey(string)" /> on one instances throws an exception, all
    ///         remaining  instances (that have not yet been interrupted) will not have
    ///         their <see cref="InterruptJobKey(string)" /> method called.
    ///     </para>
    ///     <para>
    ///         If you wish to interrupt a specific instance of a job (when more than
    ///         one is executing) you can do so by calling
    ///         <see cref="GetCurrentlyExecutingJobs" /> to obtain a handle
    ///         to the job instance, and then invoke <see cref="InterruptJobKey(string)" /> on it
    ///         yourself.
    ///     </para>
    ///     <para>
    ///         This method is not cluster aware.  That is, it will only interrupt
    ///         instances of the identified InterruptableJob currently executing in this
    ///         Scheduler instance, not across the entire cluster.
    ///     </para>
    /// </remarks>
    /// <returns>
    ///     true is at least one instance of the identified job was found and interrupted.
    /// </returns>
    /// <seealso cref="GetCurrentlyExecutingJobs" />
    [HttpGet]
    [Route("scheduler/interruptjobkey")]
    public bool InterruptJobKey([FromBody] string json)
    {
        _logger?.LogInformation(
            "Received request for cancellation, within this Scheduler instance, of all currently executing instances of the identified job");
        _logger?.LogDebug($"Received JSON '{json}'");

        var jobKey = JobKey.FromJsonString(json);
        var result = CreateScheduler.Scheduler.Interrupt(jobKey.ToJobKey()).GetAwaiter().GetResult();

        _logger?.LogInformation($"Returning '{result}'");
        return result;
    }
    #endregion

    #region InterruptFireInstanceId
    /// <summary>
    ///     Request the cancellation, within this Scheduler instance, of the
    ///     identified executing job instance.
    /// </summary>
    /// <remarks>
    ///     This method is not cluster aware.  That is, it will only interrupt
    ///     instances of the identified InterruptableJob currently executing in this
    ///     Scheduler instance, not across the entire cluster.
    /// </remarks>
    /// <seealso cref="GetCurrentlyExecutingJobs" />
    /// <seealso cref="IJobExecutionContext.FireInstanceId" />
    /// <seealso cref="InterruptJobKey(string)" />
    /// <param name="fireInstanceId">
    ///     the unique identifier of the job instance to  be interrupted (see
    ///     <see cref="IJobExecutionContext.FireInstanceId" />)
    /// </param>
    /// <returns>true if the identified job instance was found and interrupted.</returns>
    [HttpGet]
    [Route("scheduler/interruptfireinstanceid/{fireInstanceId}")]
    public bool InterruptFireInstanceId(string fireInstanceId)
    {
        _logger?.LogInformation(
            "Received request for cancellation, within this Scheduler instance, of the identified executing job instance");

        var result = CreateScheduler.Scheduler.Interrupt(fireInstanceId).GetAwaiter().GetResult();

        _logger?.LogInformation($"Returning '{result}'");
        return result;
    }
    #endregion

    #region CheckExistsJobKey
    /// <summary>
    ///     Determine whether a <see cref="IJob" /> with the given identifier already
    ///     exists within the scheduler.
    /// </summary>
    /// <param name="json">the identifier to check for</param>
    /// <returns>true if a Job exists with the given identifier</returns>
    [HttpGet]
    [Route("scheduler/checkexistsjobkey")]
    public bool CheckExistsJobKey([FromBody] string json)
    {
        _logger?.LogInformation("Received request to check if the given job key exists");
        _logger?.LogDebug($"Received JSON '{json}'");

        var jobKey = JobKey.FromJsonString(json);
        var result = CreateScheduler.Scheduler.CheckExists(jobKey.ToJobKey()).GetAwaiter().GetResult();

        _logger?.LogInformation($"Returning '{result}'");
        return result;
    }
    #endregion

    #region CheckExistsTriggerKey
    /// <summary>
    ///     Determine whether a <see cref="ITrigger" /> with the given identifier already
    ///     exists within the scheduler.
    /// </summary>
    /// <param name="json">the identifier to check for</param>
    /// <returns>true if a Trigger exists with the given identifier</returns>
    [HttpGet]
    [Route("scheduler/checkexiststriggerkey")]
    public bool CheckExistsTriggerKey([FromBody] string json)
    {
        _logger?.LogInformation("Received request to check if the given job key exists");
        _logger?.LogDebug($"Received JSON '{json}'");

        var triggerKey = TriggerKey.FromJsonString(json);
        var result = CreateScheduler.Scheduler.CheckExists(triggerKey.ToTriggerKey()).GetAwaiter().GetResult();

        _logger?.LogInformation($"Returning '{result}'");
        return result;
    }
    #endregion

    #region Clear
    /// <summary>
    ///     Clears (deletes!) all scheduling data - all <see cref="IJob" />s, <see cref="ITrigger" />s
    ///     <see cref="ICalendar" />s.
    /// </summary>
    [HttpPost]
    [Route("scheduler/clear")]
    public void Clear()
    {
        _logger?.LogInformation("Received request to clear the whole scheduler");
        CreateScheduler.Scheduler.Clear();
        _logger?.LogInformation("Scheduler cleared");
    }
    #endregion
}