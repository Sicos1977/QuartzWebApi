﻿using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Quartz;

namespace QuartzWebApi.Data
{
    public class JobDetailWithTriggers
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
        [JsonProperty("Triggers")]
        public List<Trigger> Triggers { get; private set; }

        /// <summary>
        ///     <c>true</c> when the job needs to be replaced
        /// </summary>
        [JsonProperty("Replace")]
        public bool Replace { get; private set; }
        #endregion

        #region ToReadOnlyTriggerCollection
        /// <summary>
        ///     Returns the <see cref="Triggers"/> as a <see cref="IReadOnlyCollection{T}"/>
        /// </summary>
        /// <returns></returns>
        public IReadOnlyCollection<ITrigger> ToReadOnlyTriggerCollection()
        {
            return Triggers.Select(trigger => trigger.ToTrigger()).ToList();
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
        ///     Returns the <see cref="JobDetailWithTriggers"/> object from the given <paramref name="json"/> string
        /// </summary>
        /// <param name="json">The json string</param>
        /// <returns><see cref="Data.Trigger"/></returns>
        public static JobDetailWithTriggers FromJsonString(string json)
        {
            return JsonConvert.DeserializeObject<JobDetailWithTriggers>(json);
        }
        #endregion
    }
}