using Statista.CodeChallenge.App.Models;
using System.Threading.Tasks;

namespace Statista.CodeChallenge.App.Interfaces
{
    internal interface IWeatherService
    {
        /// <summary>
        ///     Make a request to get forecast data.
        /// </summary>
        /// <param name="forecastParameters">Forecast parameters.</param>
        /// <returns>A DarkSkyResponse with the API headers and data.</returns>
        Task<DarkSkyResponse> GetForecast(ForecastParameters forecastParameters);
    }
}
