using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReckonMe.Models;
using ReckonMe.Models.Wallet;
using Xamarin.Forms;

[assembly: Dependency(typeof(ReckonMe.Services.WalletsDataStore))]
namespace ReckonMe.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ReckonMe.Services.IDataStore{ReckonMe.Models.Wallet.Wallet}" />
    public class WalletsDataStore : IDataStore<Wallet>
    {
        /// <summary>
        /// The wallets
        /// </summary>
        private List<Wallet> _wallets;

        /// <summary>
        /// The service
        /// </summary>
        private readonly IWalletService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="WalletsDataStore"/> class.
        /// </summary>
        public WalletsDataStore() : this(DependencyService.Get<IWalletService>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WalletsDataStore"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public WalletsDataStore(IWalletService service)
        {
            _service = service;
        }

        /// <summary>
        /// Adds the item asynchronous.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public async Task<bool> AddItemAsync(Wallet item)
        {
            await _service.AddWallet(new AddWalletData
            {
                Description = item.Description,
                Expenses = new List<Expense>(),
                Members = new List<string>(),
                Name = item.Name
            });

            _wallets.Add(item);

            return true;
        }

        /// <summary>
        /// Updates the item asynchronous.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public async Task<bool> UpdateItemAsync(Wallet item)
        {
            await InitializeAsync();
            
            var index = _wallets.FindIndex(arg => arg.Id == item.Id);
            if (index != -1)
            {
                _wallets[index] = item;
            }

            await _service.UpdateWallet(item.Id, new EditWalletData
            {
                Name = item.Name,
                Expenses = item.Expenses,
                Description = item.Description,
                Members = item.Members,
                Owner = item.Owner
            });

            return true;
        }

        /// <summary>
        /// Deletes the item asynchronous.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public async Task<bool> DeleteItemAsync(Wallet item)
        {
            await _service.RemoveWallet(item.Id);
            
            _wallets.RemoveAt(_wallets.FindIndex(arg => arg.Id == item.Id));
            
            return true;
        }

        /// <summary>
        /// Gets the item asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<Wallet> GetItemAsync(string id)
        {
            await InitializeAsync();

            return _wallets.FirstOrDefault(s => s.Id == id);
        }

        /// <summary>
        /// Gets the items asynchronous.
        /// </summary>
        /// <param name="forceRefresh">if set to <c>true</c> [force refresh].</param>
        /// <returns></returns>
        public async Task<IEnumerable<Wallet>> GetItemsAsync(bool forceRefresh = false)
        {
            await InitializeAsync();

            return _wallets;
        }

        /// <summary>
        /// Pulls the latest asynchronous.
        /// </summary>
        /// <returns></returns>
        public Task<bool> PullLatestAsync()
        {
            return Task.FromResult(true);
        }


        /// <summary>
        /// Synchronizes the asynchronous.
        /// </summary>
        /// <returns></returns>
        public Task<bool> SyncAsync()
        {
            return Task.FromResult(true);
        }

        /// <summary>
        /// Initializes the asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task InitializeAsync()
        {
            _wallets = new List<Wallet>();
            _wallets.AddRange(await _service.GetWalletsForUserAsync());
        }
    }
}
