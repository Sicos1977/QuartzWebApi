using Newtonsoft.Json;
using Quartz;

namespace QuartzWebApi.Data
{
    public class JobKeyWithDataMap
    {
        #region Properties
        [JsonProperty("JobKey")]
        public JobKey JobKey { get; private set; }

        [JsonProperty("JobDataMap")]
        public Quartz.JobDataMap JobDataMap { get; private set; }
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