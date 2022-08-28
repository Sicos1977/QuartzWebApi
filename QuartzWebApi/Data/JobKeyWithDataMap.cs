using Newtonsoft.Json;
using Quartz;

namespace QuartzWebApi.Data
{
    /// <summary>
    ///     Json wrapper to create a <see cref="Quartz.JobKey"/> with a <see cref="Quartz.JobDataMap"/>
    /// </summary>

    public class JobKeyWithDataMap
    {
        #region Properties
        /// <summary>
        ///     The <see cref="JobKey"/>
        /// </summary>
        [JsonProperty("JobKey")]
        public JobKey JobKey { get; private set; }

        /// <summary>
        ///     The <see cref="JobDataMap"/>
        /// </summary>
        [JsonProperty("JobDataMap")]
        public JobDataMap JobDataMap { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        ///     Makes this object and sets it's needed properties
        /// </summary>
        /// <param name="jobKey">The <see cref="JobKey"/></param>
        /// <param name="jobDataMap">The <see cref="JobDataMap"/></param>
        public JobKeyWithDataMap(JobKey jobKey, JobDataMap jobDataMap)
        {
            JobKey = jobKey;
            JobDataMap = jobDataMap;
        }

        /// <summary>
        ///     Makes this object and sets it's needed properties
        /// </summary>
        public JobKeyWithDataMap()
        {

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
        ///     Returns the <see cref="JobKeyWithDataMap"/> object from the given <paramref name="json"/> string
        /// </summary>
        /// <param name="json">The json string</param>
        /// <returns><see cref="Trigger"/></returns>
        public static JobKeyWithDataMap FromJsonString(string json)
        {
            return JsonConvert.DeserializeObject<JobKeyWithDataMap>(json);
        }
        #endregion
    }
}