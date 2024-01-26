using System.Runtime.Serialization;

namespace QuartzWebApi.Data;

public enum AddCalendarType
{
    /// <summary>
    ///     It is a cron calendar
    /// </summary>
    [DataMember(Name = "cron")] Cron,

    /// <summary>
    ///     It is a daily calendar
    /// </summary>
    [DataMember(Name = "daily")] Daily,

    /// <summary>
    ///     It is a week calendar
    /// </summary>
    [DataMember(Name = "weekly")] Weekly,

    /// <summary>
    ///     It is a monthly calendar
    /// </summary>
    [DataMember(Name = "monthly")] Monthly,

    /// <summary>
    ///     It is an annual calendar
    /// </summary>
    [DataMember(Name = "annual")] Annual,

    /// <summary>
    ///     It is a holiday calendar
    /// </summary>
    [DataMember(Name = "holiday")] Holiday
}