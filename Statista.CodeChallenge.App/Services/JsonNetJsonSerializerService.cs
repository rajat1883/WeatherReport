using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Statista.CodeChallenge.App.Interfaces;

namespace Statista.CodeChallenge.App.Services
{
    /// <summary>
    ///     Interface to use for handling JSON serialization via Json.NET
    /// </summary>
    internal class JsonNetJsonSerializerService : IJsonSerializerService
    {
        private readonly JsonSerializerSettings jsonSettings = new JsonSerializerSettings();

        /// <summary>
        ///     The method to use when deserializing a JSON object.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The resulting object from <paramref name="json" />.</returns>
        public async Task<T> DeserializeJsonAsync<T>(Task<string> json)
        {
            try
            {
                return (json != null)
                    ? JsonConvert.DeserializeObject<T>(await json.ConfigureAwait(false), jsonSettings)
                    : default;
            }
            catch (JsonReaderException e)
            {
                throw new FormatException("Json Parsing Error", e);
            }
        }
    }
}