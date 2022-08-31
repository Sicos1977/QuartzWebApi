﻿//
// JobKeys.cs
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

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json;

namespace QuartzWebApi.Data
{
    [JsonArray]
    public class JobKeys : List<JobKey>
    {
        #region Constructor
        /// <summary>
        ///     Makes this object and sets all it's needed properties
        /// </summary>
        /// <param name="jobKeys">A <see cref="ReadOnlyCollection{T}"/> of <see cref="Quartz.JobKey"/>s</param>
        public JobKeys(IEnumerable<Quartz.JobKey> jobKeys)
        {
            foreach(var jobKey in jobKeys)
                Add(new JobKey(jobKey.Name, jobKey.Group));
        }
        #endregion

        #region ToJobKeys
        /// <summary>
        ///     Returns a <see cref="ReadOnlyCollection{T}"/> of <see cref="Quartz.JobKey"/>s
        /// </summary>
        /// <returns></returns>
        public IReadOnlyCollection<Quartz.JobKey> ToJobKeys()
        {
            var result = this.Select(m => m.ToJobKey()).ToList();
            return new ReadOnlyCollection<Quartz.JobKey>(result);
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
        ///     Returns the <see cref="JobKeys"/> object from the given <paramref name="json"/> string
        /// </summary>
        /// <param name="json">The json string</param>
        /// <returns><see cref="Data.Trigger"/></returns>
        public static JobKeys FromJsonString(string json)
        {
            return JsonConvert.DeserializeObject<JobKeys>(json);
        }
        #endregion
    }

    /// <summary>
    ///     A json wrapper for the <see cref="Quartz.JobKey"/>
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
        /// <param name="key">The <see cref="Quartz.JobKey"/></param>
        public JobKey(Quartz.JobKey key) : base(key.Name, key.Group)
        {
        }
        #endregion

        #region ToJobKey
        /// <summary>
        ///     Returns this object as a Quartz <see cref="Quartz.JobKey"/>
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
        ///     Returns the <see cref="JobKey"/> object from the given <paramref name="json"/> string
        /// </summary>
        /// <param name="json">The json string</param>
        /// <returns><see cref="Data.Trigger"/></returns>
        public static JobKey FromJsonString(string json)
        {
            return JsonConvert.DeserializeObject<JobKey>(json);
        }
        #endregion
    }
}