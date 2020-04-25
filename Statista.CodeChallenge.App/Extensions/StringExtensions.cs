namespace Statista.CodeChallenge.App.Extensions
{
    /// <summary>
    ///     Extensions for the <see cref="string" /> type.
    /// </summary>
    internal static class StringExtensions
    {
        /// <summary>
        ///     Convert the string value to a <see cref="double" /> value.
        /// </summary>
        /// <param name="value">A string value.</param>
        /// <returns>A double value.</returns>
        internal static double ToDouble(this string value)
        {
            if (value == null)
                return 0;
            else
            {
                double OutVal;
                bool isParsable = double.TryParse(value, out OutVal);

                if(!isParsable)
                {
                    return double.NegativeInfinity; //Value out of the range of Latitude and Longitude
                }

                if (double.IsNaN(OutVal) || double.IsInfinity(OutVal))
                {
                    return 0;
                }
                return OutVal;
            }
        }
    }
}
