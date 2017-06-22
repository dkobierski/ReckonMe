using System.Collections.Generic;
using System.Threading.Tasks;
using ReckonMe.Models;
using ReckonMe.Models.Account;
using ReckonMe.Models.Wallet;

namespace ReckonMe.Services
{
    public interface IWalletService
    {
        /// <summary>
        /// Gets the wallets for user asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Wallet>> GetWalletsForUserAsync();
        /// <summary>
        /// Gets the wallet asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<Wallet> GetWalletAsync(string id);
        /// <summary>
        /// Adds the wallet.
        /// </summary>
        /// <param name="wallet">The wallet.</param>
        /// <returns></returns>
        Task AddWallet(AddWalletData wallet);
        /// <summary>
        /// Updates the wallet.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="wallet">The wallet.</param>
        /// <returns></returns>
        Task UpdateWallet(string id, EditWalletData wallet);
        /// <summary>
        /// Removes the wallet.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task RemoveWallet(string id);
    }
}