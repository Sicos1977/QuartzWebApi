using Newtonsoft.Json;

namespace QuartzWebApi.Data
{
    /// <summary>
    ///     Json wrapper to create a <see cref="Quartz.IJob"/> with a <see cref="Quartz.ITrigger"/>
    /// </summary>
    public class JobDetailWithTrigger
    {
        #region Properties
        /// <summary>
        ///     <see cref="JobDetail"/>
        /// </summary>
        [JsonProperty("JobDetail")]
        public JobDetail JobDetail { get; private set; }

        /// <summary>
        ///     A list with related <see cref="Trigger"/>'s
        /// </summary>
        [JsonProperty("Trigger")]
        public Trigger Trigger { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        ///     Makes this object and sets it's needed properties
        /// </summary>
        /// <param name="jobDetail"><see cref="JobDetail"/></param>
        /// <param name="trigger"><see cref="Trigger"/>'s</param>
        public JobDetailWithTrigger(JobDetail jobDetail, Trigger trigger)
        {
            JobDetail = jobDetail;
            Trigger = trigger;
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
        ///     Returns the <see cref="JobDetailWithTrigger"/> object from the given <paramref name="json"/> string
        /// </summary>
        /// <param name="json">The json string</param>
        /// <returns><see cref="Data.Trigger"/></returns>
        public static JobDetailWithTrigger FromJsonString(string json)
        {
            return JsonConvert.DeserializeObject<JobDetailWithTrigger>(json);
        }
        #endregion
    }
}