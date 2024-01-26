﻿using Newtonsoft.Json;

namespace QuartzWebApi.Data;

/// <summary>
///     A json wrapper for the <see cref="Quartz.JobKey" />
/// </summary>
public class JobKey : Key
{
    #region Constructor
    /// <summary>
    ///     Makes this object and sets it's needed properties
    /// </summary>
    /// <param name="name">The name of the job</param>
    public JobKey(string name) : base(name)
    {
    }

    /// <summary>
    ///     Makes this object and sets it's needed properties
    /// </summary>
    /// <param name="name">The name of the trigger</param>
    /// <param name="group">The group of the trigger</param>
    public JobKey(string name, string group) : base(name, group)
    {
    }

    /// <summary>
    ///     Makes this object and sets it's needed properties
    /// </summary>
    /// <param name="key">The <see cref="Quartz.JobKey" /></param>
    public JobKey(Quartz.JobKey key) : base(key.Name, key.Group)
    {
    }
    #endregion

    #region ToJobKey
    /// <summary>
    ///     Returns this object as a Quartz <see cref="Quartz.JobKey" />
    /// </summary>
    /// <returns></returns>
    public Quartz.JobKey ToJobKey()
    {
        return new Quartz.JobKey(Name, Group);
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
    ///     Returns the <see cref="JobKey" /> object from the given <paramref name="json" /> string
    /// </summary>
    /// <param name="json">The json string</param>
    /// <returns>
    ///     <see cref="Data.Trigger" />
    /// </returns>
    public static JobKey FromJsonString(string json)
    {
        return JsonConvert.DeserializeObject<JobKey>(json);
    }
    #endregion
}