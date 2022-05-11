using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace QuartzWebApi.Data
{
    public class SchedulerMetaData
    {
        #region Properties
        [JsonProperty("inStandbyMode")]
        public bool InStandbyMode { get; }

        [JsonProperty("jobStoreType")]
        public Type JobStoreType { get; }

        [JsonProperty("jobStoreClustered")]
        public bool JobStoreClustered { get; }

        [JsonProperty("jobsStoreSupportsPersistence")]
        public bool JobStoreSupportsPersistence { get; }

        [JsonProperty("numbersOfJobsExecuted")]
        public int NumbersOfJobsExecuted { get; }

        [JsonProperty("runningSince")]
        public DateTimeOffset? RunningSince { get; }

        [JsonProperty("schedulerInstanceId")]
        public string SchedulerInstanceId { get; }

        [JsonProperty("schedulerName")]
        public string SchedulerName { get; }

        [JsonProperty("schedulerRemote")]
        public bool SchedulerRemote { get; }

        [JsonProperty("schedulerType")]
        public Type SchedulerType { get; }

        [JsonProperty("shutdown")]
        public bool Shutdown { get; }

        [JsonProperty("started")]
        public bool Started { get; }

        [JsonProperty("threadPoolSize")]
        public int ThreadPoolSize { get; }

        [JsonProperty("threadPoolType")]
        public Type ThreadPoolType { get; }

        [JsonProperty("version")]
        public string Version { get; }

        //[JsonProperty("summary")]
        //public string Summary { get; }
        #endregion

        #region Constructor
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
            //Summary = metaData.GetSummary();
        }
        #endregion

        public string SerializeToString()
        {
            var serializer = new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Include
            };

            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}