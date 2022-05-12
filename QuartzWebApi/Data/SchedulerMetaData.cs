using System;
using Newtonsoft.Json;

namespace QuartzWebApi.Data
{
    public class SchedulerMetaData
    {
        #region Properties
        [JsonProperty("InStandbyMode")]
        public bool InStandbyMode { get; }

        [JsonProperty("JobStoreType")]
        public Type JobStoreType { get; }

        [JsonProperty("JobStoreClustered")]
        public bool JobStoreClustered { get; }

        [JsonProperty("JobsStoreSupportsPersistence")]
        public bool JobStoreSupportsPersistence { get; }

        [JsonProperty("NumbersOfJobsExecuted")]
        public int NumbersOfJobsExecuted { get; }

        [JsonProperty("RunningSince")]
        public DateTimeOffset? RunningSince { get; }

        [JsonProperty("SchedulerInstanceId")]
        public string SchedulerInstanceId { get; }

        [JsonProperty("SchedulerName")]
        public string SchedulerName { get; }

        [JsonProperty("SchedulerRemote")]
        public bool SchedulerRemote { get; }

        [JsonProperty("SchedulerType")]
        public Type SchedulerType { get; }

        [JsonProperty("Shutdown")]
        public bool Shutdown { get; }

        [JsonProperty("Started")]
        public bool Started { get; }

        [JsonProperty("ThreadPoolSize")]
        public int ThreadPoolSize { get; }

        [JsonProperty("ThreadPoolType")]
        public Type ThreadPoolType { get; }

        [JsonProperty("Version")]
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
        public static SchedulerMetaData FromJsonString(string json)
        {
            return JsonConvert.DeserializeObject<SchedulerMetaData>(json);
        }
        #endregion
    }
}