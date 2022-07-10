using System;
using Newtonsoft.Json;

namespace QuartzWebApi.Data
{
    public class SchedulerMetaData
    {
        #region Properties
        /// <summary>
        ///     Returns <c>true</c> when in standby mode
        /// </summary>
        [JsonProperty("InStandbyMode")]
        public bool InStandbyMode { get; }

        /// <summary>
        ///     Returns the job store type
        /// </summary>
        [JsonProperty("JobStoreType")]
        public Type JobStoreType { get; }

        /// <summary>
        ///     Returns <c>true</c> when the job store is clustered
        /// </summary>
        [JsonProperty("JobStoreClustered")]
        public bool JobStoreClustered { get; }

        /// <summary>
        ///     Returns <c>true</c> when the job store supports persistence
        /// </summary>
        [JsonProperty("JobsStoreSupportsPersistence")]
        public bool JobStoreSupportsPersistence { get; }

        /// <summary>
        ///     Returns the numbers of jobs executed
        /// </summary>
        [JsonProperty("NumbersOfJobsExecuted")]
        public int NumbersOfJobsExecuted { get; }

        /// <summary>
        ///     Returns the date time since the scheduler is running
        /// </summary>
        [JsonProperty("RunningSince")]
        public DateTimeOffset? RunningSince { get; }

        /// <summary>
        ///     Returns the scheduler instance id
        /// </summary>
        [JsonProperty("SchedulerInstanceId")]
        public string SchedulerInstanceId { get; }

        /// <summary>
        ///     Returns the scheduler name
        /// </summary>
        [JsonProperty("SchedulerName")]
        public string SchedulerName { get; }

        /// <summary>
        ///     Returns <c>true</c> when the scheduler is remote
        /// </summary>
        [JsonProperty("SchedulerRemote")]
        public bool SchedulerRemote { get; }

        /// <summary>
        ///     Returns the scheduler type
        /// </summary>
        [JsonProperty("SchedulerType")]
        public Type SchedulerType { get; }

        /// <summary>
        ///     Returns <c>true</c> when the scheduler is shutdown
        /// </summary>
        [JsonProperty("Shutdown")]
        public bool Shutdown { get; }

        /// <summary>
        ///     Returns <c>true</c> when the scheduler is started
        /// </summary>
        [JsonProperty("Started")]
        public bool Started { get; }

        /// <summary>
        ///     Returns the thread pool size
        /// </summary>
        [JsonProperty("ThreadPoolSize")]
        public int ThreadPoolSize { get; }

        /// <summary>
        ///     Returns the thread pool type
        /// </summary>
        [JsonProperty("ThreadPoolType")]
        public Type ThreadPoolType { get; }

        /// <summary>
        ///     Returns the scheduler version
        /// </summary>
        [JsonProperty("Version")]
        public string Version { get; }

        //[JsonProperty("summary")]
        //public string Summary { get; }
        #endregion

        #region Constructor
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
        ///     Returns the <see cref="SchedulerMetaData"/> object from the given <paramref name="json"/> string
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