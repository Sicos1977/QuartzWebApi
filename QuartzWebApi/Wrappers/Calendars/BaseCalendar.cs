using System;
using Newtonsoft.Json;
using Quartz;

namespace QuartzWebApi.Wrappers.Calendars;

/// <summary>
///     The base calendar for all calendars
/// </summary>
/// <remarks>
///     Takes a <see cref="ICalendar" /> and wraps it in a json object
/// </remarks>
/// <param name="calendar"><see cref="ICalendar"/></param>
public abstract class BaseCalendar(ICalendar calendar)
{
    #region Properties
    /// <summary>
    ///     The type of the calendar
    /// </summary>
    [JsonProperty("Type")]
    public CalendarType Type { get; internal set; }

    /// <summary>
    ///     The description of the calendar
    /// </summary>
    [JsonProperty("Description", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string Description { get; internal set; } = calendar.Description;

    /// <summary>
    ///     The time zone of the calendar
    /// </summary>
    [JsonProperty("TimeZone", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public TimeZoneInfo TimeZone { get; internal set; }

    /// <summary>
    ///     The base of the calendar
    /// </summary>
    [JsonProperty("CalendarBase", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string CalendarBase { get; internal set; }

    /// <summary>
    ///     If set to <c>true</c> [replace].
    /// </summary>
    [JsonProperty("Replace", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool Replace { get; internal set; }

    /// <summary>
    ///     Whether to update existing triggers that
    ///     referenced the already existing calendar so that they are 'correct'
    ///     based on the new trigger.
    /// </summary>
    [JsonProperty("UpdateTriggers", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool UpdateTriggers { get; internal set; }

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
    ///     Returns the <see cref="json" /> object from the given <paramref name="json" /> string
    /// </summary>
    /// <param name="json">The json string</param>
    /// <returns>
    ///     <see cref="BaseCalendar" />
    /// </returns>
    public static BaseCalendar FromJsonString(string json)
    {
        return JsonConvert.DeserializeObject<BaseCalendar>(json);
    }
    #endregion

    public abstract ICalendar ToCalendar();
}