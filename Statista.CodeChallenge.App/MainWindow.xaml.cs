using Statista.CodeChallenge.App.Models;
using Statista.CodeChallenge.App.Services;
using System.Windows;
using Statista.CodeChallenge.App.Interfaces;
using Statista.CodeChallenge.App.Extensions;
using System.Collections.Generic;
using System;

namespace Statista.CodeChallenge.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Declarations

        private const double InvalidInputCode = double.NegativeInfinity;
        private const string Forbidden = "Forbidden";
        private const string InvalidInputString = "Invalid input : ";
        private const string SomeErrorText = "Some Error occured";
        private const string LatitudeBoundary = "(Should be >= -90 and <= 90)";
        private const string LongitudeBoundary = "(Should be >= -180 and <= 180)";
        private IWeatherService weatherService;
        private OptionalParameters optionalParameters;
        private Dictionary<string, double> convertedLatitudeLongitude;
        private DataPoint currentWeather;
        private ForecastParameters forecastParameters;

        #endregion

        public MainWindow()
        {
            InitializeComponent();
            this.weatherService = new WeatherService();
            this.optionalParameters = new OptionalParameters()
            {
                DataBlocksToExclude = new List<ExclusionBlocks>()
                {
                    ExclusionBlocks.Alerts,
                    ExclusionBlocks.Daily,
                    ExclusionBlocks.Flags,
                    ExclusionBlocks.Hourly,
                    ExclusionBlocks.Minutely
                }
            };
            this.convertedLatitudeLongitude = new Dictionary<string, double>()
            {
                { this.Latitude.Name, 0 },
                { this.Longitude.Name, 0 }
            };
            this.forecastParameters = new ForecastParameters();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Loading(true);

            if (InputsAreInvalid())
            {
                Loading(false);
                return;
            }

            MapForecastParameters();

            weatherService.GetForecast(forecastParameters).ContinueWith((response) => MapAndFormatCurrentForecast(response.Result));
        }

        private bool InputsAreInvalid()
        {
            return ValidateApiKey()
                || ValidateAndConvertLatitudeAndLongitude(this.Latitude.Text, Latitude.Name)
                || ValidateAndConvertLatitudeAndLongitude(this.Longitude.Text, Longitude.Name);
        }

        private void MapForecastParameters()
        {
            this.forecastParameters.Latitude = this.convertedLatitudeLongitude[this.Latitude.Name];
            this.forecastParameters.Longitude = this.convertedLatitudeLongitude[this.Longitude.Name];
            this.forecastParameters.ApiKey = this.ApiKey.Text;
            this.forecastParameters.OptionalParameters = this.optionalParameters;
        }

        private bool ValidateApiKey()
        {
            if (string.IsNullOrEmpty(this.ApiKey.Text))
            {
                MessageBox.Show($"{InvalidInputString}{this.ApiKey.Name}");
                return true;
            }

            return false;
        }

        private bool ValidateAndConvertLatitudeAndLongitude(string inputValue, string inputName)
        {
            double inputValueDouble = inputValue.ToDouble();

            if (AreLatitudeLongitudeValid(inputName, inputValueDouble))
            {
                inputValueDouble = InvalidInputCode;
                var message = $"{InvalidInputString}{inputName} ";
                message += (inputName == this.Latitude.Name) ? LatitudeBoundary : LongitudeBoundary;
                MessageBox.Show(message);
            }

            convertedLatitudeLongitude[inputName] = inputValueDouble;

            return inputValueDouble == InvalidInputCode ? true : false;
        }

        private bool AreLatitudeLongitudeValid(string inputName, double inputValueDouble)
        {
            return (inputName == this.Latitude.Name && (inputValueDouble < -90 || inputValueDouble > 90)) 
                || (inputName == this.Longitude.Name && (inputValueDouble < -180 || inputValueDouble > 180));
        }

        private void MapAndFormatCurrentForecast(DarkSkyResponse darkSkyResponse)
        {
            Loading(false);

            this.Dispatcher.Invoke(() =>
            {
                if (!darkSkyResponse.IsSuccessStatus)
                {
                    if (darkSkyResponse.ResponseReasonPhrase == Forbidden)
                        MessageBox.Show($"{InvalidInputString}{this.ApiKey.Name}");
                    else
                        MessageBox.Show(SomeErrorText);
                    return;
                }

                MapCurrentForecastToUi(darkSkyResponse);
            });
        }

        private void MapCurrentForecastToUi(DarkSkyResponse darkSkyResponse)
        {
            currentWeather = darkSkyResponse?.Response?.Currently;
            this.Location.Content = currentWeather.TimeZone;
            this.ActualTemprature.Content = currentWeather.Temperature.ToDegreeCelsiusWithDegreePostfix();
            this.FeelsLikeTemprature.Content = currentWeather.ApparentTemperature.ToDegreeCelsiusWithDegreePostfix();
            this.Summary.Content = currentWeather.Summary;
            this.CloudCover.Content = currentWeather.CloudCover.ToPercentageWithPercentPostfix();
            this.WindSpeed.Content = currentWeather.WindSpeed.ToKmPerHrWithKmphPostfix();
            this.Humidity.Content = currentWeather.Humidity.ToPercentageWithPercentPostfix();
        }

        private void Loading(bool visible)
        {
            this.Dispatcher.Invoke(() =>
            {
                this.LoadingIcon.Visibility = visible ? Visibility.Visible : Visibility.Hidden;
            });
        }
    }
}
