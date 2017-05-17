using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Xamarin.Forms;

[assembly: Dependency(typeof(ReckonMe.Services.RestApiClient))]

namespace ReckonMe.Services
{
    public class RestApiClient : IRestApiClient
    {
        public RestApiClient()
        {
            Client = new HttpClient();

            Client.BaseAddress = new Uri("http://10.0.2.2:5000/api/");
        }

        public HttpClient Client { get; }

        public void SetAuthToken(string token)
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);
        }
    }
}