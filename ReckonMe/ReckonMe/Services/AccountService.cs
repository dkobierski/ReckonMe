using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ReckonMe.Helpers;
using ReckonMe.Models.Account;
using Xamarin.Forms;

[assembly: Dependency(typeof(ReckonMe.Services.AccountService))]

namespace ReckonMe.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRequstExecutor _api;
        private readonly IDecodeToken _tokenDecoder;

        public AccountService() : this(DependencyService.Get<IRequstExecutor>(), new TokenDecoder())
        {
        }

        public AccountService(IRequstExecutor api, IDecodeToken tokenDecoder)
        {
            _api = api;
            _tokenDecoder = tokenDecoder;
        }

        public async Task<AccountRegisterResult> SignUpUserAsync(AccountRegisterData user)
        {
            try
            {
                var content = new StringContent(
                    JsonConvert.SerializeObject(user),
                    Encoding.UTF8,
                    "application/json");

                var response = await _api.PostAsync("account/register", content)
                    .ConfigureAwait(false);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return AccountRegisterResult.AccountCreated;
                }

                if (response.StatusCode == HttpStatusCode.Conflict)
                    return AccountRegisterResult.AlreadyExist;
            }
            catch (HttpRequestException e)
            {
                return AccountRegisterResult.RequestException;
            }
            return AccountRegisterResult.RequestException;
        }

        public async Task<AccountLoginResult> LoginUserAsync(AccountLoginData user)
        {
            try
            {
                var content = new StringContent(
                    JsonConvert.SerializeObject(user),
                    Encoding.UTF8,
                    "application/json");

                var response = await _api.PostAsync("account/login", content)
                    .ConfigureAwait(false);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var token = await _tokenDecoder.Decode(response);
                    
                    _api.SetAuthToken(token.AccessToken);

                    return AccountLoginResult.Authenticated;
                }
            }
            catch (HttpRequestException e)
            {
                return AccountLoginResult.RequestException;
            }

            return AccountLoginResult.InvalidCredentials;
        }

        public bool IsUserLoggedIn()
        {
            return false;
        }
    }

    
}