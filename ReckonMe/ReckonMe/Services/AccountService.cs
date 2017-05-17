using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ReckonMe.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(ReckonMe.Services.AccountService))]

namespace ReckonMe.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRestApiClient _api;

        public AccountService() : this(DependencyService.Get<IRestApiClient>())
        {
        }

        public AccountService(IRestApiClient api)
        {
            _api = api;
        }

        public Task<bool> SignUpUserAsync(ApplicationUser user)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> LoginUserAsync(ApplicationUser user)
        {
            try
            {
                var response = await _api.Client.PostAsync($"{_api.Client.BaseAddress}account/login", new StringContent(
                    JsonConvert.SerializeObject(user),
                    Encoding.UTF8,
                    "application/json")).ConfigureAwait(false);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var token = new StreamReader(await response.Content.ReadAsStreamAsync()).ReadToEnd();

                    _api.SetAuthToken(token);

                }

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                    return false;
                

            }
            catch (HttpRequestException e)
            {
                return false;
            }

            return true;
        }

        public bool IsUserLoggedIn()
        {
            return false;
        }
    }
}