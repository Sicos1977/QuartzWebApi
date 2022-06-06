using System;
using Newtonsoft.Json;
using Quartz;

namespace QuartzWebApi.Data
{
    public class Trigger : JobKeyWithDataMap
    {
        #region Properties
        [JsonProperty("TriggerKey")]
        public TriggerKey TriggerKey { get; private set; }

        [JsonProperty("Description")]
        public string Description { get; private set; }

        [JsonProperty("CalendarName")]
        public string CalendarName { get; private set; }
        
        [JsonProperty("CronSchedule")]
        public string CronSchedule { get; private set; }
        
        [JsonProperty("NextFireTimeUtc")]
        public DateTimeOffset? NextFireTimeUtc { get; private set; }

        [JsonProperty("PreviousFireTimeUtc")]
        public DateTimeOffset? PreviousFireTimeUtc { get; private set; }
        
        [JsonProperty("StartTimeUtc")]
        public DateTimeOffset StartTimeUtc { get; private set; }

        [JsonProperty("EndTimeUtc")]
        public DateTimeOffset? EndTimeUtc { get; private set; }

        [JsonProperty("FinalFireTimeUtc")]
        public DateTimeOffset? FinalFireTimeUtc { get; private set; }

        [JsonProperty("Priority")]
        public int Priority { get; private set; }
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
        public string ToJsonString()
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