using System.Runtime.Serialization;
using Newtonsoft.Json;
using Statista.CodeChallenge.App.Services;

namespace Statista.CodeChallenge.App.Models
{
    /// <summary>
    ///     Types of precipitation Dark Sky API can return.
    /// </summary>
    [JsonConverter(typeof(DarkSkyEnumJsonConverter))]
    internal enum PrecipitationType
    {
        /// <summary>
        ///     An unknown precipitation.
        /// </summary>
        [EnumMember(Value = null)] None,

        /// <summary>
        ///     Rain.
        /// </summary>
        [EnumMember(Value = "rain")] Rain,

        /// <summary>
        ///     Snow.
        /// </summary>
        [EnumMember(Value = "snow")] Snow,

        /// <summary>
        ///     Sleet.
        ///     <para>(which refers to each of freezing rain, ice pellets, and “wintery mix”)</para>
        /// </summary>
        [EnumMember(Value = "sleet")] Sleet
    }
}