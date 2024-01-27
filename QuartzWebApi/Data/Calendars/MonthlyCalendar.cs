using System.Collections.Generic;
using Newtonsoft.Json;
using Quartz;

namespace QuartzWebApi.Data.Calendars;

/// <summary>
///     A json wrapper for the <see cref="Quartz.Impl.Calendar.MonthlyCalendar" />
/// </summary>
public class MonthlyCalendar : BaseCalendar
{
    #region Properties
    /// <summary>
    ///     Returns a collection of days of the month that are excluded
    /// </summary>
    [JsonProperty("DaysExcluded")]
    public List<bool> DaysExcluded { get; internal set; } = [];
    #endregion

    #region Constructor
    /// <summary>
    ///     Takes a <see cref="Quartz.Impl.Calendar.MonthlyCalendar" /> and wraps it in a json object
    /// </summary>
    /// <param name="monthlyCalendar"><see cref="Quartz.Impl.Calendar.MonthlyCalendar" /></param>
    public MonthlyCalendar(Quartz.Impl.Calendar.MonthlyCalendar monthlyCalendar) : base(monthlyCalendar)
    {
        Type = CalendarType.Monthly;

        foreach(var day in monthlyCalendar.DaysExcluded)
            DaysExcluded.Add(day);

        TimeZone = monthlyCalendar.TimeZone;
    }
    #endregion

    #region ToCalendar
    /// <summary>
    ///     Returns this object as a Quartz <see cref="Quartz.ICalendar" />
    /// </summary>
    /// <returns></returns>
    public override ICalendar ToCalendar()
    {
        var result = new Quartz.Impl.Calendar.MonthlyCalendar
        {
            Description = Description,
            TimeZone = TimeZone
        };

        for(var i = 0; i < DaysExcluded.Count; i++)
            result.SetDayExcluded(i + 1, DaysExcluded[i]);

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
    ///     Returns the <see cref="MonthlyCalendar" /> object from the given <paramref name="json" /> string
    /// </summary>
    /// <param name="json">The json string</param>
    /// <returns>
    ///     <see cref="MonthlyCalendar" />
    /// </returns>
    public new static MonthlyCalendar FromJsonString(string json)
    {
        return JsonConvert.DeserializeObject<MonthlyCalendar>(json);
    }
    #endregion
}