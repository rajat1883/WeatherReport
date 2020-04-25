using Microsoft.VisualStudio.TestTools.UnitTesting;
using Statista.CodeChallenge.App.Interfaces;
using Statista.CodeChallenge.App.Models;
using Statista.CodeChallenge.App.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Statista.CodeChallenge.App.Tests
{
    [TestClass]
    public class WeatherDataTest
    {
        private const string ApiKey = "5f9ed8d77da8834ba1b684b44d800320";
        private const string BadRequest = "Bad Request";
        private readonly IWeatherRepository weatherRepository;
        private readonly ForecastParameters forecastParameters;
        private DarkSkyResponse darkSkyResponse;

        public WeatherDataTest()
        {
            weatherRepository = new WeatherRepository();
            forecastParameters = new ForecastParameters()
            {
                ApiKey = ApiKey,
                OptionalParameters = new OptionalParameters()
                {
                    DataBlocksToExclude = new List<ExclusionBlocks>()
                    {
                        ExclusionBlocks.Alerts,
                        ExclusionBlocks.Daily,
                        ExclusionBlocks.Flags,
                        ExclusionBlocks.Hourly,
                        ExclusionBlocks.Minutely
                    }
                },
                Latitude = 51,
                Longitude = 14
            };
        }

        [TestMethod]
        public async Task AssertIsResponseNull()
        {
            darkSkyResponse = await weatherRepository.GetForecast(this.forecastParameters);

            Assert.IsNotNull(darkSkyResponse);
        }

        [TestMethod]
        public async Task AssertIsStatusSuccess()
        {
            darkSkyResponse = await weatherRepository.GetForecast(this.forecastParameters);

            Assert.AreEqual(darkSkyResponse.IsSuccessStatus, true);
        }

        [TestMethod]
        public async Task AssertHasOnlyCurrently()
        {
            darkSkyResponse = await weatherRepository.GetForecast(this.forecastParameters);

            Assert.IsNull(darkSkyResponse.Response.Alerts);
            Assert.IsNull(darkSkyResponse.Response.Daily);
            Assert.IsNull(darkSkyResponse.Response.Flags);
            Assert.IsNull(darkSkyResponse.Response.Hourly);
            Assert.IsNull(darkSkyResponse.Response.Minutely);
            Assert.IsNotNull(darkSkyResponse.Response.Currently);
        }

        [TestMethod]
        public async Task AssertResponseLocation()
        {
            darkSkyResponse = await weatherRepository.GetForecast(this.forecastParameters);

            Assert.IsTrue(darkSkyResponse.Response.TimeZone.ToLower().Contains("europe"));
        }

        [TestMethod]
        public async Task AssertInvalidApiKey()
        {
            this.forecastParameters.ApiKey = "Invalid API Key";
            darkSkyResponse = await weatherRepository.GetForecast(this.forecastParameters);

            Assert.AreEqual(darkSkyResponse.ResponseReasonPhrase, BadRequest);
        }
    }
}
