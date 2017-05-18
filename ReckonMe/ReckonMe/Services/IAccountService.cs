using System.Threading.Tasks;
using ReckonMe.Models.Account;

namespace ReckonMe.Services
{
    public interface IAccountService
    {
        Task<bool> SignUpUserAsync(AccountRegisterData user);

        Task<AccountLoginResult> LoginUserAsync(AccountLoginData user);

        bool IsUserLoggedIn();
    }
}
