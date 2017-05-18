using System.Net.Http;
using System.Threading.Tasks;
using ReckonMe.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(ReckonMe.Helpers.RequestExecutor))]

namespace ReckonMe.Helpers
{
    public interface IRequstExecutor
    {
        Task<HttpResponseMessage> PostAsync(string relativeUrl, StringContent content);
        void SetAuthToken(string token);
    }

    public class RequestExecutor : IRequstExecutor
    {
        private readonly IRestApiClient _api;

        public RequestExecutor() : this(DependencyService.Get<IRestApiClient>())
        {
        }

        public RequestExecutor(IRestApiClient api)
        {
            _api = api;
        }

        public Task<HttpResponseMessage> PostAsync(string relativeUrl, StringContent content)
        {
            return _api.Client.PostAsync($"{_api.Client.BaseAddress}{relativeUrl}", content);
        }

        public void SetAuthToken(string token)
        {
            _api.SetAuthToken(token);
        }
    }
}