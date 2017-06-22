using System.Threading.Tasks;
using ReckonMe.Models.Account;

namespace ReckonMe.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Signs up user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        Task<AccountRegisterResult> SignUpUserAsync(AccountRegisterData user);

        /// <summary>
        /// Logins the user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        Task<AccountLoginResult> LoginUserAsync(AccountLoginData user);

        /// <summary>
        /// Determines whether [is user logged in].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is user logged in]; otherwise, <c>false</c>.
        /// </returns>
        bool IsUserLoggedIn();
    }
}
