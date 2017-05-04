using System.Threading.Tasks;
using ReckonMe.Models;

namespace ReckonMe.Services
{
    public interface IAccountService
    {
        Task<bool> SignUpUserAsync(ApplicationUser user);

        Task<bool> LoginUserAsync(ApplicationUser user);

        bool IsUserLoggedIn();
    }
}
