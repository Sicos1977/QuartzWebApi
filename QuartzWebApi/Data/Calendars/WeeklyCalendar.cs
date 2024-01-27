using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Quartz;

namespace QuartzWebApi.Data.Calendars;

/// <summary>
///     A json wrapper for the <see cref="Quartz.ICalendar" />
/// </summary>
public class WeeklyCalendar : BaseCalendar
{
    #region Properties
    /// <summary>
    ///     Returns a collection of days of the week that are excluded
    /// </summary>
    [JsonProperty("DaysExcluded")]
    public List<bool> DaysExcluded { get; internal set; } = [];
    #endregion

    #region Constructor
    /// <summary>
    ///     Takes a <see cref="Quartz.Impl.Calendar.WeeklyCalendar" /> and wraps it in a json object
    /// </summary>
    /// <param name="weeklyCalendar"><see cref="Quartz.Impl.Calendar.WeeklyCalendar" /></param>
    public WeeklyCalendar(Quartz.Impl.Calendar.WeeklyCalendar weeklyCalendar) : base(weeklyCalendar)
    {
        Type = CalendarType.Weekly;

        foreach(var day in weeklyCalendar.DaysExcluded)
            DaysExcluded.Add(day);

        TimeZone = weeklyCalendar.TimeZone;
    }
    #endregion

    #region ToCalendar
    /// <summary>
    ///     Returns this object as a Quartz <see cref="Quartz.ICalendar" />
    /// </summary>
    /// <returns></returns>
    public override ICalendar ToCalendar()
    {
        var result = new Quartz.Impl.Calendar.WeeklyCalendar
        {
            Description = Description,
            TimeZone = TimeZone
        };

        for(var i = 0; i < DaysExcluded.Count; i++)
            result.SetDayExcluded((DayOfWeek) i + 1, DaysExcluded[i]);

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
    ///     Returns the <see cref="json" /> object from the given <paramref name="json" /> string
    /// </summary>
    /// <param name="json">The json string</param>
    /// <returns>
    ///     <see cref="WeeklyCalendar" />
    /// </returns>
    public new static WeeklyCalendar FromJsonString(string json)
    {
        return JsonConvert.DeserializeObject<WeeklyCalendar>(json);
    }
    #endregion
}