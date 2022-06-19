using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace QuartzWebApi.Data
{
    #region GroupMatcherType
    /// <summary>
    ///     The matching type
    /// </summary>
    public enum GroupMatcherType
    {
        /// <summary>
        ///     Should contain
        /// </summary>
        [DataMember(Name = "Contains")]
        Contains,

        /// <summary>
        ///     Should end with
        /// </summary>
        [DataMember(Name = "EndsWith")]
        EndsWith,

        /// <summary>
        ///     Should equal
        /// </summary>
        [DataMember(Name = "Equals")]
        Equals,

        /// <summary>
        ///     Should start with
        /// </summary>
        [DataMember(Name = "StartsWith")]
        StartsWith
    }
    #endregion

    public class GroupMatcher<T> where T : Quartz.Util.Key<T>
    {
        #region Properties
        /// <summary>
        ///     The type
        /// </summary>
        [JsonProperty("Type")]
        public GroupMatcherType Type { get; set; }

        /// <summary>
        ///     The value to match
        /// </summary>
        [JsonProperty("Value")]
        public string Value { get; set; }
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
        ///     Returns the <see cref="GroupMatcher{T}"/> object from the given <paramref name="json"/> string
        /// </summary>
        /// <param name="json">The json string</param>
        /// <returns><see cref="Data.Trigger"/></returns>
        public static GroupMatcher<T> FromJsonString(string json)
        {
            return JsonConvert.DeserializeObject<GroupMatcher<T>>(json);
        }
        #endregion

        #region ToGroupMatcher
        public Quartz.Impl.Matchers.GroupMatcher<T> ToGroupMatcher()
        {
            switch (Type)
            {
                case GroupMatcherType.Contains:
                    return Quartz.Impl.Matchers.GroupMatcher<T>.GroupContains(Value);

                case GroupMatcherType.EndsWith:
                    return Quartz.Impl.Matchers.GroupMatcher<T>.GroupEndsWith(Value);

                case GroupMatcherType.Equals:
                    return Quartz.Impl.Matchers.GroupMatcher<T>.GroupEquals(Value);

                case GroupMatcherType.StartsWith:
                    return Quartz.Impl.Matchers.GroupMatcher<T>.GroupStartsWith(Value);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        #endregion
    }
}