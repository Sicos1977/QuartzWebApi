using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Quartz;
using Quartz.Impl.Matchers;
using Quartz.Spi;

namespace QuartzWebApi.Controllers
{
    public class SchedulerController : ApiController
    {
        /// <summary>
        ///     Returns <c>true</c> if the given JobGroup is paused
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("scheduler/isjobgrouppaused/{groupName}")]
        public Task<bool> IsJobGroupPaused(string groupName)
        {
            return CreateScheduler.Scheduler.IsJobGroupPaused(groupName);
        }

        /// <summary>
        ///     Returns <c>true</c> if the given TriggerGroup is paused
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("scheduler/istriggergrouppaused/{groupName}")]
        public Task<bool> IsTriggerGroupPaused(string groupName)
        {
            return CreateScheduler.Scheduler.IsTriggerGroupPaused(groupName);
        }

        /// <summary>
        ///     Returns the name of the scheduler
        /// </summary>
        [HttpGet]
        [Route("scheduler/schedulername")]
        public string SchedulerName()
        {
            return CreateScheduler.Scheduler.SchedulerName;
        }

        /// <summary>
        ///     Returns the instance Id of the scheduler
        /// </summary>
        [HttpGet]
        [Route("scheduler/getschedulerinstanceid")]
        public string SchedulerInstanceId()
        {
            return CreateScheduler.Scheduler.SchedulerInstanceId;
        }

        ///// <summary>
        ///// Returns the <see cref="SchedulerContext" /> of the <see cref="IScheduler" />.
        ///// </summary>
        //SchedulerContext Context { get; }

        /// <summary>
        ///     Reports whether the <see cref="IScheduler" /> is in stand-by mode.
        /// </summary>
        /// <seealso cref="Standby" />
        /// <seealso cref="Start" />
        [HttpGet]
        [Route("scheduler/instandbymode")]
        public bool InStandbyMode()
        {
            return CreateScheduler.Scheduler.InStandbyMode;
        }

        /// <summary>
        ///     Reports whether the <see cref="IScheduler" /> has been Shutdown.
        /// </summary>
        [HttpGet]
        [Route("scheduler/isshutdown")]
        public bool IsShutdown()
        {
            return CreateScheduler.Scheduler.IsShutdown;
        }

        ///// <summary>
        ///// Get a <see cref="SchedulerMetaData" /> object describing the settings
        ///// and capabilities of the scheduler instance.
        ///// </summary>
        ///// <remarks>
        ///// Note that the data returned is an 'instantaneous' snap-shot, and that as
        ///// soon as it's returned, the meta data values may be different.
        ///// </remarks>
        //Task<SchedulerMetaData> GetMetaData();

        ///// <summary>
        ///// Return a list of <see cref="IJobExecutionContext" /> objects that
        ///// represent all currently executing Jobs in this Scheduler instance.
        ///// </summary>
        ///// <remarks>
        ///// <para>
        ///// This method is not cluster aware.  That is, it will only return Jobs
        ///// currently executing in this Scheduler instance, not across the entire
        ///// cluster.
        ///// </para>
        ///// <para>
        ///// Note that the list returned is an 'instantaneous' snap-shot, and that as
        ///// soon as it's returned, the true list of executing jobs may be different.
        ///// Also please read the doc associated with <see cref="IJobExecutionContext" />-
        ///// especially if you're using remoting.
        ///// </para>
        ///// </remarks>
        ///// <seealso cref="IJobExecutionContext" />
        //Task<IReadOnlyCollection<IJobExecutionContext>> GetCurrentlyExecutingJobs(
        //    );

        ///// <summary>
        ///// Set the <see cref="JobFactory" /> that will be responsible for producing 
        ///// instances of <see cref="IJob" /> classes.
        ///// </summary>
        ///// <remarks>
        ///// JobFactories may be of use to those wishing to have their application
        ///// produce <see cref="IJob" /> instances via some special mechanism, such as to
        ///// give the opportunity for dependency injection.
        ///// </remarks>
        ///// <seealso cref="IJobFactory" />
        //IJobFactory JobFactory { set; }

        ///// <summary>
        ///// Get a reference to the scheduler's <see cref="IListenerManager" />,
        ///// through which listeners may be registered.
        ///// </summary>
        ///// <returns>the scheduler's <see cref="IListenerManager" /></returns>
        ///// <seealso cref="ListenerManager" />
        ///// <seealso cref="IJobListener" />
        ///// <seealso cref="ITriggerListener" />
        ///// <seealso cref="ISchedulerListener" />
        //IListenerManager ListenerManager { get; }

        /// <summary>
        ///     Get the names of all known <see cref="IJobDetail" /> groups.
        /// </summary>
        [HttpGet]
        [Route("scheduler/getjobgroupnames")]
        public Task<IReadOnlyCollection<string>> GetJobGroupNames()
        {
            return CreateScheduler.Scheduler.GetJobGroupNames();
        }


        /// <summary>
        ///     Get the names of all known <see cref="ITrigger" /> groups.
        /// </summary>
        [HttpGet]
        [Route("scheduler/gettriggergroupnames")]
        public Task<IReadOnlyCollection<string>> GetTriggerGroupNames()
        {
            return CreateScheduler.Scheduler.GetTriggerGroupNames();
        }

        /// <summary>
        ///     Get the names of all <see cref="ITrigger" /> groups that are paused.
        /// </summary>
        [HttpGet]
        [Route("scheduler/getpausedtriggergroups")]
        public Task<IReadOnlyCollection<string>> GetPausedTriggerGroups()
        {
            return CreateScheduler.Scheduler.GetPausedTriggerGroups();
        }

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
            return CreateScheduler.Scheduler.Start();
        }

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
        [Route("scheduler/startdelayed")]
        public Task StartDelayed(TimeSpan delay)
        {
            return CreateScheduler.Scheduler.StartDelayed(delay);
        }

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
            return CreateScheduler.Scheduler.IsStarted;
        }

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
            return CreateScheduler.Scheduler.Standby();
        }

        /// <summary>
        ///     Halts the <see cref="IScheduler" />'s firing of <see cref="ITrigger" />s,
        ///     and cleans up all resources associated with the Scheduler. Equivalent to Shutdown(false).
        /// </summary>
        /// <remarks>
        ///     The scheduler cannot be re-started.
        /// </remarks>
        /// <seealso cref="Shutdown(bool)" />
        [HttpPost]
        [Route("scheduler/shutdown")]
        public Task Shutdown()
        {
            return CreateScheduler.Scheduler.Standby();
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
            return CreateScheduler.Scheduler.Shutdown(waitForJobsToComplete);
        }

        ///// <summary>
        ///// Add the given <see cref="IJobDetail" /> to the
        ///// Scheduler, and associate the given <see cref="ITrigger" /> with
        ///// it.
        ///// </summary>
        ///// <remarks>
        ///// If the given Trigger does not reference any <see cref="IJob" />, then it
        ///// will be set to reference the Job passed with it into this method.
        ///// </remarks>
        //DateTimeOffset ScheduleJob(IJobDetail jobDetail, ITrigger trigger);

        ///// <summary>
        ///// Schedule the given <see cref="ITrigger" /> with the
        ///// <see cref="IJob" /> identified by the <see cref="ITrigger" />'s settings.
        ///// </summary>
        //Task<DateTimeOffset> ScheduleJob(ITrigger trigger);

        ///// <summary>
        ///// Schedule all of the given jobs with the related set of triggers.
        ///// </summary>
        ///// <remarks>
        ///// <para>If any of the given jobs or triggers already exist (or more
        ///// specifically, if the keys are not unique) and the replace
        ///// parameter is not set to true then an exception will be thrown.</para>
        ///// </remarks>
        //Task ScheduleJobs(IReadOnlyDictionary<IJobDetail, IReadOnlyCollection<ITrigger>> triggersAndJobs, bool replace);

        ///// <summary>
        ///// Schedule the given job with the related set of triggers.
        ///// </summary>
        ///// <remarks>
        ///// If any of the given job or triggers already exist (or more
        ///// specifically, if the keys are not unique) and the replace 
        ///// parameter is not set to true then an exception will be thrown.
        ///// </remarks>
        //Task ScheduleJob(
        //    IJobDetail jobDetail,
        //    IReadOnlyCollection<ITrigger> triggersForJob,
        //    bool replace,
        //    );

        /// <summary>
        ///     Remove the indicated <see cref="ITrigger" /> from the scheduler.
        ///     <para>
        ///         If the related job does not have any other triggers, and the job is
        ///         not durable, then the job will also be deleted.
        ///     </para>
        /// </summary>
        [HttpGet]
        [Route("scheduler/unschedulejob/{triggerKey}")]
        public Task<bool> UnscheduleJob(TriggerKey triggerKey)
        {
            return CreateScheduler.Scheduler.UnscheduleJob(triggerKey);
        }

        /// <summary>
        ///     Remove all of the indicated <see cref="ITrigger" />s from the scheduler.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         If the related job does not have any other triggers, and the job is
        ///         not durable, then the job will also be deleted.
        ///     </para>
        ///     Note that while this bulk operation is likely more efficient than
        ///     invoking <see cref="UnscheduleJob" /> several
        ///     times, it may have the adverse affect of holding data locks for a
        ///     single long duration of time (rather than lots of small durations
        ///     of time).
        /// </remarks>
        [HttpGet]
        [Route("scheduler/unschedulejobs/{triggerKeys}")]
        public Task<bool> UnscheduleJobs(IReadOnlyCollection<TriggerKey> triggerKeys)
        {
            return CreateScheduler.Scheduler.UnscheduleJobs(triggerKeys);
        }

        ///// <summary>
        ///// Remove (delete) the <see cref="ITrigger" /> with the
        ///// given key, and store the new given one - which must be associated
        ///// with the same job (the new trigger must have the job name &amp; group specified) 
        ///// - however, the new trigger need not have the same name as the old trigger.
        ///// </summary>
        ///// <param name="triggerKey">The <see cref="ITrigger" /> to be replaced.</param>
        ///// <param name="newTrigger">
        /////     The new <see cref="ITrigger" /> to be stored.
        ///// </param>
        //
        ///// <returns> 
        ///// <see langword="null" /> if a <see cref="ITrigger" /> with the given
        ///// name and group was not found and removed from the store (and the 
        ///// new trigger is therefore not stored),  otherwise
        ///// the first fire time of the newly scheduled trigger.
        ///// </returns>
        //Task<DateTimeOffset?> RescheduleJob(TriggerKey triggerKey, ITrigger newTrigger);

        ///// <summary>
        ///// Add the given <see cref="IJob" /> to the Scheduler - with no associated
        ///// <see cref="ITrigger" />. The <see cref="IJob" /> will be 'dormant' until
        ///// it is scheduled with a <see cref="ITrigger" />, or <see cref="TriggerJob(Quartz.JobKey, CancellationToken)" />
        ///// is called for it.
        ///// </summary>
        ///// <remarks>
        ///// The <see cref="IJob" /> must by definition be 'durable', if it is not,
        ///// SchedulerException will be thrown.
        ///// </remarks>
        //Task AddJob(
        //    IJobDetail jobDetail,
        //    bool replace,
        //    );

        ///// <summary>
        ///// Add the given <see cref="IJob" /> to the Scheduler - with no associated
        ///// <see cref="ITrigger" />. The <see cref="IJob" /> will be 'dormant' until
        ///// it is scheduled with a <see cref="ITrigger" />, or <see cref="TriggerJob(Quartz.JobKey, CancellationToken)" />
        ///// is called for it.
        ///// </summary>
        ///// <remarks>
        ///// With the <paramref name="storeNonDurableWhileAwaitingScheduling"/> parameter
        ///// set to <code>true</code>, a non-durable job can be stored.  Once it is
        ///// scheduled, it will resume normal non-durable behavior (i.e. be deleted
        ///// once there are no remaining associated triggers).
        ///// </remarks>
        //Task AddJob(IJobDetail jobDetail, bool replace, bool storeNonDurableWhileAwaitingScheduling);

        /// <summary>
        ///     Delete the identified <see cref="IJob" /> from the Scheduler - and any
        ///     associated <see cref="ITrigger" />s.
        /// </summary>
        /// <returns> true if the Job was found and deleted.</returns>
        [HttpGet]
        [Route("scheduler/deletejob")]
        public Task<bool> GetDeleteJob([FromBody] JobKey jobKey)
        {
            return CreateScheduler.Scheduler.DeleteJob(jobKey);
        }

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
        ///     true if all of the Jobs were found and deleted, false if
        ///     one or more were not deleted.
        /// </returns>
        [HttpGet]
        [Route("scheduler/deletejobs")]
        public bool DeleteJobs(IReadOnlyCollection<JobKey> jobKeys)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Trigger the identified <see cref="IJobDetail" />
        ///     (Execute it now).
        /// </summary>
        [HttpPost]
        [Route("scheduler/triggerjob/{jobKey}")]
        public void TriggerJob([FromBody] string jobKey)
        {
            throw new NotImplementedException();
        }

        ///// <summary>
        ///// Trigger the identified <see cref="IJobDetail" /> (Execute it now).
        ///// </summary>
        ///// <param name="data">
        ///// the (possibly <see langword="null" />) JobDataMap to be
        ///// associated with the trigger that fires the job immediately.
        ///// </param>
        ///// <param name="jobKey">
        ///// The <see cref="JobKey"/> of the <see cref="IJob" /> to be executed.
        ///// </param>
        //
        //Task TriggerJob(
        //    JobKey jobKey,
        //    JobDataMap data,
        //    );

        ///// <summary>
        ///// Pause the <see cref="IJobDetail" /> with the given
        ///// key - by pausing all of its current <see cref="ITrigger" />s.
        ///// </summary>
        //Task PauseJob(
        //    JobKey jobKey,
        //    );

        ///// <summary>
        ///// Pause all of the <see cref="IJobDetail" />s in the
        ///// matching groups - by pausing all of their <see cref="ITrigger" />s.
        ///// </summary>
        ///// <remarks>
        ///// <para>
        ///// The Scheduler will "remember" that the groups are paused, and impose the
        ///// pause on any new jobs that are added to any of those groups until it is resumed.
        ///// </para>
        ///// <para>NOTE: There is a limitation that only exactly matched groups
        ///// can be remembered as paused.  For example, if there are pre-existing
        ///// job in groups "aaa" and "bbb" and a matcher is given to pause
        ///// groups that start with "a" then the group "aaa" will be remembered
        ///// as paused and any subsequently added jobs in group "aaa" will be paused,
        ///// however if a job is added to group "axx" it will not be paused,
        ///// as "axx" wasn't known at the time the "group starts with a" matcher 
        ///// was applied.  HOWEVER, if there are pre-existing groups "aaa" and
        ///// "bbb" and a matcher is given to pause the group "axx" (with a
        ///// group equals matcher) then no jobs will be paused, but it will be 
        ///// remembered that group "axx" is paused and later when a job is added 
        ///// in that group, it will become paused.</para>
        ///// </remarks>
        ///// <seealso cref="ResumeJobs" />
        //Task PauseJobs(GroupMatcher<JobKey> matcher);

        ///// <summary> 
        ///// Pause the <see cref="ITrigger" /> with the given key.
        ///// </summary>
        //Task PauseTrigger(TriggerKey triggerKey);

        ///// <summary>
        ///// Pause all of the <see cref="ITrigger" />s in the groups matching.
        ///// </summary>
        ///// <remarks>
        ///// <para>
        ///// The Scheduler will "remember" all the groups paused, and impose the
        ///// pause on any new triggers that are added to any of those groups until it is resumed.
        ///// </para>
        ///// <para>NOTE: There is a limitation that only exactly matched groups
        ///// can be remembered as paused.  For example, if there are pre-existing
        ///// triggers in groups "aaa" and "bbb" and a matcher is given to pause
        ///// groups that start with "a" then the group "aaa" will be remembered as
        ///// paused and any subsequently added triggers in that group be paused,
        ///// however if a trigger is added to group "axx" it will not be paused,
        ///// as "axx" wasn't known at the time the "group starts with a" matcher 
        ///// was applied.  HOWEVER, if there are pre-existing groups "aaa" and
        ///// "bbb" and a matcher is given to pause the group "axx" (with a
        ///// group equals matcher) then no triggers will be paused, but it will be 
        ///// remembered that group "axx" is paused and later when a trigger is added
        ///// in that group, it will become paused.</para>
        ///// </remarks>
        ///// <seealso cref="ResumeTriggers" />
        //Task PauseTriggers(GroupMatcher<TriggerKey> matcher);

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
        [Route("scheduler/resumejob/{jobKey}")]
        public void ResumeJob(JobKey jobKey)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Resume (un-pause) all of the <see cref="IJobDetail" />s
        ///     in matching groups.
        /// </summary>
        /// <remarks>
        ///     If any of the <see cref="IJob" /> s had <see cref="ITrigger" /> s that
        ///     missed one or more fire-times, then the <see cref="ITrigger" />'s
        ///     misfire instruction will be applied.
        /// </remarks>
        /// <seealso cref="PutPauseJobs" />
        [HttpPost]
        [Route("scheduler/resumejobs/{matcher}")]
        public void ResumeJobs(GroupMatcher<JobKey> matcher)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Resume (un-pause) the <see cref="ITrigger" /> with the given
        ///     key.
        /// </summary>
        /// <remarks>
        ///     If the <see cref="ITrigger" /> missed one or more fire-times, then the
        ///     <see cref="ITrigger" />'s misfire instruction will be applied.
        /// </remarks>
        [HttpPost]
        [Route("scheduler/resumetrigger/{triggerKey}")]
        public void ResumeTrigger(TriggerKey triggerKey)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Resume (un-pause) all of the <see cref="ITrigger" />s in matching groups.
        /// </summary>
        /// <remarks>
        ///     If any <see cref="ITrigger" /> missed one or more fire-times, then the
        ///     <see cref="ITrigger" />'s misfire instruction will be applied.
        /// </remarks>
        /// <seealso cref="PauseTriggers" />
        [HttpPost]
        [Route("scheduler/resumetriggers/{matcher}")]
        public void ResumeTriggers(GroupMatcher<TriggerKey> matcher)
        {
            throw new NotImplementedException();
        }

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
            throw new NotImplementedException();
        }

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
            throw new NotImplementedException();
        }

        ///// <summary>
        ///// Get the keys of all the <see cref="IJobDetail" />s in the matching groups.
        ///// </summary>
        //Task<IReadOnlyCollection<JobKey>> GetJobKeys(GroupMatcher<JobKey> matcher);

        ///// <summary>
        ///// Get all <see cref="ITrigger" /> s that are associated with the
        ///// identified <see cref="IJobDetail" />.
        ///// </summary>
        ///// <remarks>
        ///// The returned Trigger objects will be snap-shots of the actual stored
        ///// triggers.  If you wish to modify a trigger, you must re-store the
        ///// trigger afterward (e.g. see <see cref="RescheduleJob(TriggerKey, ITrigger, CancellationToken)" />).
        ///// </remarks>
        //public IReadOnlyCollection<ITrigger> GetTriggersOfJob(JobKey jobKey)
        //{
        //    throw new NotImplementedException();
        //}

        ///// <summary>
        ///// Get the names of all the <see cref="ITrigger" />s in the given
        ///// groups.
        ///// </summary>
        //IReadOnlyCollection<TriggerKey> GetTriggerKeys(GroupMatcher<TriggerKey> matcher)
        //{
        //    throw new NotImplementedException();
        //}

        ///// <summary>
        ///// Get the <see cref="IJobDetail" /> for the <see cref="IJob" />
        ///// instance with the given key .
        ///// </summary>
        ///// <remarks>
        ///// The returned JobDetail object will be a snap-shot of the actual stored
        ///// JobDetail.  If you wish to modify the JobDetail, you must re-store the
        ///// JobDetail afterward (e.g. see <see cref="AddJob(IJobDetail, bool, CancellationToken)" />).
        ///// </remarks>
        //Task<IJobDetail?> GetJobDetail(JobKey jobKey);

        ///// <summary>
        ///// Get the <see cref="ITrigger" /> instance with the given key.
        ///// </summary>
        ///// <remarks>
        ///// The returned Trigger object will be a snap-shot of the actual stored
        ///// trigger. If you wish to modify the trigger, you must re-store the
        ///// trigger afterward (e.g. see <see cref="RescheduleJob(TriggerKey, ITrigger, CancellationToken)" />).
        ///// </remarks>
        //Task<ITrigger?> GetTrigger(TriggerKey triggerKey)
        //{
        //    throw new NotImplementedException();
        //}

        ///// <summary>
        ///// Get the current state of the identified <see cref="ITrigger" />.
        ///// </summary>
        ///// <seealso cref="TriggerState.Normal" />
        ///// <seealso cref="TriggerState.Paused" />
        ///// <seealso cref="TriggerState.Complete" />
        ///// <seealso cref="TriggerState.Blocked" />
        ///// <seealso cref="TriggerState.Error" />
        ///// <seealso cref="TriggerState.None" />
        //Task<TriggerState> GetTriggerState(
        //    TriggerKey triggerKey,
        //    );

        ///// <summary>
        ///// Add (register) the given <see cref="ICalendar" /> to the Scheduler.
        ///// </summary>
        ///// <param name="calName">Name of the calendar.</param>
        ///// <param name="calendar">The calendar.</param>
        ///// <param name="replace">if set to <c>true</c> [replace].</param>
        ///// <param name="updateTriggers">whether or not to update existing triggers that
        ///// referenced the already existing calendar so that they are 'correct'
        ///// based on the new trigger.</param>
        //
        //Task AddCalendar(
        //    string calName,
        //    ICalendar calendar,
        //    bool replace,
        //    bool updateTriggers,
        //    );

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
        [HttpGet]
        [Route("scheduler/deletecalender/{calName}")]
        public bool DeleteCalendar(string calName)
        {
            throw new NotImplementedException();
        }

        ///// <summary>
        ///// Get the <see cref="ICalendar" /> instance with the given name.
        ///// </summary>
        //Task<ICalendar?> GetCalendar(string calName, );

        /// <summary>
        ///     Get the names of all registered <see cref="ICalendar" />.
        /// </summary>
        [HttpGet]
        [Route("scheduler/getcalendarnames")]
        public IReadOnlyCollection<string> GetCalendarNames()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Request the cancellation, within this Scheduler instance, of all
        ///     currently executing instances of the identified <see cref="IJob" />.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         If more than one instance of the identified job is currently executing,
        ///         the cancellation token will be set on each instance.  However, there is a limitation that in the case that
        ///         <see cref="Interrupt(JobKey)" /> on one instances throws an exception, all
        ///         remaining  instances (that have not yet been interrupted) will not have
        ///         their <see cref="Interrupt(JobKey)" /> method called.
        ///     </para>
        ///     <para>
        ///         If you wish to interrupt a specific instance of a job (when more than
        ///         one is executing) you can do so by calling
        ///         <see cref="CurrentlyExecutingJobs" /> to obtain a handle
        ///         to the job instance, and then invoke <see cref="Interrupt(JobKey)" /> on it
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
        /// <seealso cref="CurrentlyExecutingJobs" />
        [HttpGet]
        [Route("scheduler/interrupt/jobkey/{jobKey}")]
        public bool Interrupt(JobKey jobKey)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Request the cancellation, within this Scheduler instance, of the
        ///     identified executing job instance.
        /// </summary>
        /// <remarks>
        ///     This method is not cluster aware.  That is, it will only interrupt
        ///     instances of the identified InterruptableJob currently executing in this
        ///     Scheduler instance, not across the entire cluster.
        /// </remarks>
        /// <seealso cref="CurrentlyExecutingJobs" />
        /// <seealso cref="IJobExecutionContext.FireInstanceId" />
        /// <seealso cref="Interrupt(JobKey)" />
        /// <param name="fireInstanceId">
        ///     the unique identifier of the job instance to  be interrupted (see
        ///     <see cref="IJobExecutionContext.FireInstanceId" />)
        /// </param>
        /// <returns>true if the identified job instance was found and interrupted.</returns>
        [HttpGet]
        [Route("scheduler/interrupt/fireinstanceid/{fireInstanceId}")]
        public bool Interrupt(string fireInstanceId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Determine whether a <see cref="IJob" /> with the given identifier already
        ///     exists within the scheduler.
        /// </summary>
        /// <param name="jobKey">the identifier to check for</param>
        /// <returns>true if a Job exists with the given identifier</returns>
        [HttpGet]
        [Route("scheduler/checkexists/jobkey/{jobKey}")]
        public bool CheckExistsJobKey(JobKey jobKey)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Determine whether a <see cref="ITrigger" /> with the given identifier already
        ///     exists within the scheduler.
        /// </summary>
        /// <param name="triggerKey">the identifier to check for</param>
        /// <returns>true if a Trigger exists with the given identifier</returns>
        [HttpGet]
        [Route("scheduler/checkexists/triggerkey/{triggerKey}")]
        public bool CheckExists(TriggerKey triggerKey)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Clears (deletes!) all scheduling data - all <see cref="IJob" />s, <see cref="ITrigger" />s
        ///     <see cref="ICalendar" />s.
        /// </summary>
        [HttpPost]
        [Route("scheduler/clear")]
        public void Clear()
        {
        }
    }
}