using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ReckonMe.Helpers;
using ReckonMe.Models;
using ReckonMe.Models.Account;
using Xamarin.Forms;

[assembly: Dependency(typeof(ReckonMe.Services.AccountService))]

namespace ReckonMe.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRequstExecutor _api;
        private readonly IDecodeToken _tokenDecoder;
        private readonly IAppStateHolder _appState;

        public AccountService() : this(DependencyService.Get<IRequstExecutor>(), new TokenDecoder(), DependencyService.Get<IAppStateHolder>())
        {
        }

        public AccountService(IRequstExecutor api, IDecodeToken tokenDecoder, IAppStateHolder appState)
        {
            _api = api;
            _tokenDecoder = tokenDecoder;
            _appState = appState;
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

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return AccountRegisterResult.AccountCreated;
                    case HttpStatusCode.Conflict:
                        return AccountRegisterResult.AlreadyExist;
                    default:
                        return AccountRegisterResult.RequestException;
                }
            }
            catch (HttpRequestException)
            {
                return AccountRegisterResult.RequestException;
            }
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

                    _appState.SetUser(new ApplicationUser
                    {
                        Username = user.Username
                    });

                    return AccountLoginResult.Authenticated;
                }

                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    return AccountLoginResult.RequestException;
                }
            }
            catch (HttpRequestException)
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