using Newtonsoft.Json;
using Quartz;

namespace QuartzWebApi.Data
{
    public class RescheduleJob
    {
        #region Properties
        /// <summary>
        ///     The current trigger key
        /// </summary>
        [JsonProperty("CurrentTriggerKey")] 
        public TriggerKey CurrentTriggerKey { get; private set; }

        /// <summary>
        ///     The new trigger
        /// </summary>
        [JsonProperty("NewTrigger")] 
        public Trigger Trigger { get; private set; }
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
        ///     Returns the <see cref="RescheduleJob" /> object from the given <paramref name="json" /> string
        /// </summary>
        /// <param name="json">The json string</param>
        /// <returns>
        ///     <see cref="Data.Trigger" />
        /// </returns>
        public static RescheduleJob FromJsonString(string json)
        {
            return JsonConvert.DeserializeObject<RescheduleJob>(json);
        }
        #endregion
    }
}