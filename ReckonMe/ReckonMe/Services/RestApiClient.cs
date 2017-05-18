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
            Client = new HttpClient
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

        public HttpClient Client { get; }

        public void SetAuthToken(string token)
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}