using System.Collections.Generic;
using System.Threading.Tasks;
using ReckonMe.Models;
using ReckonMe.Models.Account;
using ReckonMe.Models.Wallet;

namespace ReckonMe.Services
{
    public interface IWalletService
    {
        Task<IEnumerable<Wallet>> GetWalletsForUserAsync();
        Task<Wallet> GetWalletAsync(string id);
        Task AddWallet(AddWalletData wallet);
        Task UpdateWallet(string id, EditWalletData wallet);
        Task RemoveWallet(string id);
    }
}