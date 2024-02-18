using System.Collections.Generic;
using Newtonsoft.Json;

namespace QuartzWebApi.Wrappers;

/// <summary>
///     A json wrapper for the <see cref="Quartz.SchedulerContext" />
/// </summary>
public class SchedulerContext : Quartz.SchedulerContext
{
    #region Constructor
    internal SchedulerContext(Dictionary<string, string> items)
    {
        foreach (var item in items)
            Add(item.Key, item.Value);
    }

    internal SchedulerContext(Quartz.SchedulerContext schedulerContext)
    {
        foreach (var item in schedulerContext)
            Add(item.Key, item.Value);
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
    ///     Returns the <see cref="SchedulerContext" /> object from the given <paramref name="json" /> string
    /// </summary>
    /// <param name="json">The json string</param>
    /// <returns>
    ///     <see cref="Trigger" />
    /// </returns>
    public static SchedulerContext FromJsonString(string json)
    {
        var result2 = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        var result = JsonConvert.DeserializeObject<SchedulerContext>(json);
        return new SchedulerContext(result);
    }
    #endregion
}