﻿using System;
using Newtonsoft.Json;
using Quartz;

namespace QuartzWebApi.Wrappers.Calendars;

/// <summary>
///     A json wrapper for the <see cref="Quartz.Impl.Calendar.DailyCalendar" />
/// </summary>
public class DailyCalendar : BaseCalendar
{
    #region Properties
    /// <summary>
    ///     Must be in the format &quot;HH:MM[:SS[:mmm]]&quot; where:
    ///     <ul>
    ///         <li>
    ///             HH is the hour of the specified time. The hour should be
    ///             specified using military (24-hour) time and must be in the range
    ///             0 to 23.
    ///         </li>
    ///         <li>
    ///             MM is the minute of the specified time and must be in the range
    ///             0 to 59.
    ///         </li>
    ///         <li>
    ///             SS is the second of the specified time and must be in the range
    ///             0 to 59.
    ///         </li>
    ///         <li>
    ///             mmm is the millisecond of the specified time and must be in the
    ///             range 0 to 999.
    ///         </li>
    ///         <li>items enclosed in brackets ('[', ']') are optional.</li>
    ///         <li>
    ///             The time range starting time must be before the time range ending
    ///             time. Note this means that a time range may not cross daily
    ///             boundaries (10PM - 2AM)
    ///         </li>
    ///     </ul>
    /// </summary>
    /// <remarks>
    ///     Only used when the <see cref="Type" /> is <see cref="CalendarType.Daily" />
    /// </remarks>
    [JsonProperty("RangeStartingTime")]
    public string RangeStartingTime { get; internal set; }

    /// <summary>
    ///     Must be in the format &quot;HH:MM[:SS[:mmm]]&quot; where:
    ///     <ul>
    ///         <li>
    ///             HH is the hour of the specified time. The hour should be
    ///             specified using military (24-hour) time and must be in the range
    ///             0 to 23.
    ///         </li>
    ///         <li>
    ///             MM is the minute of the specified time and must be in the range
    ///             0 to 59.
    ///         </li>
    ///         <li>
    ///             SS is the second of the specified time and must be in the range
    ///             0 to 59.
    ///         </li>
    ///         <li>
    ///             mmm is the millisecond of the specified time and must be in the
    ///             range 0 to 999.
    ///         </li>
    ///         <li>items enclosed in brackets ('[', ']') are optional.</li>
    ///         <li>
    ///             The time range starting time must be before the time range ending
    ///             time. Note this means that a time range may not cross daily
    ///             boundaries (10PM - 2AM)
    ///         </li>
    ///     </ul>
    /// </summary>
    [JsonProperty("RangeEndingTime")]
    public string RangeEndingTime { get; internal set; }

    /// <summary>
    /// Indicates whether the time range represents an inverted time range (see
    /// class description).
    /// </summary>
    /// <value><c>true</c> if invert time range; otherwise, <c>false</c>.</value>
    [JsonProperty("InvertTimeRange")]
    public bool InvertTimeRange { get; internal set; }
    #endregion

    #region Constructor
    /// <summary>
    ///     Takes a <see cref="Quartz.Impl.Calendar.CronCalendar" /> and wraps it in a json object
    /// </summary>
    /// <param name="dailyCalendar"><see cref="Quartz.Impl.Calendar.DailyCalendar" /></param>
    public DailyCalendar(Quartz.Impl.Calendar.DailyCalendar dailyCalendar) : base(dailyCalendar)
    {
        Type = CalendarType.Daily;
        RangeStartingTime = dailyCalendar.RangeStartingTime;
        RangeEndingTime = dailyCalendar.RangeEndingTime;
        InvertTimeRange = dailyCalendar.InvertTimeRange;
        TimeZone = dailyCalendar.TimeZone;
    }
    #endregion

    #region ToCalendar
    /// <summary>
    ///     Returns this object as a Quartz <see cref="Quartz.ICalendar" />
    /// </summary>
    /// <returns></returns>
    public override ICalendar ToCalendar()
    {
        var result = new Quartz.Impl.Calendar.DailyCalendar(RangeStartingTime, RangeEndingTime)
        {
            Description = Description,
            TimeZone = TimeZone,
            InvertTimeRange = InvertTimeRange
        };

        if (!string.IsNullOrEmpty(CalendarBase))
            result.CalendarBase = CreateScheduler.Scheduler.GetCalendar(CalendarBase).GetAwaiter().GetResult();

        return result;
    }
    #endregion

    #region ToJsonString
    /// <summary>
    ///     Returns this object as a json string
    /// </summary>
    /// <returns></returns>
    public new string ToJsonString()
    {
        return JsonConvert.SerializeObject(this, Formatting.Indented);
    }
    #endregion

    #region FromJsonString
    /// <summary>
    ///     Returns the <see cref="DailyCalendar" /> object from the given <paramref name="json" /> string
    /// </summary>
    /// <param name="json">The json string</param>
    /// <returns>
    ///     <see cref="DailyCalendar" />
    /// </returns>
    public new static DailyCalendar FromJsonString(string json)
    {
        return JsonConvert.DeserializeObject<DailyCalendar>(json);
    }
    #endregion
}