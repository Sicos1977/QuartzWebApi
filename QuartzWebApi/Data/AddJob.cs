using System;
using Newtonsoft.Json;
using Quartz;

namespace QuartzWebApi.Data
{
    public class AddJob
    {
        #region Properties
        /// <summary>
        ///     The key that identifies this jobs uniquely.
        /// </summary>
        [JsonProperty("JobKey")]
        public JobKey JobKey { get; private set; }

        /// <summary>
        /// Get or set the description given to the <see cref="IJob" /> instance by its
        /// creator (if any).
        /// </summary>
        [JsonProperty("Description")]
        public string Description { get; private set; }

        /// <summary>
        ///     Get or sets the instance of <see cref="IJob" /> that will be executed.
        /// </summary>
        [JsonProperty("JobType")]
        public string JobType { get; private set;}

        /// <summary>
        ///     Get or set the <see cref="JobDataMap" /> that is associated with the <see cref="IJob" />.
        /// </summary>
        [JsonProperty("JobDataMap")]
        public JobDataMap JobDataMap { get; private set; }

        /// <summary>
        ///     Whether or not the <see cref="IJob" /> should remain stored after it is
        ///     orphaned (no <see cref="ITrigger" />s point to it).
        /// </summary>
        /// <remarks>
        ///     If not explicitly set, the default value is <see langword="false" />.
        /// </remarks>
        /// <returns> 
        ///     <see langword="true" /> if the Job should remain persisted after being orphaned.
        /// </returns>
        [JsonProperty("Durable")]
        public bool Durable { get; private set; }

        [JsonProperty("Replace")]
        public bool Replace { get; private set; }

        [JsonProperty("StoreNonDurableWhileAwaitingScheduling")]
        public bool StoreNonDurableWhileAwaitingScheduling { get; private set; }
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
               .WithIdentity(JobKey)
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
        ///     Returns the <see cref="AddJob"/> object from the given <paramref name="json"/> string
        /// </summary>
        /// <param name="json">The json string</param>
        /// <returns><see cref="Data.Trigger"/></returns>
        public static AddJob FromJsonString(string json)
        {
            return JsonConvert.DeserializeObject<AddJob>(json);
        }
        #endregion
    }
}