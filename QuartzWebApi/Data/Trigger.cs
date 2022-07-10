//
// Trigger.cs
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

namespace QuartzWebApi.Data
{
    public class Trigger : JobKeyWithDataMap
    {
        #region Properties
        /// <summary>
        ///     The <see cref="TriggerKey"/>
        /// </summary>
        [JsonProperty("TriggerKey")]
        public TriggerKey TriggerKey { get; private set; }

        /// <summary>
        ///     A description for the <see cref="Trigger"/>
        /// </summary>
        [JsonProperty("Description")]
        public string Description { get; private set; }

        /// <summary>
        ///     The name of the calender to use or <c>null</c> when not
        /// </summary>
        [JsonProperty("CalendarName")]
        public string CalendarName { get; private set; }
        
        /// <summary>
        ///     The cron schedule
        /// </summary>
        [JsonProperty("CronSchedule")]
        public string CronSchedule { get; private set; }
        
        /// <summary>
        ///     The next fire time in UTC format
        /// </summary>
        [JsonProperty("NextFireTimeUtc")]
        public DateTimeOffset? NextFireTimeUtc { get; private set; }

        /// <summary>
        ///     The previous fire time in UTC format
        /// </summary>
        [JsonProperty("PreviousFireTimeUtc")]
        public DateTimeOffset? PreviousFireTimeUtc { get; private set; }

        /// <summary>
        ///     The start time in UTC format
        /// </summary>
        [JsonProperty("StartTimeUtc")]
        public DateTimeOffset StartTimeUtc { get; private set; }

        /// <summary>
        ///     The end time in UTC format
        /// </summary>
        [JsonProperty("EndTimeUtc")]
        public DateTimeOffset? EndTimeUtc { get; private set; }

        /// <summary>
        ///     The final fire time in UTC format
        /// </summary>
        [JsonProperty("FinalFireTimeUtc")]
        public DateTimeOffset? FinalFireTimeUtc { get; private set; }

        /// <summary>
        ///     The priority
        /// </summary>
        [JsonProperty("Priority")]
        public int Priority { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        ///     Makes this object and sets all it's needed properties
        /// </summary>
        /// <param name="triggerKey">The <see cref="TriggerKey"/></param>
        /// <param name="description">A description for the <see cref="Trigger"/></param>
        /// <param name="calendarName">The name of the calender to use or <c>null</c> when not</param>
        /// <param name="cronSchedule">The cron schedule</param>
        /// <param name="nextFireTimeUtc">The next fire time in UTC format</param>
        /// <param name="previousFireTimeUtc">The previous fire time in UTC format</param>
        /// <param name="startTimeUtc">The start time in UTC format</param>
        /// <param name="endTimeUtc">The end time in UTC format</param>
        /// <param name="finalFireTimeUtc">The final fire time in UTC format</param>
        /// <param name="priority">The priority</param>
        /// <param name="jobKey">The <see cref="JobKey"/></param>
        /// <param name="jobDataMap">The <see cref="JobDataMap"/></param>
        public Trigger(
            TriggerKey triggerKey,
            string description,
            string calendarName,
            string cronSchedule,
            DateTimeOffset? nextFireTimeUtc,
            DateTimeOffset? previousFireTimeUtc,
            DateTimeOffset startTimeUtc,
            DateTimeOffset? endTimeUtc,
            DateTimeOffset? finalFireTimeUtc,
            int priority,
            JobKey jobKey, 
            JobDataMap jobDataMap) : base (jobKey, jobDataMap)
        {
            TriggerKey = triggerKey;
            Description = description;
            CalendarName = calendarName;
            CronSchedule = cronSchedule;
            NextFireTimeUtc = nextFireTimeUtc;
            PreviousFireTimeUtc = previousFireTimeUtc;
            StartTimeUtc = startTimeUtc;
            EndTimeUtc = endTimeUtc;
            FinalFireTimeUtc = finalFireTimeUtc;
            Priority = priority;
        }
        #endregion

        #region ToTrigger
        /// <summary>
        ///     Returns this object as a Quartz <see cref="ITrigger"/>
        /// </summary>
        /// <returns></returns>
        public ITrigger ToTrigger()
        {
            var trigger = TriggerBuilder
                .Create()
                .ForJob(JobKey)
                .WithIdentity(TriggerKey)
                .WithPriority(Priority)
                .StartAt(StartTimeUtc);

            if (EndTimeUtc.HasValue)
                trigger = trigger.EndAt(EndTimeUtc.Value);

            if (!string.IsNullOrWhiteSpace(CronSchedule))
                trigger = trigger.WithCronSchedule(CronSchedule);
            
            if (!string.IsNullOrWhiteSpace(CalendarName))
                trigger = trigger.ModifiedByCalendar(CalendarName);

            if (!string.IsNullOrWhiteSpace(Description))
                trigger = trigger.WithDescription(Description);

            if (JobDataMap != null)
                trigger = trigger.UsingJobData(JobDataMap);

            return trigger.Build();
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
        ///     Returns the <see cref="Trigger"/> object from the given <paramref name="json"/> string
        /// </summary>
        /// <param name="json">The json string</param>
        /// <returns><see cref="Trigger"/></returns>
        public new static Trigger FromJsonString(string json)
        {
            return JsonConvert.DeserializeObject<Trigger>(json);
        }
        #endregion
    }
}