using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(ReckonMe.Helpers.RequestExecutor))]

namespace ReckonMe.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRequstExecutor
    {
        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <param name="relativeUrl">The relative URL.</param>
        /// <returns></returns>
        Task<HttpResponseMessage> GetAsync(string relativeUrl);
        /// <summary>
        /// Posts the asynchronous.
        /// </summary>
        /// <param name="relativeUrl">The relative URL.</param>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        Task<HttpResponseMessage> PostAsync(string relativeUrl, StringContent content);
        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="relativeUrl">The relative URL.</param>
        /// <returns></returns>
        Task<HttpResponseMessage> DeleteAsync(string relativeUrl);
        /// <summary>
        /// Puts the asynchronous.
        /// </summary>
        /// <param name="relativeUrl">The relative URL.</param>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        Task<HttpResponseMessage> PutAsync(string relativeUrl, StringContent content);

        /// <summary>
        /// Sets the authentication token.
        /// </summary>
        /// <param name="token">The token.</param>
        void SetAuthToken(string token);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ReckonMe.Helpers.IRequstExecutor" />
    public class RequestExecutor : IRequstExecutor
    {
        /// <summary>
        /// The client
        /// </summary>
        private readonly HttpClient _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestExecutor"/> class.
        /// </summary>
        public RequestExecutor()
        {
            _client = new HttpClient
            {
                //                BaseAddress = new Uri("http://192.168.1.102:5001/api/"),
                BaseAddress = new Uri("http://reckonmeapi.azurewebsites.net/api/"),
                DefaultRequestHeaders =
                {
                    Accept =
                    {
                        new MediaTypeWithQualityHeaderValue("application/json")
                    }
                }
            };
        }

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <param name="relativeUrl">The relative URL.</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetAsync(string relativeUrl)
        {
            return await _client.GetAsync(BuildUrl(relativeUrl));
        }

        /// <summary>
        /// Puts the asynchronous.
        /// </summary>
        /// <param name="relativeUrl">The relative URL.</param>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PutAsync(string relativeUrl, StringContent content)
        {
            return await _client.PutAsync(BuildUrl(relativeUrl), content);
        }

        /// <summary>
        /// Sets the authentication token.
        /// </summary>
        /// <param name="token">The token.</param>
        public void SetAuthToken(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        /// <summary>
        /// Posts the asynchronous.
        /// </summary>
        /// <param name="relativeUrl">The relative URL.</param>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PostAsync(string relativeUrl, StringContent content)
        {
            return await _client.PostAsync(BuildUrl(relativeUrl), content);
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="relativeUrl">The relative URL.</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> DeleteAsync(string relativeUrl)
        {
            return await _client.DeleteAsync(BuildUrl(relativeUrl));
        }

        /// <summary>
        /// Builds the URL.
        /// </summary>
        /// <param name="relativeUrl">The relative URL.</param>
        /// <returns></returns>
        private string BuildUrl(string relativeUrl)
        {
            return $"{_client.BaseAddress}{relativeUrl}";
        }
    }
}