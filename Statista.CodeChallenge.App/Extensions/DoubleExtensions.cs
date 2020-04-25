using System;
using System.Globalization;

namespace Statista.CodeChallenge.App.Extensions
{
    /// <summary>
    ///     Extensions for the <see cref="double" /> type.
    /// </summary>
    internal static class DoubleExtensions
    {
        /// <summary>
        ///     Convert Fahrenheit to a <see cref="string" /> Celsius.
        /// </summary>
        /// <param name="temperature">Temperature.</param>
        /// <returns>A string value representing degree celsius temperature with a °C postfix.</returns>
        internal static string ToDegreeCelsiusWithDegreePostfix(this double? temperature)
        {
            return $"{Math.Round(((temperature - 32) * 5 / 9).Value).ToString()}°C";
        }

        /// <summary>
        ///     Convert a double value to a <see cref="string" /> Percentage.
        /// </summary>
        /// <param name="doubleValue">Double Value.</param>
        /// <returns>A string value representing a percentage with % postfix.</returns>
        internal static string ToPercentageWithPercentPostfix(this double? doubleValue)
        {
            return $"{(doubleValue * 100).ToString()}%";
        }

        /// <summary>
        ///     Convert mile value to a <see cref="string" /> kilometer.
        /// </summary>
        /// <param name="mile">mile.</param>
        /// <returns>A string value representing a km/h with postfix.</returns>
        internal static string ToKmPerHrWithKmphPostfix(this double? mile)
        {
            return $"{mile * 1.6} km/h";
        }
    }
}
