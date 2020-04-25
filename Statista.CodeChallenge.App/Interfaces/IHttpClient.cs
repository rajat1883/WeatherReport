using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Statista.CodeChallenge.App.Interfaces
{
    /// <summary>
    ///     Interface to use for making an HTTP request.
    /// </summary>
    internal interface IHttpClient : IDisposable
    {
        /// <summary>
        ///     The method to use when making an HTTP request.
        /// </summary>
        /// <param name="requestString">The full URL to make the request to.</param>
        /// <returns>The response from <paramref name="requestString" />.</returns>
        Task<HttpResponseMessage> HttpRequestAsync(string requestString);
    }
}