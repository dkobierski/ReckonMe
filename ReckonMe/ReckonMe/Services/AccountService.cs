using System.Threading.Tasks;
using ReckonMe.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(ReckonMe.Services.AccountService))]

namespace ReckonMe.Services
{
    public class AccountService : IAccountService
    {
        private IRestApiClient _api;

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

        public Task<bool> LoginUserAsync(ApplicationUser user)
        {
            
            var x = new Task<bool>(() => true);
            x.Start();

            return x;
        }

        public bool IsUserLoggedIn()
        {
            return false;
        }
    }
}