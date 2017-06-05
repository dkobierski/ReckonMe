using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(ReckonMe.Helpers.RequestExecutor))]

namespace ReckonMe.Helpers
{
    public interface IRequstExecutor
    {
        Task<HttpResponseMessage> GetAsync(string relativeUrl);
        Task<HttpResponseMessage> PostAsync(string relativeUrl, StringContent content);

        void SetAuthToken(string token);
    }

    public class RequestExecutor : IRequstExecutor
    {
        private readonly HttpClient _client;

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

        public async Task<HttpResponseMessage> GetAsync(string relativeUrl)
        {
            return await _client.GetAsync(BuildUrl(relativeUrl));
        }

        public void SetAuthToken(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<HttpResponseMessage> PostAsync(string relativeUrl, StringContent content)
        {
            return await _client.PostAsync(BuildUrl(relativeUrl), content);
        }

        private string BuildUrl(string relativeUrl)
        {
            return $"{_client.BaseAddress}{relativeUrl}";
        }
    }
}