using System;
using Newtonsoft.Json;
using Quartz;

namespace QuartzWebApi.Wrappers;

/// <summary>
///     Json wrapper for the Quartz <see cref="Quartz.IJobExecutionContext" />
/// </summary>
[JsonObject("JobExecutionContext")]
public class JobExecutionContext(IJobExecutionContext jobExecutionContext)
{
    #region Properties
    /// <summary>
    ///     Get a handle to the <see cref="IScheduler" /> instance that fired the
    ///     <see cref="IJob" />.
    /// </summary>
    [JsonProperty("Scheduler")]
    public IScheduler Scheduler { get; private set; } = jobExecutionContext.Scheduler;

    /// <summary>
    ///     Get a handle to the <see cref="ITrigger" /> instance that fired the
    ///     <see cref="IJob" />.
    /// </summary>
    [JsonProperty("Trigger")]
    public Trigger Trigger { get; private set; } = new(jobExecutionContext.Trigger);

    /// <summary>
    ///     Get a handle to the <see cref="ICalendar" /> referenced by the <see cref="ITrigger" />
    ///     instance that fired the <see cref="IJob" />.
    /// </summary>
    [JsonProperty("Calender")]
    public ICalendar Calendar { get; private set; } = jobExecutionContext.Calendar;

    /// <summary>
    ///     If the <see cref="IJob" /> is being re-executed because of a 'recovery'
    ///     situation, this method will return <see langword="true" />.
    /// </summary>
    [JsonProperty("Recovering")]
    public bool Recovering { get; private set; } = jobExecutionContext.Recovering;

    /// <summary>
    ///     Returns the <see cref="TriggerKeys" /> of the originally scheduled and now recovering job.
    /// </summary>
    /// <remarks>
    ///     When recovering a previously failed job execution this property returns the identity
    ///     of the originally firing trigger. This recovering job will have been scheduled for
    ///     the same firing time as the original job, and so is available via the
    ///     <see cref="ScheduledFireTimeUtc" /> property. The original firing time of the job can be
    ///     accessed via the <see cref="SchedulerConstants.FailedJobOriginalTriggerFiretime" />
    ///     element of this job's <see cref="JobDataMap" />.
    /// </remarks>
    [JsonProperty("RecoveringTriggerKey")]
    public Quartz.TriggerKey RecoveringTriggerKey { get; private set; } = jobExecutionContext.RecoveringTriggerKey;

    /// <summary>
    ///     Gets the refire count.
    /// </summary>
    /// <value>The refire count.</value>
    [JsonProperty("RefireCount")]
    public int RefireCount { get; private set; } = jobExecutionContext.RefireCount;

    /// <summary>
    ///     Get the convenience <see cref="JobDataMap" /> of this execution context.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         The <see cref="JobDataMap" /> found on this object serves as a convenience -
    ///         it is a merge of the <see cref="JobDataMap" /> found on the
    ///         <see cref="JobDetail" /> and the one found on the <see cref="ITrigger" />, with
    ///         the value in the latter overriding any same-named values in the former.
    ///         <i>
    ///             It is thus considered a 'best practice' that the Execute code of a Job
    ///             retrieve data from the JobDataMap found on this object.
    ///         </i>
    ///     </para>
    ///     <para>
    ///         NOTE: Do not expect value 'set' into this JobDataMap to somehow be
    ///         set back onto a job's own JobDataMap.
    ///     </para>
    ///     <para>
    ///         Attempts to change the contents of this map typically result in an
    ///         illegal state.
    ///     </para>
    /// </remarks>
    [JsonProperty("MergedJobDataMap")]
    public JobDataMap MergedJobDataMap { get; private set; } = jobExecutionContext.MergedJobDataMap;

    /// <summary>
    ///     Get the <see cref="JobDetail" /> associated with the <see cref="IJob" />.
    /// </summary>
    [JsonProperty("JobDetail")]
    public JobDetail JobDetail { get; private set; } = new(jobExecutionContext.JobDetail);

    /// <summary>
    ///     Get the instance of the <see cref="IJob" /> that was created for this
    ///     execution.
    ///     <para>
    ///         Note: The Job instance is not available through remote scheduler
    ///         interfaces.
    ///     </para>
    /// </summary>
    [JsonProperty("JobInstance")]
    public string JobInstance { get; private set; } = jobExecutionContext.JobInstance.ToString();

    /// <summary>
    ///     The actual time the trigger fired. For instance the scheduled time may
    ///     have been 10:00:00 but the actual fire time may have been 10:00:03 if
    ///     the scheduler was too busy.
    /// </summary>
    /// <returns> Returns the fireTimeUtc.</returns>
    /// <seealso cref="ScheduledFireTimeUtc" />
    [JsonProperty("FireTimeUtc")]
    public DateTimeOffset FireTimeUtc { get; private set; } = jobExecutionContext.FireTimeUtc;

    /// <summary>
    ///     The scheduled time the trigger fired for. For instance the scheduled
    ///     time may have been 10:00:00 but the actual fire time may have been
    ///     10:00:03 if the scheduler was too busy.
    /// </summary>
    /// <returns> Returns the scheduledFireTimeUtc.</returns>
    /// <seealso cref="FireTimeUtc" />
    [JsonProperty("ScheduledFireTimeUtc")]
    public DateTimeOffset? ScheduledFireTimeUtc { get; private set; } = jobExecutionContext.ScheduledFireTimeUtc;

    /// <summary>
    ///     Gets the previous fire time.
    /// </summary>
    /// <value>The previous fire time.</value>
    [JsonProperty("PreviousFireTimeUtc")]
    public DateTimeOffset? PreviousFireTimeUtc { get; private set; } = jobExecutionContext.PreviousFireTimeUtc;

    /// <summary>
    ///     Gets the next fire time.
    /// </summary>
    /// <value>The next fire time.</value>
    [JsonProperty("NextFireTimeUtc")]
    public DateTimeOffset? NextFireTimeUtc { get; private set; } = jobExecutionContext.NextFireTimeUtc;

    /// <summary>
    ///     Get the unique id that identifies this particular firing instance of the
    ///     trigger that triggered this job execution.  It is unique to this
    ///     JobExecutionContext instance as well.
    /// </summary>
    /// <returns>the unique fire instance id</returns>
    /// <seealso cref="IScheduler.Interrupt(System.String, System.Threading.CancellationToken)" />
    [JsonProperty("FireInstanceId")]
    public string FireInstanceId { get; private set; } = jobExecutionContext.FireInstanceId;

    ///// <summary>
    /////     Returns the result (if any) that the <see cref="IJob" /> set before its
    /////     execution completed (the type of object set as the result is entirely up
    /////     to the particular job).
    ///// </summary>
    ///// <remarks>
    /////     <para>
    /////         The result itself is meaningless to Quartz, but may be informative
    /////         to <see cref="IJobListener" />s or
    /////         <see cref="ITriggerListener" />s that are watching the job's
    /////         execution.
    /////     </para>
    /////     Set the result (if any) of the <see cref="IJob" />'s execution (the type of
    /////     object set as the result is entirely up to the particular job).
    /////     <para>
    /////         The result itself is meaningless to Quartz, but may be informative
    /////         to <see cref="IJobListener" />s or
    /////         <see cref="ITriggerListener" />s that are watching the job's
    /////         execution.
    /////     </para>
    ///// </remarks>
    //[JsonProperty("Result")]
    //public object Result { get; private set; }

    /// <summary>
    ///     The amount of time the job ran for.  The returned
    ///     value will be <see cref="TimeSpan.MinValue" /> until the job has actually completed (or thrown an
    ///     exception), and is therefore generally only useful to
    ///     <see cref="IJobListener" />s and <see cref="ITriggerListener" />s.
    /// </summary>
    [JsonProperty("JobRunTime")]
    public TimeSpan JobRunTime { get; private set; } = jobExecutionContext.JobRunTime;
    #endregion

    #region Constructor
    //Result = jobExecutionContext.Result;
    #endregion
}