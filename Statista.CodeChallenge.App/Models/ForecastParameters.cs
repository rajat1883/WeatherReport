namespace Statista.CodeChallenge.App.Models
{
    internal class ForecastParameters
    {
        /// <summary>
        ///     Latitude for the location.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        ///     Longitude for the location.
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        ///     An API Key for the location.
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        ///     Optional Parameters for the location.
        /// </summary>
        public OptionalParameters OptionalParameters { get; set; }
    }
}
