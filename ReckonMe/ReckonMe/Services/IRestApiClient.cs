using System.Net.Http;

namespace ReckonMe.Services
{
    public interface IRestApiClient
    {
        HttpClient Client { get; }
        void SetAuthToken(string token);
    }
}
