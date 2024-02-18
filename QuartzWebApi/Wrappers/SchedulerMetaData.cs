//
// SchedulerMetaData.cs
//
// Author: Kees van Spelde <sicos2002@hotmail.com>
//
// Copyright (c) 2022 - 2024 Magic-Sessions. (www.magic-sessions.com)
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NON INFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//

using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace QuartzWebApi.Wrappers;

/// <summary>
///     Class used to read or create json to get the schedulers meta-data
/// </summary>
public class SchedulerMetaData
{
    #region Properties
    /// <summary>
    ///     Returns <c>true</c> when in standby mode
    /// </summary>
    [JsonProperty("InStandbyMode")]
    public bool InStandbyMode { get; private set; }

    /// <summary>
    ///     Returns the job store type
    /// </summary>
    [JsonProperty("JobStoreType")]
    public Type JobStoreType { get; private set; }

    /// <summary>
    ///     Returns <c>true</c> when the job store is clustered
    /// </summary>
    [JsonProperty("JobStoreClustered")]
    public bool JobStoreClustered { get; private set; }

    /// <summary>
    ///     Returns <c>true</c> when the job store supports persistence
    /// </summary>
    [JsonProperty("JobsStoreSupportsPersistence")]
    public bool JobStoreSupportsPersistence { get; private set; }

    /// <summary>
    ///     Returns the numbers of jobs executed
    /// </summary>
    [JsonProperty("NumbersOfJobsExecuted")]
    public int NumbersOfJobsExecuted { get; private set; }

    /// <summary>
    ///     Returns the date time since the scheduler is running
    /// </summary>
    [JsonProperty("RunningSince")]
    public DateTimeOffset? RunningSince { get; private set; }

    /// <summary>
    ///     Returns the scheduler instance id
    /// </summary>
    [JsonProperty("SchedulerInstanceId")]
    public string SchedulerInstanceId { get; private set; }

    /// <summary>
    ///     Returns the scheduler name
    /// </summary>
    [JsonProperty("SchedulerName")]
    public string SchedulerName { get; private set; }

    /// <summary>
    ///     Returns <c>true</c> when the scheduler is remote
    /// </summary>
    [JsonProperty("SchedulerRemote")]
    public bool SchedulerRemote { get; private set; }

    /// <summary>
    ///     Returns the scheduler type
    /// </summary>
    [JsonProperty("SchedulerType")]
    public Type SchedulerType { get; private set; }

    /// <summary>
    ///     Returns <c>true</c> when the scheduler is shutdown
    /// </summary>
    [JsonProperty("Shutdown")]
    public bool Shutdown { get; private set; }

    /// <summary>
    ///     Returns <c>true</c> when the scheduler is started
    /// </summary>
    [JsonProperty("Started")]
    public bool Started { get; private set; }

    /// <summary>
    ///     Returns the thread pool size
    /// </summary>
    [JsonProperty("ThreadPoolSize")]
    public int ThreadPoolSize { get; private set; }

    /// <summary>
    ///     Returns the thread pool type
    /// </summary>
    [JsonProperty("ThreadPoolType")]
    public Type ThreadPoolType { get; private set; }

    /// <summary>
    ///     Returns the scheduler version
    /// </summary>
    [JsonProperty("Version")]
    public string Version { get; private set; }

    [JsonProperty("Summary")]
    public string Summary { get; private set; }
    #endregion

    #region Constructor
    public SchedulerMetaData()
    {
    }

    /// <summary>
    ///     Creates this object and sets all it's needed properties
    /// </summary>
    /// <param name="metaData"></param>
    public SchedulerMetaData(Quartz.SchedulerMetaData metaData)
    {
        InStandbyMode = metaData.InStandbyMode;
        JobStoreType = metaData.JobStoreType;
        JobStoreClustered = metaData.JobStoreClustered;
        JobStoreSupportsPersistence = metaData.JobStoreSupportsPersistence;
        NumbersOfJobsExecuted = metaData.NumberOfJobsExecuted;
        RunningSince = metaData.RunningSince;
        SchedulerInstanceId = metaData.SchedulerInstanceId;
        SchedulerName = metaData.SchedulerName;
        SchedulerRemote = metaData.SchedulerRemote;
        SchedulerType = metaData.SchedulerType;
        Shutdown = metaData.Shutdown;
        Started = metaData.Started;
        ThreadPoolSize = metaData.ThreadPoolSize;
        ThreadPoolType = metaData.ThreadPoolType;
        Version = metaData.Version;
        Summary = metaData.GetSummary();
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

    static string UnescapeJsonString(string jsonString)
    {
        // Regular expression to match escaped characters
        var regex = new Regex(@"\\[rnt""\\]");
        return regex.Replace(jsonString, match =>
        {
            switch (match.Value)
            {
                case @"\r": return "\r";
                case @"\n": return "\n";
                case @"\t": return "\t";
                case @"\""": return "\"";
                case @"\\": return "\\";
                default: return match.Value;
            }
        });
    }

    #region FromJsonString
    /// <summary>
    ///     Returns the <see cref="SchedulerMetaData" /> object from the given <paramref name="json" /> string
    /// </summary>
    /// <param name="json">The json string</param>
    /// <returns>
    ///     <see cref="Trigger" />
    /// </returns>
    public static SchedulerMetaData FromJsonString(string json)
    {
        json = UnescapeJsonString(json);
        return JsonConvert.DeserializeObject<SchedulerMetaData>(json);
    }
    #endregion
}