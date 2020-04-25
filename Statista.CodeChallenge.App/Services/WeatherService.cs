using Statista.CodeChallenge.App.Models;
using Statista.CodeChallenge.App.Interfaces;
using System.Threading.Tasks;
using Statista.CodeChallenge.App.Repositories;

namespace Statista.CodeChallenge.App.Services
{
    internal class WeatherService : IWeatherService
    {
        #region Declarations

        private readonly IWeatherRepository weatherRepository;

        #endregion

        public WeatherService()
        {
            weatherRepository = new WeatherRepository();
        }

        /// <summary>
        ///     Make a request to get forecast data.
        /// </summary>
        /// <param name="forecastParameters">Forecast parameters.</param>
        /// <returns>A DarkSkyResponse with the API headers and data.</returns>
        public Task<DarkSkyResponse> GetForecast(ForecastParameters forecastParameters)
        {
            return weatherRepository.GetForecast(forecastParameters);
        }
    }
}
