using System;
using Newtonsoft.Json;
using Quartz;

namespace QuartzWebApi.Data
{
    public class Trigger
    {
        [JsonProperty("key")]
        public Quartz.TriggerKey Key { get; }

        [JsonProperty("jobKey")]
        public JobKey JobKey { get; }

        [JsonProperty("description")]
        public string Description { get; }

        [JsonProperty("calendarName")]
        public string CalendarName { get; }

        [JsonProperty("jobDataMap")]
        public Quartz.JobDataMap JobDataMap { get; }

        [JsonProperty("finalFireTimeUtc")]
        public DateTimeOffset? FinalFireTimeUtc { get; }

        [JsonProperty("misfireInstruction")]
        public int MisfireInstruction { get; }

        [JsonProperty("endTimeUtc")]
        public DateTimeOffset? EndTimeUtc { get; }

        [JsonProperty("startTimeUtc")]
        public DateTimeOffset StartTimeUtc { get; }

        [JsonProperty("priority")]
        public int Priority { get; set; }

        [JsonProperty("hasMillisecondPrecision")]
        public bool HasMillisecondPrecision { get; }
    }
}