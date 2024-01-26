using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Quartz;
using Quartz.Impl.Calendar;

namespace QuartzWebApi.Data;

/// <summary>
///     A json wrapper for the <see cref="Quartz.ICalendar" />
/// </summary>
public class Calendar
{
    #region Properties
    /// <summary>
    ///     The name of the calendar
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; internal set; }

    /// <summary>
    ///     The type of the calendar
    /// </summary>
    [JsonProperty("type")]
    public AddCalendarType Type { get; internal set; }

    /// <summary>
    ///     The description of the calendar
    /// </summary>
    [JsonProperty("description")]
    public string Description { get; internal set; }

    /// <summary>
    ///     The base of the calendar
    /// </summary>
    [JsonProperty("calendarbase")]
    public string CalendarBase { get; internal set; }

    /// <summary>
    ///     If set to <c>true</c> [replace].
    /// </summary>
    [JsonProperty("replace")]
    public bool Replace { get; internal set; }

    /// <summary>
    ///     Whether to update existing triggers that
    ///     referenced the already existing calendar so that they are 'correct'
    ///     based on the new trigger.
    /// </summary>
    [JsonProperty("updatetriggers")]
    public bool UpdateTriggers { get; internal set; }

    /// <summary>
    ///     The cron expression
    /// </summary>
    /// <remarks>
    ///     Only used when the <see cref="Type" /> is <see cref="AddCalendarType.Cron" />
    /// </remarks>
    [JsonProperty("cron")]
    public string Cron { get; internal set; }

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
    ///     Only used when the <see cref="Type" /> is <see cref="AddCalendarType.Daily" />
    /// </remarks>
    [JsonProperty("rangestarttime")]
    public string RangeStartTime { get; internal set; }

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
    ///     Only used when the <see cref="Type" /> is <see cref="AddCalendarType.Daily" />
    /// </remarks>
    [JsonProperty("rangeendtime")]
    public string RangeEndTime { get; internal set; }

    /// <summary>
    ///     The day of the week
    /// </summary>
    /// <remarks>
    ///     Only used when the <see cref="Type" /> is <see cref="AddCalendarType.Weekly" />
    /// </remarks>
    [JsonProperty("dayofweeks")]
    public Dictionary<DayOfWeek, bool> DayOfWeeks { get; internal set; }

    /// <summary>
    ///     The days
    /// </summary>
    /// <remarks>
    ///     Only used when the <see cref="Type" /> is <see cref="AddCalendarType.Monthly" />
    /// </remarks>
    [JsonProperty("daysofmonth")]
    public Dictionary<int, bool> DaysOfMonth { get; internal set; }

    /// <summary>
    ///     The days
    /// </summary>
    /// <remarks>
    ///     Only used when the <see cref="Type" /> is <see cref="AddCalendarType.Annual" />
    /// </remarks>
    [JsonProperty("annualdays")]
    public Dictionary<DateTime, bool> AnnualDays { get; internal set; }

    /// <summary>
    ///     The days
    /// </summary>
    /// <remarks>
    ///     Only used when the <see cref="Type" /> is <see cref="AddCalendarType.Holiday" />
    /// </remarks>
    [JsonProperty("holidaydays")]
    public List<DateTime> HolidayDays { get; internal set; }
    #endregion

    #region ToCalendar
    /// <summary>
    ///     Returns this object as a Quartz <see cref="Quartz.ICalendar" />
    /// </summary>
    /// <returns></returns>
    public ICalendar ToCalendar()
    {
        ICalendar result;

        switch (Type)
        {
            case AddCalendarType.Cron:
                result = new CronCalendar(Cron);
                break;

            case AddCalendarType.Daily:
                result = new DailyCalendar(RangeStartTime, RangeEndTime);
                break;

            case AddCalendarType.Weekly:
                result = new WeeklyCalendar();
                foreach (var dayOfWeek in DayOfWeeks)
                    ((WeeklyCalendar)result).SetDayExcluded(dayOfWeek.Key, dayOfWeek.Value);
                break;

            case AddCalendarType.Monthly:
                result = new MonthlyCalendar();
                foreach (var dayOfMonth in DaysOfMonth)
                    ((MonthlyCalendar)result).SetDayExcluded(dayOfMonth.Key, dayOfMonth.Value);
                break;

            case AddCalendarType.Annual:
                result = new AnnualCalendar();
                foreach (var annualDay in AnnualDays)
                    ((AnnualCalendar)result).SetDayExcluded(annualDay.Key, annualDay.Value);
                break;

            case AddCalendarType.Holiday:
                result = new HolidayCalendar();
                foreach (var holidayDay in HolidayDays)
                    ((HolidayCalendar)result).AddExcludedDate(holidayDay);
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }

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
    public string ToJsonString()
    {
        return JsonConvert.SerializeObject(this, Formatting.Indented);
    }
    #endregion

    #region FromJsonString
    /// <summary>
    ///     Returns the <see cref="Calendar" /> object from the given <paramref name="json" /> string
    /// </summary>
    /// <param name="json">The json string</param>
    /// <returns>
    ///     <see cref="Trigger" />
    /// </returns>
    public static Calendar FromJsonString(string json)
    {
        return JsonConvert.DeserializeObject<Calendar>(json);
    }
    #endregion
}