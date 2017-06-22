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
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ReckonMe.Services.IAccountService" />
    public class AccountService : IAccountService
    {
        /// <summary>
        /// The API
        /// </summary>
        private readonly IRequstExecutor _api;
        /// <summary>
        /// The token decoder
        /// </summary>
        private readonly IDecodeToken _tokenDecoder;
        /// <summary>
        /// The application state
        /// </summary>
        private readonly IAppStateHolder _appState;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountService"/> class.
        /// </summary>
        public AccountService() : this(DependencyService.Get<IRequstExecutor>(), new TokenDecoder(), DependencyService.Get<IAppStateHolder>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountService"/> class.
        /// </summary>
        /// <param name="api">The API.</param>
        /// <param name="tokenDecoder">The token decoder.</param>
        /// <param name="appState">State of the application.</param>
        public AccountService(IRequstExecutor api, IDecodeToken tokenDecoder, IAppStateHolder appState)
        {
            _api = api;
            _tokenDecoder = tokenDecoder;
            _appState = appState;
        }

        /// <summary>
        /// Signs up user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Logins the user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Determines whether [is user logged in].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is user logged in]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsUserLoggedIn()
        {
            return false;
        }
    }
}