﻿//
// JobDetail.cs
//
// Author: Kees van Spelde <sicos2002@hotmail.com>
//
// Copyright (c) 2022 Magic-Sessions. (www.magic-sessions.com)
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
using Newtonsoft.Json;
using Quartz;

namespace QuartzWebApi.Data;

/// <summary>
///     Json wrapper for the Quartz <see cref="IJobDetail"/>
/// </summary>
public class JobDetail
{
    #region Properties
    /// <summary>
    ///     The key that identifies this jobs uniquely
    /// </summary>
    [JsonProperty("JobKey")]
    public JobKey JobKey { get; private set; }

    /// <summary>
    /// Get or set the description given to the <see cref="IJob" /> instance by its
    /// creator (if any)
    /// </summary>
    [JsonProperty("Description")]
    public string Description { get; private set; }

    /// <summary>
    ///     Get or sets the instance of <see cref="IJob" /> that will be executed
    /// </summary>
    [JsonProperty("JobType")]
    public string JobType { get; private set;}

    /// <summary>
    ///     Get or set the <see cref="JobDataMap" /> that is associated with the <see cref="IJob" />
    /// </summary>
    [JsonProperty("JobDataMap")]
    public JobDataMap JobDataMap { get; private set; }

    /// <summary>
    ///     Whether or not the <see cref="IJob" /> should remain stored after it is
    ///     orphaned (no <see cref="ITrigger" />s point to it)
    /// </summary>
    /// <remarks>
    ///     If not explicitly set, the default value is <see langword="false" />
    /// </remarks>
    /// <returns> 
    ///     <see langword="true" /> if the Job should remain persisted after being orphaned
    /// </returns>
    [JsonProperty("Durable")]
    public bool Durable { get; private set; }

    /// <summary>
    ///     Whether or not the <see cref="IJob"/> should be replaced
    /// </summary>
    [JsonProperty("Replace")]
    public bool Replace { get; private set; }

    /// <summary>
    ///     Whether or not the <see cref="IJob"/> should be stored durable while awaiting scheduling
    /// </summary>
    [JsonProperty("StoreNonDurableWhileAwaitingScheduling")]
    public bool StoreNonDurableWhileAwaitingScheduling { get; private set; }
    #endregion

    #region Constructor
    /// <summary>
    ///     Creates this object and sets it's needed properties
    /// </summary>
    /// <param name="jobKey">The key that identifies this jobs uniquely</param>
    /// <param name="description">Get or set the description given to the <see cref="IJob" /> instance by its
    /// creator (if any)</param>
    /// <param name="jobType">Get or sets the instance of <see cref="IJob" /> that will be executed</param>
    /// <param name="jobDataMap">Get or set the <see cref="JobDataMap" /> that is associated with the <see cref="IJob" /></param>
    /// <param name="durable">Whether or not the <see cref="IJob" /> should remain stored after it is
    ///     orphaned (no <see cref="ITrigger" />s point to it)</param>
    /// <param name="replace">Whether or not the <see cref="IJob"/> should be replaced</param>
    /// <param name="storeNonDurableWhileAwaitingScheduling">Whether or not the <see cref="IJob"/> should be stored durable while awaiting scheduling</param>
    public JobDetail(
        JobKey jobKey,
        string description,
        string jobType,
        JobDataMap jobDataMap,
        bool durable,
        bool replace,
        bool storeNonDurableWhileAwaitingScheduling)
    {
        JobKey = jobKey;
        Description = description;
        JobType = jobType;
        JobDataMap = jobDataMap;
        Durable = durable;
        Replace = replace;
        StoreNonDurableWhileAwaitingScheduling = storeNonDurableWhileAwaitingScheduling;
    }

    /// <summary>
    ///     Create this object and sets it's needed properties
    /// </summary>
    /// <param name="jobDetail"><see cref="IJobDetail"/></param>
    public JobDetail(IJobDetail jobDetail)
    {
        JobKey = new JobKey(jobDetail.Key);
        Description = jobDetail.Description;
        JobType = jobDetail.JobType.ToString();
        JobDataMap = jobDetail.JobDataMap;
        Durable = jobDetail.Durable;
    }
    #endregion

    #region ToJobDetail
    /// <summary>
    ///     Returns this object as a Quartz <see cref="IJobDetail"/>
    /// </summary>
    /// <returns></returns>
    public IJobDetail ToJobDetail()
    {
        var type = Type.GetType(JobType);

        if (type == null)
            throw new Exception($"Could not find an IJob class with the type '{JobType}'");

        var job = JobBuilder.Create(type)
           .WithIdentity(JobKey.ToJobKey())
           .WithDescription(Description)
           .StoreDurably(Durable)
           .RequestRecovery()
           .Build();

        return job;
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
    ///     Returns the <see cref="JobDetail"/> object from the given <paramref name="json"/> string
    /// </summary>
    /// <param name="json">The json string</param>
    /// <returns><see cref="Data.Trigger"/></returns>
    public static JobDetail FromJsonString(string json)
    {
        return JsonConvert.DeserializeObject<JobDetail>(json);
    }
    #endregion
}