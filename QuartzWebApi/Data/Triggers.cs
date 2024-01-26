using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Quartz;

namespace QuartzWebApi.Data;

/// <summary>
///    A list of <see cref="Quartz.ITrigger" />s
/// </summary>
[JsonArray]
public class Triggers : List<Trigger>
{
    #region Constructor
    /// <summary>
    ///     Makes this object and sets all it's needed properties
    /// </summary>
    /// <param name="triggers">A <see cref="ReadOnlyCollection{T}" /> of <see cref="Quartz.ITrigger" />s</param>
    public Triggers(IEnumerable<ITrigger> triggers)
    {
        foreach (var trigger in triggers)
            Add(new Trigger(trigger));
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
    ///     Returns the <see cref="Triggers" /> object from the given <paramref name="json" /> string
    /// </summary>
    /// <param name="json">The json string</param>
    /// <returns>
    ///     <see cref="Trigger" />
    /// </returns>
    public static Triggers FromJsonString(string json)
    {
        return JsonConvert.DeserializeObject<Triggers>(json);
    }
    #endregion
}